using Common.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Common.Orm
{
    public static class OrderByDynamic
    {
        private static readonly MethodInfo OrderByMethod =
            typeof(Queryable).GetMethods().Single(method =>
           method.Name == "OrderBy" && method.GetParameters().Length == 2);

        private static readonly MethodInfo OrderByDescendingMethod =
            typeof(Queryable).GetMethods().Single(method =>
           method.Name == "OrderByDescending" && method.GetParameters().Length == 2);


        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, OrderByType orderByType, string[] propertyName)
        {
            if (propertyName.IsNotAny())
                return source;

            if (orderByType == OrderByType.OrderByDescending)
                return OrderByPropertyDescending(source, DefinePropertyName(propertyName));

            return OrderByPropertyAscending(source, DefinePropertyName(propertyName));
        }

        private static string DefinePropertyName(string[] propertyName)
        {
            var _propertyName = propertyName.LastOrDefault();
            var _parentProperty = _propertyName.Split('.')[0];
            return _parentProperty;
        }

        public static IQueryable<T> OrderByPropertyAscending<T>(this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return source;
            }
            var paramterExpression = Expression.Parameter(typeof(T));
            var orderByProperty = Expression.Property(paramterExpression, propertyName);
            var lambda = Expression.Lambda(orderByProperty, paramterExpression);
            var genericMethod = OrderByMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            var ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }

        public static IQueryable<T> OrderByPropertyDescending<T>(
            this IQueryable<T> source, string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return source;
            }
            var paramterExpression = Expression.Parameter(typeof(T));
            var orderByProperty = Expression.Property(paramterExpression, propertyName);
            var lambda = Expression.Lambda(orderByProperty, paramterExpression);
            var genericMethod = OrderByDescendingMethod.MakeGenericMethod(typeof(T), orderByProperty.Type);
            var ret = genericMethod.Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)ret;
        }


        private static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                BindingFlags.Public | BindingFlags.Instance) != null;
        }
    }
}
