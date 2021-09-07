using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace App1.Controls
{
    public class DecimalConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0";
            decimal thedecimal = (decimal)value;
            return NumberHelper.DecimalConverter(thedecimal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            decimal decValue = 0;
            (decValue, strValue) = NumberHelper.DecimalConverter(strValue);

            if (parameter is Entry entry)
            {
                entry.Unfocused -= Unfocused_Event;
                entry.Unfocused += Unfocused_Event;
            }

            System.Diagnostics.Debug.WriteLine("ConvertBack");

            return decValue;
        }

        private void Unfocused_Event(object sender, FocusEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Unfocused_Event");
            if (sender is Entry entry)
            {
                string strValue = entry.Text;
                decimal decValue = 0;
                (decValue, strValue) = NumberHelper.DecimalConverter(strValue);
                entry.Text = strValue;
            }
        }
    }
}
