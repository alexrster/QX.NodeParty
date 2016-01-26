using System;
using System.ComponentModel;
using System.Linq;

namespace QX.NodeParty
{
  [TypeConverter(typeof(NodeUriTypeConverter))]
  public sealed class NodeUri : Uri
  {
    public const string UriScheme = @"urn-qx";
    public static readonly NodeUri LocalHost = new NodeUri($"{UriScheme}{SchemeDelimiter}.", UriKind.Absolute);

    public NodeUri(Uri uri) : this(uri.ToString(), UriKind.RelativeOrAbsolute)
    { }

    public NodeUri(NodeUri nodeUri, string nodeId) : base(nodeUri, nodeId)
    { }

    public NodeUri(string uriString, UriKind uriKind) : base(uriString, uriKind)
    {
      if (IsAbsoluteUri)
      {
        if (Segments.Length == 0)
        {
          throw new UriFormatException("Node URI format invalid");
        }

        if (!UriScheme.Equals(Scheme, StringComparison.InvariantCultureIgnoreCase))
        {
          throw new UriFormatException("Node URI scheme invalid");
        }
      }
    }

    public static NodeUri CreateLocalNodeUri(string localPath = null)
    {
      var localhost = new NodeUri($"{UriScheme}{SchemeDelimiter}./", UriKind.Absolute);
      return localPath == null ? localhost : new NodeUri(localhost, localPath);
    }

    public static implicit operator NodeUri (string uri)
    {
      return new NodeUri(uri, UriKind.RelativeOrAbsolute);
    }
  }
}
