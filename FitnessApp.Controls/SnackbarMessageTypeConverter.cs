﻿using System.ComponentModel;
using System.Globalization;

namespace FitnessApp.Controls;

public class SnackbarMessageTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string s)
        {
            return new SnackbarMessage
            {
                Content = s
            };
        }

        return base.ConvertFrom(context, culture, value);
    }
}