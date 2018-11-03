using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Ybm.Infrastructure.Core.ExpressionHelper;
using Ybm.Infrastructure.Core.Pagination;

namespace Ybm.Infrastructure.Core.ExpressionHelper
{
    public static class ExpressionBuilder
    {
        #region OrderBy and OrderByDescending Extensions by field name
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }
        public static IQueryable<T> ThenBy<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "ThenBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }
        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }
        public static IQueryable<T> ThenByDescending<T>(this IQueryable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == "ThenByDescending" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, expr });
        }


        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> query, string name)
        {
            var propInfo = GetPropertyInfo(typeof(T), name);
            var expr = GetOrderExpression(typeof(T), propInfo);

            var method = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "OrderBy" && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(T), propInfo.PropertyType);
            return (IEnumerable<T>)genericMethod.Invoke(null, new object[] { query, expr.Compile() });
        }


        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }
        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }
        #endregion




        public static IOrderedQueryable<T> MakeOrderExpression<T>(this IQueryable<T> source, HashSet<SortDescriptor> descriptors)
        {
            var type = typeof(T);
            LambdaExpression tempExpressionTree;
            foreach (var descriptor in descriptors)
            {
                ParameterExpression pe = Expression.Parameter(type, "q");
                MemberExpression me = Expression.Property(pe, descriptor.field);
                tempExpressionTree = Expression.Lambda(me, pe);
                return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(tempExpressionTree);
            }
            return null;
        }



        public static Expression<Func<T, bool>> GetExpression<T>(QueryModel queryModel)
        {
            List<CustomFilterDescriptor> selectedFilters = new List<CustomFilterDescriptor>();
            if (queryModel.CustomFilterMetDataDescriptors != null && queryModel.CustomFilterMetDataDescriptors.Any())
            {
                foreach (var q in queryModel.CustomFilterMetDataDescriptors)
                {
                    Type MemberType = null;
                    if (q.MemberType == "System.Int32?")
                        MemberType = typeof(Int32?);
                    else if (q.MemberType == "System.DateTime?")
                        MemberType = typeof(System.DateTime?);
                    else if (q.MemberType == "System.TimeSpan?")
                        MemberType = typeof(System.TimeSpan?);
                    else
                        MemberType = Type.GetType(q.MemberType);

                    selectedFilters.Add(
                        new CustomFilterDescriptor()
                        {
                            Member = q.Member,
                            Value = q.Value,
                            MemberType = MemberType,
                            Operator = (FilterOperator)Enum.Parse(typeof(FilterOperator), q.Operator)
                        });
                }
            }

            return MakeTheExpression<T>(selectedFilters);
        }

        private static List<CustomFilterDescriptor> GetFilterFields<T>(params Tuple<Expression<Func<Object>>, string>[] property)
        {

            var entityType = typeof(T);
            List<CustomFilterDescriptor> descriptors = new List<CustomFilterDescriptor>();
            Expression<Func<T, bool>> ExpressionTree = null;
            foreach (var prop in property)
            {
                var p = prop.Item1;
                string propertyName = "";
                Type propertyType;




                var myVisitor = new MyExpressionVisitor();
                // visit the expression's Body instead
                myVisitor.Visit(p);


                if (((LambdaExpression)(p as Expression)).Body is UnaryExpression)
                {
                    propertyName = ((MemberExpression)((UnaryExpression)((LambdaExpression)(p as Expression)).Body).Operand).Member.Name;
                    propertyType = ((System.Reflection.PropertyInfo)(((MemberExpression)((UnaryExpression)((LambdaExpression)(p as Expression)).Body).Operand).Member)).PropertyType;
                }
                else
                {
                    propertyName = ((MemberExpression)((LambdaExpression)(p as Expression)).Body).Member.Name;
                    propertyType = ((PropertyInfo)(((MemberExpression)((LambdaExpression)(p as Expression)).Body).Member)).PropertyType;
                }
                //descriptors.Add(new Kendo.Mvc.FilterDescriptor(propertyName, Kendo.Mvc.FilterOperator.IsEqualTo, GetDefault(propertyType)) { MemberType = propertyType });
                descriptors.Add(new CustomFilterDescriptor()
                {
                    Member = "FilterField_" + propertyName,
                    Operator = FilterOperator.IsEqualTo,
                    Value = GetDefault(propertyType),
                    MemberType = propertyType,
                    Name = prop.Item2

                });// (propertyName, Kendo.Mvc.FilterOperator.IsEqualTo, GetDefault(propertyType)) { MemberType = propertyType });
            }
            return descriptors;
        }
        public static void SyncFilterData(List<CustomFilterDescriptor> filterData, Dictionary<string, string> selectedFilters)
        {

            foreach (var sf in selectedFilters)
            {
                var filter = filterData.FirstOrDefault(q => q.Member == sf.Key.Replace("FilterField_", ""));
                var selected = selectedFilters.FirstOrDefault(q => q.Key.Replace("FilterField_", "") == sf.Key.Replace("FilterField_", ""));

                if (filter == null)
                    continue;

                if (selected.Equals(new KeyValuePair<string, string>()))
                    continue;

                if (selected.Value == null)
                    continue;

                if (selected.Value.Equals(GetDefault(filter.MemberType)))
                    continue;

                if (filter.MemberType == typeof(string))
                    filter.Value = selected.Value;
                else
                if ((filter.Value != null) && (filter.Value.ToString() != selected.Value))
                    filter.Value = selected.Value;
                else
                    filter.Value = selected.Value;
            }

            List<CustomFilterDescriptor> filterDataToDelete = new List<CustomFilterDescriptor>();

            foreach (var fd in filterData)
                if ((fd.Value == null) || (fd.Value.Equals(GetDefault(fd.MemberType))))
                    filterDataToDelete.Add(fd);

            foreach (var fd in filterDataToDelete)
                filterData.Remove(fd);

        }
        public static Expression<Func<T, bool>> MakeTheExpression<T>(List<CustomFilterDescriptor> descriptors)
        {
            var entityType = typeof(T);
            Expression<Func<T, bool>> ExpressionTree = null;

            foreach (var descriptor in descriptors)
            {
                if ((descriptor.Value == null) || (string.IsNullOrWhiteSpace(descriptor.Value.ToString())))
                    continue;
                Expression<Func<T, bool>> tempExpressionTree;
                if (descriptor.MemberType == typeof(TimeSpan?))
                {
                    descriptor.Value = descriptor.Value.ToString();
                    var timeValue = DateTime.Parse(descriptor.Value.ToString()).TimeOfDay;

                    ParameterExpression pe2 = Expression.Parameter(entityType, "q");
                    MemberExpression me2 = Expression.Property(pe2, descriptor.Member);
                    var value2 = ChangeType(timeValue, descriptor.MemberType);
                    ConstantExpression constant2 = Expression.Constant(value2, descriptor.MemberType);
                    BinaryExpression body2 = MakeConstant(descriptor, me2, constant2);
                    tempExpressionTree = Expression.Lambda<Func<T, bool>>(body2, new[] { pe2 });
                }
                else if (descriptor.MemberType == typeof(DateTime))
                {
                    #region [DateTime]
                    descriptor.Value = descriptor.Value.ToString();
                    var dateTimeValue = descriptor.Value; // ((DateTime)descriptor.Value.ToString());//.GetEnglishDateTime());

                    ParameterExpression pe2 = Expression.Parameter(entityType, "q");
                    MemberExpression me2 = Expression.Property(pe2, descriptor.Member);
                    var value2 = ChangeType(dateTimeValue, descriptor.MemberType);
                    ConstantExpression constant2 = Expression.Constant(value2, descriptor.MemberType);
                    BinaryExpression body2 = MakeConstant(descriptor, me2, constant2);
                    //BinaryExpression body2 = Expression.LessThanOrEqual(me2, constant2);
                    tempExpressionTree = Expression.Lambda<Func<T, bool>>(body2, new[] { pe2 });


                    //ParameterExpression pe1 = Expression.Parameter(entityType, "q");
                    //MemberExpression me1 = Expression.Property(pe1, descriptor.Member);
                    //var value1 = ChangeType(descriptor.Value, descriptor.MemberType);
                    //ConstantExpression constant1 = Expression.Constant(value1, descriptor.MemberType);

                    //BinaryExpression body1 = MakeConstant(descriptor, me1, constant1);
                    ////BinaryExpression body1 = Expression.GreaterThan(me1, constant1);
                    //var tempExpressionTree1 = Expression.Lambda<Func<T, bool>>(body1, new[] { pe1 });

                    //ParameterExpression pe2 = Expression.Parameter(entityType, "q");
                    //MemberExpression me2 = Expression.Property(pe2, descriptor.Member);
                    //var value2 = ChangeType(endDateTime, descriptor.MemberType);
                    //ConstantExpression constant2 = Expression.Constant(value2, descriptor.MemberType);
                    //BinaryExpression body2 = MakeConstant(descriptor, me2, constant2);
                    ////BinaryExpression body2 = Expression.LessThanOrEqual(me2, constant2);
                    //var tempExpressionTree2 = Expression.Lambda<Func<T, bool>>(body2, new[] { pe2 });

                    //tempExpressionTree = tempExpressionTree1.And(tempExpressionTree2);
                    #endregion
                }
                else
              if (descriptor.MemberType == typeof(String) && descriptor.Operator == FilterOperator.Contains)
                {
                    var pe = Expression.Parameter(entityType, "q");
                    var me = Expression.Property(pe, descriptor.Member);
                    MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var value = ChangeType(descriptor.Value, descriptor.MemberType);
                    var someValue = Expression.Constant(value, descriptor.MemberType);
                    var containsMethodExp = Expression.Call(me, method, someValue);

                    tempExpressionTree = Expression.Lambda<Func<T, bool>>(containsMethodExp, pe);
                }
                else
              if (descriptor.MemberType == typeof(String))
                {
                    ParameterExpression pe2 = Expression.Parameter(entityType, "q");
                    MemberExpression me2 = Expression.Property(pe2, descriptor.Member);
                    var value2 = ChangeType(descriptor.Value, descriptor.MemberType);
                    ConstantExpression constant2 = Expression.Constant(value2, descriptor.MemberType);
                    BinaryExpression body2 = MakeConstant(descriptor, me2, constant2);
                    //BinaryExpression body2 = Expression.LessThanOrEqual(me2, constant2);
                    tempExpressionTree = Expression.Lambda<Func<T, bool>>(body2, new[] { pe2 });
                }
                else
                {
                    #region [Other Types]
                    ParameterExpression pe = Expression.Parameter(entityType, "q");
                    MemberExpression me = Expression.Property(pe, descriptor.Member);
                    var value = ChangeType(descriptor.Value, descriptor.MemberType);
                    ConstantExpression constant = Expression.Constant(value, descriptor.MemberType);
                    BinaryExpression body = MakeConstant(descriptor, me, constant);
                    //BinaryExpression body = Expression.Equal(me, constant);

                    tempExpressionTree = Expression.Lambda<Func<T, bool>>(body, new[] { pe });
                    #endregion
                }

                if (ExpressionTree == null)
                    ExpressionTree = tempExpressionTree;
                else
                {
                    ExpressionTree = ExpressionTree.And(tempExpressionTree);
                }
            }

            //var myVisitor = new MyExpressionVisitor();
            // visit the expression's Body instead
            //myVisitor.Visit(ExpressionTree.Body);


            return ExpressionTree;
            //var testlist = complaintBiz.FetchMulti(ExpressionTree).ToList();
        }

        public static BinaryExpression MakeConstant(CustomFilterDescriptor descriptor, MemberExpression me, ConstantExpression constant)
        {
            if (descriptor.Operator == FilterOperator.IsEqualTo)
                return Expression.Equal(me, constant);
            else
            if (descriptor.Operator == FilterOperator.IsGreaterThan)
                return Expression.GreaterThan(me, constant);
            else
            if (descriptor.Operator == FilterOperator.IsGreaterThanOrEqualTo)
                return Expression.GreaterThanOrEqual(me, constant);
            else
            if (descriptor.Operator == FilterOperator.IsLessThan)
                return Expression.LessThan(me, constant);
            else
            if (descriptor.Operator == FilterOperator.IsLessThanOrEqualTo)
                return Expression.LessThanOrEqual(me, constant);
            else

                return Expression.Equal(me, constant);
        }


        public static object ChangeType(object value, Type toType)
        {
            if (Nullable.GetUnderlyingType(toType) != null)
            {
                if (toType == typeof(Nullable<bool>))
                    return Convert.ToBoolean(int.Parse(value.ToString()));

                if (toType == typeof(Nullable<Int32>))
                    return Convert.ToInt32(int.Parse(value.ToString()));

                // It's nullable
                TypeConverter conv = TypeDescriptor.GetConverter(toType);
                return conv.ConvertFrom(value);
            }
            else
                return Convert.ChangeType(value, toType);
        }
        public static object GetDefault(Type type)
        {
            if (type.GetTypeInfo().IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
