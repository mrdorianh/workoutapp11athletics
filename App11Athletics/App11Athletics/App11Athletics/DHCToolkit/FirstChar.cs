﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11Athletics.DHCToolkit
{
    public class FirstChar : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var str = (TimeSpan)value;
            string time = null;
            switch (parameter.ToString())
            {
                case "ff":
                    time = str.ToString("ff");
                    break;
                case "ss":
                    time = str.ToString("ss");
                    break;
                case "mm":
                    time = str.ToString("mm");
                    break;
                case "hh":
                    time = str.ToString("hh");
                    break;
            }
            //            var second = str?.Substring(1, 1);
            return time?.Substring(0, 1);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
