using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDocument.ViewModels
{
    public class FilePathToImageConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string format = (string)value;

            switch (format)
            {
                case ".rtf":
                    return new Uri("../Resources/rtf.png", UriKind.Relative);
                case ".txt":
                    return new Uri("../Resources/text.png", UriKind.Relative);
                case ".odt":
                    return new Uri("../Resources/odt.png", UriKind.Relative);
                case ".doc":
                    return new Uri("../Resources/doc.png", UriKind.Relative);
                case ".exe":
                    return new Uri("../Resources/exe.png", UriKind.Relative);
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StringToFlowDocumentConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new StringToFlowDocumentConverter();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
