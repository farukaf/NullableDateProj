using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace App1.Controls
{
    public static class NumberHelper
    {
        public static CultureInfo DefaultCultureInfo()
        {
            return new CultureInfo("pt-BR");
        }

        public static decimal StringToDecimalConverter(string valueString)
        {
            if (string.IsNullOrEmpty(valueString))
            {
                return 0;
            }
            valueString = Regex.Replace(valueString, @"[^0-9,.]", "");

            var indx = valueString.IndexOf(',');
            valueString = valueString.Replace(",", "");
            if (indx > 0 && indx < valueString.Length)
            {
                valueString = valueString.Insert(indx, ",");
            }

            decimal _value;
            if (!decimal.TryParse(valueString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands, DefaultCultureInfo(), out _value))
                _value = 0m;

            return _value;
        }

        public static (decimal decimalValue, string stringValue) CurrencyConverter(string valueString)
        {
            var _value = StringToDecimalConverter(valueString);
            var stringValue = CurrencyConverter(_value);

            _value = StringToDecimalConverter(stringValue);

            return (_value, stringValue);
        }

        /// <summary>
        /// Vai converter o valor decimal em String Monetario. Com o Formato definido pelo DefaultCultureInfo <para/> 
        /// </summary>
        /// <param name="valueDeci"></param>
        /// <returns></returns>
        public static string CurrencyConverter(decimal valueDeci)
        {
            NumberFormatInfo nfi = DefaultCultureInfo().NumberFormat;

            var format = "N2";
            var stringValue = valueDeci.ToString(format, nfi);

            return stringValue;
        }

        /// <summary>
        /// A partir do parametro retorna um valor Decimal e a string formatada.     
        /// </summary>
        /// <param name="valueString"></param>
        /// <returns></returns>
        public static (decimal decimalValue, string stringValue) DecimalConverter(string valueString)
        {
            var _value = StringToDecimalConverter(valueString);
            var stringValue = DecimalConverter(_value);

            return (_value, stringValue);
        }

        /// <summary>
        /// Vai converter o valor decimal em String. <para/>
        /// Com um minimo de 2 casas decimais ou nenhuma (um valor sem casas decimais vai ser mostrado como inteiro)
        /// </summary>
        /// <param name="valueDeci"></param>
        /// <returns></returns>
        public static string DecimalConverter(decimal valueDeci)
        {
            NumberFormatInfo nfi = DefaultCultureInfo().NumberFormat;

            var format = "N";
            var stringValue = valueDeci.ToString(nfi);

            if (stringValue.Contains(","))
            {
                var afterComma = stringValue.Split(',')[1];

                while (!string.IsNullOrEmpty(afterComma) && afterComma.EndsWith("0"))
                {
                    afterComma = afterComma.Substring(0, afterComma.Length - 1);
                }

                var decLen = afterComma.Length;

                if (decLen > 2) format = "N" + decLen;
                else format = "N2";

                stringValue = valueDeci.ToString(format, nfi);
            }

            return stringValue;
        }


    }
}
