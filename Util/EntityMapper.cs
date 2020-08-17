using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace ClinicWeb.Util
{
    public class EntityMapper
    {
        public T Map<T>(IDataReader reader) where T : new()
        {
            var obj = new T();
            var objType = obj.GetType();
            var colNames = Enumerable.Range(0, reader.FieldCount)
                .Select(i => reader.GetName(i));

            foreach (var property in objType.GetProperties())
            {
                var colName = PascalCaseToSnakeCase(property.Name);
                if (!colNames.Contains(colName)) continue;

                var value = reader[colName];
                if (value is DBNull)
                    value = null;

                var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var actual = (value == null) ? null : Convert.ChangeType(value, type);
                property.SetValue(obj, actual);
            }

            return obj;
        }

        private string PascalCaseToSnakeCase(string str)
        {
            var underscoreConnected = string.Concat(str.Select((c, i) => (i != 0 && char.IsUpper(c)) ? "_" + c.ToString() : c.ToString()));
            return underscoreConnected.ToLower();
        }

        public List<T> MapList<T>(IDataReader reader) where T : new()
        {
            var result = new List<T>();
            while (reader.Read())
            {
                result.Add(Map<T>(reader));
            }

            return result;
        }
    }
}