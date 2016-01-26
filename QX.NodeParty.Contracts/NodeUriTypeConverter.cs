using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace QX.NodeParty
{
  public class NodeUriTypeConverter : UriTypeConverter
  {
    private static string TypeName = typeof(NodeUri).FullName;

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value == null)
      {
        throw new ArgumentNullException(nameof(value), $"Cannot convert null to '{TypeName}'");
      }

      var str = value as string;
      if (str != null)
      {
        return new NodeUri(str, UriKind.RelativeOrAbsolute);
      }
      else if (value is Uri)
      {
        return new NodeUri((Uri) value);
      }

      throw new ArgumentNullException(nameof(value), $"Cannot convert null to '{TypeName}'");
    }

    public override bool IsValid(ITypeDescriptorContext context, object value)
    {
      return (value as string ?? string.Empty).StartsWith(NodeUri.UriScheme + Uri.SchemeDelimiter) && base.IsValid(context, value);
    }
  }
}
