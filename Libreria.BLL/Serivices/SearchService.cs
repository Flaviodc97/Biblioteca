using BibliotecaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibliotecaBLL.Serivices
{
    public static class SearchService<T> where T : class
    {

        public static IQueryable<T> GenericSearch(IQueryable<T> query, KeyValuePair<string, object> parameter)
        {
            var property = typeof(T).GetProperty(parameter.Key, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
            if (property is null) throw new Exception($"Error: Not Found Key {parameter.Key} in {typeof(T).Name} class");

            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameterExpression, property);

            Expression filterExpression = null;

            if (property.PropertyType == typeof(string))
            {
                var toLowerMeethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                var propertyToLower = Expression.Call(propertyAccess, toLowerMeethod);
                var valueToLower = Expression.Constant(parameter.Value.ToString().ToLower());

                filterExpression = Expression.Call(propertyToLower, containsMethod, valueToLower);

            }
            else if (IsNumericType(property.PropertyType))
            {

                var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                object convertedValue;

                if (parameter.Value == null || parameter.Value == DBNull.Value)
                {
                    convertedValue = null;
                }
                else if (parameter.Value is System.Text.Json.JsonElement jsonElement)
                {
                    // Estrazione da JsonElement a seconda del tipo
                    if (targetType == typeof(int))
                        convertedValue = jsonElement.GetInt32();
                    else if (targetType == typeof(long))
                        convertedValue = jsonElement.GetInt64();
                    else if (targetType == typeof(string))
                        convertedValue = jsonElement.GetString();
                    else if (targetType == typeof(bool))
                        convertedValue = jsonElement.GetBoolean();
                    else if (targetType == typeof(double))
                        convertedValue = jsonElement.GetDouble();
                    else
                        throw new InvalidOperationException($"Unsupported JsonElement to {targetType.Name} conversion.");
                }
                else if (targetType.IsInstanceOfType(parameter.Value))
                {
                    convertedValue = parameter.Value;
                }
                else if (parameter.Value is IConvertible)
                {
                    convertedValue = Convert.ChangeType(parameter.Value, targetType);
                }
                else
                {
                    throw new InvalidOperationException($"Parameter value of type {parameter.Value.GetType().Name} cannot be converted to {targetType.Name}");
                }

                var constant = Expression.Constant(convertedValue, property.PropertyType);
                filterExpression = Expression.Equal(propertyAccess, constant);
            }
            else
            {
                var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                var convertedValue = Convert.ChangeType(parameter.Value, targetType);
                var constantValue = Expression.Constant(parameter.Value, property.PropertyType);
                filterExpression = Expression.Equal(propertyAccess, constantValue);
            }

            if (!(filterExpression is null))
            {
                var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameterExpression);
                query = query.Where(lambda);
            }

            return query;

        }


        private static bool IsNumericType(Type type)
        {
            if (type == null) return false;

            // Gestisce tipi numerici standard
           
            return type == typeof(byte) || type == typeof(sbyte) ||
                   type == typeof(short) || type == typeof(ushort) ||
                   type == typeof(int) || type == typeof(uint) ||
                   type == typeof(long) || type == typeof(ulong) ||
                   type == typeof(float) || type == typeof(double) ||
                   type == typeof(decimal);

        }
    }

}
