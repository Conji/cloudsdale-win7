﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Cloudsdale_Win7.MVVM {
    public class IsSlashMeVis : IValueConverter {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter != null && parameter.ToString() == "Inverse") {
                return value.ToString().StartsWith("/me") ? Visibility.Collapsed : Visibility.Visible;
            }
            return value.ToString().StartsWith("/me") ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
