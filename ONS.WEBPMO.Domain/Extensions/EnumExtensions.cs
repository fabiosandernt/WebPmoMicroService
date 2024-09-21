
using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string ToDescription(this Enum value)
    {
        if (value == null) return null;

        FieldInfo field = value.GetType().GetField(value.ToString());
        if (field != null)
        {
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute != null ? attribute.Description : value.ToString();
        }

        return value.ToString();
    }
}
