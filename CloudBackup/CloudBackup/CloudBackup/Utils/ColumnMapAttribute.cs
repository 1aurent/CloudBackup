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

namespace CloudBackup.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnMapAttribute : Attribute
    {
        readonly string _name;
        public ColumnMapAttribute(string param) { _name = param; }
        public string Name { get { return _name; } }
    }
}
