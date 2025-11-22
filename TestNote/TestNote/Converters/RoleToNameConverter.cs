using System.Globalization;
using Microsoft.Maui.Controls;

namespace TestNote.Converters
{
    public class RoleToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int role)
            {
                return role switch
                {
                    1 => "Administrador",
                    2 => "Gerente",
                    3 => "Funcionário",
                    _ => "Desconhecido",
                };
            }
            return "Desconhecido";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}