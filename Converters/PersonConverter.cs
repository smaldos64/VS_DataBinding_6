using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using DataBinding_6.Models;

namespace DataBinding_6.Converters
{
    [ValueConversion(typeof(Person), typeof(string))]
    public class PersonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Person Person_Object = (Person)value;
            return (Person_Object.ID.ToString() + " : " + Person_Object.Fornavn);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
            //      return value;
        }
    }
}
