using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DataService.Utils
{
    public static class ClassReflectionUtil
    {
        public static string[] ToStringArray(this Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            return fields
                .Where(fieldInfo => fieldInfo.FieldType == typeof(string))
                .Select(fieldInfo => fieldInfo.GetValue(null).ToString())
                .ToArray();
        }
        
        public static Dictionary<string, string> ToKeyValuePairs(this Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            return fields
                .Where(fieldInfo => fieldInfo.FieldType == typeof(string))
                .ToDictionary(field => field.Name, field => field.GetValue(null).ToString());
        }
        
        public static List<string> GetAllConstantNames(this Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            return fields.Select(field => field.Name).ToList();
        }

        public static void SetConstant(this Type type, string name, string value)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var field in fields)
            {
                if (field.Name == name)
                {
                    field.SetValue(null, value);
                    return;
                }
            }
        }
        
        public static string GetConstant(this Type type, string name)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var field in fields)
            {
                if (field.Name == name)
                {
                    return field.GetValue(null).ToString();
                }
            }

            return null;
        }
        
        public static TProp GetPropertyValue<TProp>(this object src, string propName)
        {
            PropertyInfo propertyInfo = src.GetType().GetProperty(propName);
            
            var value = propertyInfo.GetValue(src);

            return (TProp) value;
        }
        
        //https://stackoverflow.com/questions/238765/given-a-type-expressiontype-memberaccess-how-do-i-get-the-field-value
        public static TProperty GetPropertyValue<TEntity, TProperty>(this TEntity src,
            Expression<Func<TEntity, TProperty>> propertyPath)
        {
            MemberExpression outerMember = (MemberExpression) propertyPath.Body;
            PropertyInfo outerProperty = (PropertyInfo) outerMember.Member;
            
            return (TProperty) outerProperty.GetValue(src);
        }
        
        public static void SetPropertyValue<TEntity, TProperty>(this TEntity src, TEntity from,
            Expression<Func<TEntity, TProperty>> propertyPath)
        {
            MemberExpression outerMember = (MemberExpression) propertyPath.Body;
            PropertyInfo outerProperty = (PropertyInfo) outerMember.Member;
            
            outerProperty.SetValue(src, from.GetPropertyValue(propertyPath));
        }
    }
}