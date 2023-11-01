using System.ComponentModel;
using System.Reflection;

namespace Domain.Enumerator
{
    public static class Enumerator
    {
        public static string ReturnDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            if (field.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }

        public static T ReturnValue<T>(string description) where T : Enum
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            throw new ArgumentException($"Valor de enum com a descrição '{description}' não encontrado em { typeof(T).Name }");
        }
    }
}
