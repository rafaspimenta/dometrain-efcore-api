using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Globalization;

namespace Dometrain.EFCore.API.Data.ValueConverts;

public class DataTimeToChar8Converter : ValueConverter<DateTime, string>
{
    public DataTimeToChar8Converter() :base(
        dateTime => dateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture),
        stringValue => DateTime.ParseExact(stringValue, "yyyyMMdd", CultureInfo.InvariantCulture))
    {
    }
}
