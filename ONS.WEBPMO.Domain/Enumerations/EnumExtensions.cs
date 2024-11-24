using System.ComponentModel;
using System.Reflection;

namespace ONS.WEBPMO.Domain.Enumerations
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum value) where TEnum : struct, System.Enum
        {
            // Verificar se o tipo é realmente um enum
            var type = typeof(TEnum);
            if (!type.IsEnum)
            {
                throw new ArgumentException("O tipo fornecido deve ser um enum.");
            }

            // Obter o campo do enum
            var field = type.GetField(value.ToString());
            if (field == null)
            {
                return value.ToString(); // Retorna o nome se não houver descrição
            }

            // Buscar o atributo Description
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}
