using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExifAnalyzer.Common.Helpers
{
    public class EnumHelper
    {
        /// <summary>
        /// Get the Description of a Enum
        /// </summary>
        /// <param name="value">The Enum value of which you want the description</param>
        public static string GetEnumDescription(object value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi == null)
            {
                return value.ToString();
            }
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        /// <summary>
        /// Gets all descriptions for the enum type.
        /// </summary>
        /// <param name="enumType">The enum type to get the descriptions for.</param>
        /// <returns>An array with all descriptions.</returns>
        public static string[] GetAllEnumDescriptions(Type enumType)
        {
            List<string> descriptions = new List<string>();
            foreach (var value in Enum.GetValues(enumType))
            {
                descriptions.Add(GetEnumDescription(value));
            }
            return descriptions.ToArray();
        }
    }
}
