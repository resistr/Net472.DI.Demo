using System;
using System.Runtime.InteropServices;

namespace Common
{
    public static class TypeExtensions
    {
        public static bool TryCreateType<T>(out T createdInstance)
            => TryCreateType(Type.EmptyTypes, null, out createdInstance);

        public static bool TryCreateType<T>(Type[] parameterTypes, object[] parameters, out T createdInstance)
            => typeof(T).TryCreateType(parameterTypes, parameters, out createdInstance);

        public static bool TryCreateType<T>(this _Type type, out T createdInstance)
            => type.TryCreateType(Type.EmptyTypes, null, out createdInstance);

        public static bool TryCreateType<T>(this _Type type, Type[] parameterTypes, object[] parameters, out T createdInstance)
        {
            if (type.TryCreateType(parameterTypes, parameters, out object instance) && instance is T typedInstance)
            {
                createdInstance = typedInstance;
                return true;
            }
            createdInstance = default;
            return false;
        }

        public static bool TryCreateType(this _Type type, out object createdInstance)
            => type.TryCreateType(Type.EmptyTypes, null, out createdInstance);

        public static bool TryCreateType(this _Type type, Type[] types, object[] parameters, out object createdInstance)
        {
            if (type == null 
                || (types != null && types.Rank != 1)
                || (parameters != null && parameters.Rank != 1 && parameters.Length != types.Length))
            {
                createdInstance = null;
                return false;
            }

            try
            {
                var cstor = type.GetConstructor(types);
                if (cstor == null)
                {
                    createdInstance = default;
                    return false;
                }
                createdInstance = cstor.Invoke(parameters);
                return true;
            }
            catch (Exception ex)
            when (
                ex is MemberAccessException ||
                ex is MethodAccessException ||
                ex is ArgumentException ||
                ex is ArgumentNullException ||
                ex is System.Reflection.TargetInvocationException ||
                ex is System.Reflection.TargetParameterCountException ||
                ex is NotSupportedException ||
                ex is System.Security.SecurityException
            )
            {
                createdInstance = null;
                return false;
            }
        }
    }
}
