using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return ApplyOrder(source, propertyName, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return ApplyOrder(source, propertyName, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        public static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            Expression expr = parameter;
            foreach (string prop in property.Split('.'))
            {
                // use reflection (not ComponentModel) to mirror LINQ
                expr = Expression.PropertyOrField(expr, prop);
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), expr.Type);
            LambdaExpression keySelector = Expression.Lambda(delegateType, expr, parameter);
            return (IOrderedQueryable<T>)
            typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                && method.IsGenericMethodDefinition
                && method.GetGenericArguments().Length == 2
                && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), expr.Type)
                .Invoke(null, new object[] { source, keySelector });
        }
    }
}
