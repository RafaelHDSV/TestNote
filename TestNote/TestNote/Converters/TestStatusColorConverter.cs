using System.Globalization;
using Microsoft.Maui.Controls;

namespace TestNote.Converters
{
    public class TestStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string status)
            {
                return status.ToLower() switch
                {
                    "concluído" or "concluido" or "aprovado" => Colors.Green,
                    "reprovado" => Colors.Red,
                    "em andamento" => Colors.Orange,
                    "pendente" => Colors.Gray,
                    _ => Colors.Black
                };
            }
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}