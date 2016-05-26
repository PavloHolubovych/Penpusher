using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Penpusher.Test
{
    public class PropertiesEqualityComparer<T> : IEqualityComparer<T>
        where T : class
    {
        public bool Equals(T x, T y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (y == null || x == null)
            {
                return false;
            }

            return CompareProperties(x, y);
        }

        public int GetHashCode(T obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return GenerateHashCode(obj);
        }

        protected static int GetHashCodeFromValues(params object[] values)
        {
            return GetHashCodeFromHashCodes(values.Where(value => value != null).Select(value => value.GetHashCode()).ToArray());
        }

        protected static int GetHashCodeFromHashCodes(params int[] hashCodes)
        {
            return hashCodes.Aggregate(17, (current, value) => (current * 59) + value);
        }

        protected virtual int GenerateHashCode(T obj)
        {
            IEnumerable<PropertyInfo> properties = GetPublicProperties(obj);
            return GetHashCodeFromValues(properties.Select(property => property.GetValue(obj)));
        }

        protected virtual bool CompareProperties(T x, T y)
        {
            return ArePublicPropertiesEquals(x, y);
        }

        private static bool ArePublicPropertiesEquals(T x, T y)
        {
            PropertyInfo[] properties = GetPublicProperties(x);

            foreach (PropertyInfo property in properties)
            {
                object valueX = property.GetValue(y);
                object valueY = property.GetValue(x);

                if (valueX == null && valueY == null)
                {
                    continue;
                }

                if ((valueX == null || valueY == null) || !valueX.Equals(valueY))
                {
                    return false;
                }
            }

            return true;
        }

        private static PropertyInfo[] GetPublicProperties(T x)
        {
            return x == null ? new PropertyInfo[0] : x.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }
    }
}