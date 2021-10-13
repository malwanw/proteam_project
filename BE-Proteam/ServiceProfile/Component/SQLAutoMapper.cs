using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServiceProfile.Component
{
    public static class SQLAutoMapper
    {
        public static async Task<T> ToSingleAsync<T>(this DbDataReader dataReader)
        {
            if (dataReader == null || !dataReader.HasRows)
                return default(T);

            var returnType = typeof(T);
            T returnObject = (T)Activator.CreateInstance(returnType);
            var returnObjectProperties = returnType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(property => property.Name, property => property);

            await dataReader.ReadAsync();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string colName = dataReader.GetName(i);

                if (returnObjectProperties.ContainsKey(colName))
                {
                    var propertyInfo = returnObjectProperties[colName];

                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        var colValue = dataReader.GetValue(i);
                        var isNull = string.IsNullOrEmpty(colValue.ToString());
                        propertyInfo.SetValue(returnObject, isNull ? null : colValue);
                    }
                }
            }

            return returnObject;
        }

        public static T ToSingle<T>(this DbDataReader dataReader)
        {
            if (dataReader == null || !dataReader.HasRows)
                return default(T);

            var returnType = typeof(T);
            T returnObject = (T)Activator.CreateInstance(returnType);
            var returnObjectProperties = returnType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(property => property.Name, property => property);

            dataReader.Read();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string colName = dataReader.GetName(i);

                if (returnObjectProperties.ContainsKey(colName))
                {
                    var propertyInfo = returnObjectProperties[colName];

                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        var colValue = dataReader.GetValue(i);
                        var isNull = string.IsNullOrEmpty(colValue.ToString());
                        propertyInfo.SetValue(returnObject, isNull ? null : colValue);
                    }
                }
            }

            return returnObject;
        }

        public static async Task<List<T>> ToListAsync<T>(this DbDataReader dataReader)
        {
            if (dataReader == null || !dataReader.HasRows)
                return null;

            var returnObjectList = new List<T>();
            var returnType = typeof(T);
            var returnObjectProperties = returnType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(property => property.Name, property => property);

            while (await dataReader.ReadAsync())
            {
                T returnObject = (T)Activator.CreateInstance(returnType);

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    string colName = dataReader.GetName(i);

                    if (returnObjectProperties.ContainsKey(colName))
                    {
                        var propertyInfo = returnObjectProperties[colName];

                        if (propertyInfo != null && propertyInfo.CanWrite)
                        {
                            var colValue = dataReader.GetValue(i);
                            var isNull = string.IsNullOrEmpty(colValue.ToString());
                            propertyInfo.SetValue(returnObject, isNull ? null : colValue);
                        }
                    }
                }

                returnObjectList.Add(returnObject);
            }

            return returnObjectList;
        }

        public static List<T> ToList<T>(this DbDataReader dataReader)
        {
            if (dataReader == null || !dataReader.HasRows)
                return null;

            var returnObjectList = new List<T>();
            var returnType = typeof(T);
            var returnObjectProperties = returnType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(property => property.Name, property => property);

            while (dataReader.Read())
            {
                T returnObject = (T)Activator.CreateInstance(returnType);

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    string colName = dataReader.GetName(i);

                    if (returnObjectProperties.ContainsKey(colName))
                    {
                        var propertyInfo = returnObjectProperties[colName];

                        if (propertyInfo != null && propertyInfo.CanWrite)
                        {
                            var colValue = dataReader.GetValue(i);
                            var isNull = string.IsNullOrEmpty(colValue.ToString());
                            propertyInfo.SetValue(returnObject, isNull ? null : colValue);
                        }
                    }
                }

                returnObjectList.Add(returnObject);
            }

            return returnObjectList;
        }
    }
}