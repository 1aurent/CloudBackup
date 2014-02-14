using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace CloudBackup.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UseAttributeAttribute : Attribute
    {
        private readonly string _name;

        public UseAttributeAttribute(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }
    }

    public class IgnoreAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Property)]
    public class UseInnerTextAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Property)]
    public class UseChildrenAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Class)]
    public class NamedAsAttribute : Attribute
    {
        private readonly string _name;

        public NamedAsAttribute(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }
    }

    /// <summary>
    /// Generic object loader. This class attempt to map XML nodes to object definitions 
    /// by using reflection (think a generic "XAML" loader).
    /// </summary>
    static public class ObjectLoadHelper
    {
        enum AttributeProcessStatus
        {
            Skip,
            LoadInnerText,
            LoadAttribute,
            Normal,
            ScanChilds
        }

        static XmlElement GetFirstChildNames(string tag, XmlNode parent)
        {
            var child = parent.FirstChild;
            for (;child != null;child = child.NextSibling)
            {
                if(! (child is XmlElement) ) continue;
                if (string.Compare(tag, child.Name, true) == 0) return (XmlElement)child;
            }
            return null;
        }

        static AttributeProcessStatus ScanAttribute(PropertyInfo prop,out string name)
        {
            name = null;

            foreach (var attribute in prop.GetCustomAttributes(true))
            {
                if (attribute is IgnoreAttribute)       return AttributeProcessStatus.Skip;
                if (attribute is UseInnerTextAttribute) return AttributeProcessStatus.LoadInnerText;
                if (attribute is UseChildrenAttribute)  return AttributeProcessStatus.ScanChilds;
                if (attribute is UseAttributeAttribute)
                {
                    name = ((UseAttributeAttribute)attribute).Name;
                    return AttributeProcessStatus.LoadAttribute;
                }
            }

            return AttributeProcessStatus.Normal;
        }

        static Type IdentifyCollectionType(Type propType)
        {
            if (propType.IsInterface && propType.Name == "ICollection`1")
            {
                return propType.GetGenericArguments()[0];
            }
            var cCollection = propType.GetInterface("ICollection`1");
            return cCollection != null ? cCollection.GetGenericArguments()[0] : null;
        }

        static bool IsTypeValid(Type t)
        {
            if (!t.IsClass || t.IsAbstract) return false;       //< Ignore value and abstract type
            return t.GetConstructor(Type.EmptyTypes) != null;   //< Must have a default public constructor
        }

        static string GetAlternateName(Type t)
        {
            foreach (var attribute in t.GetCustomAttributes(true))
            {
                if (attribute is NamedAsAttribute) return ((NamedAsAttribute)attribute).Name;
            }
            return t.Name;
        }

        static void LoadCollection(XmlElement element,PropertyInfo prop,Type colElementType,object target)
        {
            // Create a list of possible types
            var possibleTypes = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase);
            if(IsTypeValid(colElementType)) possibleTypes.Add(colElementType.Name,colElementType);
            foreach (var t in colElementType.Assembly.GetTypes())
            {
                if(!IsTypeValid(t) || !colElementType.IsAssignableFrom(t)) continue;
                if (!possibleTypes.ContainsKey(t.Name)) possibleTypes.Add(GetAlternateName(t), t);
            }

            var child = element.FirstChild;
            for (; child != null; child = child.NextSibling)
            {
                var childElement = child as XmlElement;
                if (childElement==null) continue;

                Type t;
                if (!possibleTypes.TryGetValue(childElement.Name,out t)) continue;
                var @object = t.Assembly.CreateInstance(t.FullName);
                Load(@object, childElement);

                prop.PropertyType.InvokeMember("Add", BindingFlags.InvokeMethod, null, target, new object[] {@object});
            }
        }

        static object CreateAndLoadObject(Type type,XmlElement element)
        {
            var instance = type.Assembly.CreateInstance(type.FullName);
            Load(instance, element);
            return instance;
        }

        static public void Load(object target,XmlElement element)
        {
            if(target  ==null) throw new ArgumentException("'target' parameter must point to an initialized object.");
            if(element ==null) throw new ArgumentException("Attempt to load from a null element.");

            var t = target.GetType();
            foreach (var prop in t.GetProperties())
            {
                string name;
                var status = ScanAttribute(prop, out name);
                if(status==AttributeProcessStatus.Skip) continue;


                var propType = prop.PropertyType;
                var cCollection = IdentifyCollectionType(propType);

                object pValue;
                if (cCollection != null)
                {
                    if (!prop.CanRead) continue;

                    var parent = status == AttributeProcessStatus.ScanChilds?element:GetFirstChildNames(prop.Name,element);
                    if(parent!=null) LoadCollection(parent, prop, cCollection, prop.GetValue(target, null));

                    continue;
                }
                if (propType.IsClass && propType != typeof(string) || propType.IsInterface)
                {
                    var child = GetFirstChildNames(prop.Name,element);
                    if (child == null) continue;

                    if (propType.IsInterface || !prop.CanWrite)
                    {
                        // Interface
                        if (!prop.CanRead) continue;

                        pValue = prop.GetValue(target, null);

                        Load(pValue, child);
                        continue;
                    }
                    pValue = CreateAndLoadObject(propType, child);
                }
                else
                {
                    if (!prop.CanWrite) continue;

                    string sourceText = null;

                    switch (status)
                    {
                        case AttributeProcessStatus.LoadAttribute:
                            var attr = element.Attributes[name];
                            if (attr != null) sourceText = attr.InnerText;
                            break;
                        case AttributeProcessStatus.LoadInnerText:
                            sourceText = element.InnerText;
                            break;
                        case AttributeProcessStatus.Normal:
                            var tchild = GetFirstChildNames(prop.Name,element);
                            if (tchild != null)
                            {
                                sourceText = tchild.InnerText;
                            }
                            break;
                    }

                    if (sourceText == null) continue;

                    if (propType != typeof(string))
                    {
                        pValue = propType.IsEnum ? Enum.Parse(propType, sourceText, true) : Convert.ChangeType(sourceText, propType);
                    }
                    else
                    {
                        pValue = sourceText;
                    }
                }

                prop.SetValue(target, pValue, null);
            }

        }


    }
}