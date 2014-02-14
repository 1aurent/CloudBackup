/* ........................................................................
 * copyright 2010 Laurent Dupuis
 * ........................................................................
 * < This program is free software: you can redistribute it and/or modify
 * < it under the terms of the GNU General Public License as published by
 * < the Free Software Foundation, either version 3 of the License, or
 * < (at your option) any later version.
 * < 
 * < This program is distributed in the hope that it will be useful,
 * < but WITHOUT ANY WARRANTY; without even the implied warranty of
 * < MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * < GNU General Public License for more details.
 * < 
 * < You should have received a copy of the GNU General Public License
 * < along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * ........................................................................
 *
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using PropertyAttributes=System.Reflection.PropertyAttributes;

namespace CloudBackup.Utils
{
    class EnumeratorCompiler
    {
        readonly TypeBuilder _tBuilder;
        readonly FieldBuilder _fldCls;
        readonly FieldBuilder _fldCur;
        readonly FieldBuilder _fldRdr;
        readonly PropertyInfo[] _fields;

        private EnumeratorCompiler(Type t, ModuleBuilder module)
        {
            var et = typeof(IEnumerator<>).MakeGenericType(t);
            _tBuilder = module.DefineType("DbEnumerator_" + t.Name, TypeAttributes.Class | TypeAttributes.Public, null, new[] { et });

            _fldCls = _tBuilder.DefineField("_cols", typeof(int[]), FieldAttributes.Private);
            _fldCur = _tBuilder.DefineField("_current", t, FieldAttributes.Private);
            _fldRdr = _tBuilder.DefineField("_rdr", t, FieldAttributes.Private);

            var fields = new List<PropertyInfo>();
            foreach (var member in t.GetMembers())
            {
                if (!(member is PropertyInfo)) continue;
                var property = (PropertyInfo) member;
                if (property.CanWrite && property.GetSetMethod().IsPublic) fields.Add(property);
            }
            _fields = fields.ToArray();
        }

        static string GetFieldName(PropertyInfo field)
        {
            var nameAttr = field.GetCustomAttributes(typeof(ColumnMapAttribute), true);
            return nameAttr.Length == 0 ? field.Name : ((ColumnMapAttribute)nameAttr[0]).Name;
        }

        void EmitConstructor()
        {
            var objectConstructor = typeof(object).GetConstructor(new Type[0]);
            var readerGetOrdinal = typeof(IDataRecord).GetMethod("GetOrdinal");

            var cb = _tBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard,
                                             new[] { typeof(IDataReader) });

            var il = cb.GetILGenerator();
            il.DeclareLocal(typeof(bool));
            var prcP = il.DefineLabel();
            var retP = il.DefineLabel();


            // call root constructor
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, objectConstructor);

            // if(_rdr==null) return;
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Brtrue_S, prcP);
            il.Emit(OpCodes.Br, retP);
            il.MarkLabel(prcP);

            // this._rdr = rdr;
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, _fldRdr);

            // this._cols = new int[ <fields.Length> ]
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldc_I4, _fields.Length);
            il.Emit(OpCodes.Newarr, typeof(int));
            il.Emit(OpCodes.Stfld, _fldCls);

            // <foreach fields>  { _cols[ <i> ] = _rdr.GetOrdinal( <name> ); }
            int i = 0;
            foreach (var field in _fields)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _fldCls);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _fldRdr);
                il.Emit(OpCodes.Ldstr, GetFieldName(field));
                il.Emit(OpCodes.Callvirt, readerGetOrdinal);
                il.Emit(OpCodes.Stelem_I4);

                i++;
            }

            il.MarkLabel(retP);
            il.Emit(OpCodes.Ret);
        }

        void EmitDispose()
        {
            var readerDispose = typeof(IDisposable).GetMethod("Dispose");
            var cb = _tBuilder.DefineMethod("Dispose", MethodAttributes.Public | MethodAttributes.Virtual);

            var il = cb.GetILGenerator();
            il.DeclareLocal(typeof(bool));
            var prcP = il.DefineLabel();
            var retP = il.DefineLabel();

            // if(_rdr=null) return;
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, _fldRdr);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Brtrue_S, prcP);
            il.Emit(OpCodes.Br_S, retP);
            il.MarkLabel(prcP);

            // _rdr.Dispose();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, _fldRdr);
            il.Emit(OpCodes.Callvirt, readerDispose);

            // _rdr = null;
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Stfld, _fldRdr);

            il.MarkLabel(retP);
            il.Emit(OpCodes.Ret);
        }

        void EmitReset()
        {
            var cb = _tBuilder.DefineMethod("Reset", MethodAttributes.Public | MethodAttributes.Virtual);
            var il = cb.GetILGenerator();
            il.Emit(OpCodes.Newobj, typeof(NotImplementedException).GetConstructor(new Type[0]));
            il.Emit(OpCodes.Throw);
        }

        void EmitMoveNext(Type t)
        {
            var readerRead = typeof(IDataReader).GetMethod("Read");
            var readerIsDBNull = typeof(IDataRecord).GetMethod("IsDBNull");
            var cb = _tBuilder.DefineMethod("MoveNext", MethodAttributes.Public | MethodAttributes.Virtual, typeof(bool), new Type[0]);
            var il = cb.GetILGenerator();

            il.DeclareLocal(typeof(bool));
            il.DeclareLocal(typeof(bool));

            var prPa = il.DefineLabel();
            var prPb = il.DefineLabel();
            var retP = il.DefineLabel();

            // if(_rdr=null) return false;
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, _fldRdr);
            il.Emit(OpCodes.Ldnull);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Ceq);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Brtrue_S, prPa);
            il.Emit(OpCodes.Br, retP);
            il.MarkLabel(prPa);

            //if(!_rdr.Read()) return false;
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, _fldRdr);
            il.Emit(OpCodes.Callvirt, readerRead);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Brtrue_S, prPb);
            il.Emit(OpCodes.Br, retP);
            il.MarkLabel(prPb);

            //_current=new <t>();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Newobj, t.GetConstructor(new Type[0]));
            il.Emit(OpCodes.Stfld, _fldCur);

            // <foreach fields>  { _current.<field> = _rdr.Get???( _cols[ <i> ] ); }
            var i = 0;
            foreach (var field in _fields)
            {
                var lclLbl = il.DefineLabel();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _fldRdr);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _fldCls);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem_I4);
                il.Emit(OpCodes.Callvirt, readerIsDBNull);
                il.Emit(OpCodes.Stloc_1);
                il.Emit(OpCodes.Ldloc_1);
                il.Emit(OpCodes.Brtrue_S, lclLbl);
                
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _fldCur);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _fldRdr);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, _fldCls);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem_I4);



                switch (field.PropertyType.Name)
                {
                    case "Object":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetValue"));
                        break;
                    case "Boolean":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetBoolean"));
                        break;
                    case "Byte":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetByte"));
                        break;
                    case "Char":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetChar"));
                        break;
                    case "DateTime":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetDateTime"));
                        break;
                    case "Decimal":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetDecimal"));
                        break;
                    case "Double":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetDouble"));
                        break;
                    case "Float":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetFloat"));
                        break;
                    case "Guid":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetGuid"));
                        break;
                    case "Int16":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetInt16"));
                        break;
                    case "Int32":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetInt32"));
                        break;
                    case "Int64":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetInt64"));
                        break;
                    case "String":
                        il.Emit(OpCodes.Callvirt, typeof(IDataRecord).GetMethod("GetString"));
                        break;

                    default:
                        throw new Exception("Unsupported type " + field.PropertyType.Name + " for property " + field.Name);
                }

                il.Emit(OpCodes.Callvirt, field.GetSetMethod());
                il.MarkLabel(lclLbl);
                i++;
            }

            il.MarkLabel(retP);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        void EmitCurrent(Type rT)
        {
            var isObject = rT == typeof (object);

            var pr = _tBuilder.DefineProperty("Current", PropertyAttributes.None, rT, new Type[0]);
            var cb = _tBuilder.DefineMethod(
                isObject ? "get_Current" : "get_Current", 
                MethodAttributes.Public | MethodAttributes.Virtual | 
                (isObject?(MethodAttributes.NewSlot|MethodAttributes.Final):0), rT, new Type[0]);

            pr.SetGetMethod(cb);
            var il = cb.GetILGenerator();
            il.DeclareLocal(rT);

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, _fldCur);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
        }

        public static Type Compile(Type t, ModuleBuilder module)
        {
            var c = new EnumeratorCompiler(t, module);

            c.EmitConstructor();
            c.EmitDispose();
            c.EmitReset();
            c.EmitMoveNext(t);

            c.EmitCurrent(t);
            c.EmitCurrent(typeof(object));

            return c._tBuilder.CreateType();
        }
    }
}
