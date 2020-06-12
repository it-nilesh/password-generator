using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PasswordGenerator
{
    public static class PasswordPolicyExtensions
    {
        public static T AutoSetPassword<T>(this IPasswordPolicy policy, T @object, Expression<Func<T, string>> propertyAccessor)
        {
            var password = policy.Generate();
            Expression<Action<T, string>> assigner = SetPropertyValue(propertyAccessor);
            assigner.Compile()(@object, password.GetPassword());

            return @object;
        }

        public static List<T> AutoSetPassword<T>(this IPasswordPolicy policy, List<T> @objects, Expression<Func<T, string>> propertyAccessor)
        {
            foreach (var @object in @objects)
            {
                policy.AutoSetPassword<T>(@object, propertyAccessor);
            }

            return @objects;
        }

        public static T RenderPassword<T>(this IPasswordPolicy policy, T @object, Action<T, RandomSecurePassword> result)
        {
            result(@object, policy.Generate());
            return @object;
        }

        public static List<T> RenderPassword<T>(this IPasswordPolicy policy, List<T> @objects, Action<T, RandomSecurePassword> result)
        {
            foreach (var @object in @objects)
            {
                policy.RenderPassword<T>(@object, result);
            }

            return @objects;
        }

        public static string RenderUniquePassword(this IPasswordPolicy policy, Func<string, bool> result, int maxAttempt = 3)
        {
            bool status;
            string finalPassword;
            do
            {
                RandomSecurePassword securePassword = policy.Generate();
                finalPassword = securePassword.GetPassword();
                status = result(finalPassword);
                maxAttempt--;
            } while (status && maxAttempt > 0);

            return status == false ? finalPassword : string.Empty;
        }

        private static Expression<Action<T, R>> SetPropertyValue<T, R>(Expression<Func<T, R>> propertyAccessor)
        {
            var prop = ((MemberExpression)propertyAccessor.Body).Member;
            var typeParam = Expression.Parameter(typeof(T));
            var valueParam = Expression.Parameter(typeof(R));

            var assigner = Expression.Lambda<Action<T, R>>(
                Expression.Assign(Expression.MakeMemberAccess(typeParam, prop), valueParam), typeParam, valueParam);

            return assigner;
        }
    }
}
