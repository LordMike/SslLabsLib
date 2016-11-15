using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SslLabsLib.Tests.Helpers
{
    internal static class TestHelpers
    {
        public static void EnsureAllPropertiesSet(object obj, params string[] ignoreFields)
        {
            Type objType = obj.GetType();
            PropertyInfo[] props = objType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                if (ignoreFields.Contains(prop.Name))
                    continue;

                if (prop.PropertyType.IsValueType)
                {

                }
                else
                {
                    // Must not be null
                    object value = prop.GetValue(obj, null);
                    Assert.IsNotNull(value, $"Field {prop.Name} of {objType.Name} was null");
                }
            }
        }
    }
}
