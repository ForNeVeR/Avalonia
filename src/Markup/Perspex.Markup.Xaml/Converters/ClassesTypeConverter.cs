// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Globalization;
using OmniXaml.TypeConversion;
using Perspex.Styling;

namespace Perspex.Markup.Xaml.Converters
{
    public class ClassesTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(IXamlTypeConverterContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public bool CanConvertTo(IXamlTypeConverterContext context, Type destinationType)
        {
            return false;
        }

        public object ConvertFrom(IXamlTypeConverterContext context, CultureInfo culture, object value)
        {
            return new Classes(((string)value).Split(' '));
        }

        public object ConvertTo(IXamlTypeConverterContext context, CultureInfo culture, object value, Type destinationType)
        {
            throw new NotImplementedException();
        }
    }
}