using BibliotecaDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
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
            // OrderBy Section
            if (parameter.Key.ToLower() == "orderby")
            {
                // Recover Orderfield from parameter and verify if is Contained as property on the Entity
                string orderfield = parameter.Value.ToString();
                var orderProperty = typeof(T).GetProperty(orderfield, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);

                if (orderProperty is null) return query;

                // Preparetion of the Expression
                var orderParam = Expression.Parameter(typeof(T), "x");
                var orderByExpression = Expression.Property(orderParam, orderProperty);
                var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), orderProperty.PropertyType);
                var orderByLambda = Expression.Lambda(delegateType, orderByExpression, orderParam);

                // Finding the OrderBy Method for Queryable
                var orderByMethod = typeof(Queryable).GetMethods()
                    .First(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), orderProperty.PropertyType);

                // Invoke of OrderBy on query
                query = (IQueryable<T>)orderByMethod.Invoke(null, new object[] { query, orderByLambda });
            }
            else
            {
                // Equals section
                var property = typeof(T).GetProperty(parameter.Key, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
                if (property is null) return query;

                var parameterExpression = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.Property(parameterExpression, property);

                Expression filterExpression = null;
               
                // Equal String section
                if (property.PropertyType == typeof(string))
                {
                    // Recover To Lower Method
                    var toLowerMeethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                    // Recover Contains Method
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                    
                    var propertyToLower = Expression.Call(propertyAccess, toLowerMeethod);
                    var valueToLower = Expression.Constant(parameter.Value.ToString().ToLower());

                    // Create expression with ToLower property and Contains method
                    filterExpression = Expression.Call(propertyToLower, containsMethod, valueToLower);

                }
                else if (IsNumericType(property.PropertyType)) // Equal Numeric Section
                {

                    var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    object convertedValue;

                    if (parameter.Value == null || parameter.Value == DBNull.Value)
                    {
                        convertedValue = null;
                    }
                    else if (parameter.Value is System.Text.Json.JsonElement jsonElement)
                    {
                        // Estraction of the Json Element for  the target type
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
                else if (property.PropertyType == typeof(DateTime) ||  property.PropertyType == typeof(DateOnly) || property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateOnly?))
                {

                    var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    object convertedDate = default;

                    if (parameter.Value is null || parameter.Value == DBNull.Value)
                    {
                        convertedDate = null;
                    }
                    else if (parameter.Value is System.Text.Json.JsonElement jsonElment)
                    {
                        if (targetType == typeof(DateTime))
                            convertedDate = jsonElment.GetDateTime();
                        else if (targetType == typeof(DateOnly))
                            convertedDate = DateOnly.FromDateTime(jsonElment.GetDateTime());
                        else
                            return query;
                    }
                    else if (targetType.IsInstanceOfType(parameter.Value))
                    {
                        convertedDate = parameter.Value;
                    }
                    var constantDate = Expression.Constant(convertedDate, property.PropertyType);
                    filterExpression = Expression.Equal(propertyAccess, constantDate);
                }
                

                if (!(filterExpression is null))
                {
                    // Create lambda Expression with filter selected
                    var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameterExpression);
                    // Apply the filter method to the query with Where clause
                    query = query.Where(lambda);
                }

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
