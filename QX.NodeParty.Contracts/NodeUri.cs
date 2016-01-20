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

    public string NodeId { get; private set; }

    public bool IsNodeIdUri
    {
      get { return (IsAbsoluteUri && Segments.Length > 1 ) || !IsAbsoluteUri; }
    }

    public NodeUri(NodeUri nodeUri, string nodeId)
      : base(nodeUri, nodeId)
    {
      if (string.IsNullOrWhiteSpace(nodeId))
      {
        throw new ArgumentNullException(nameof(nodeId));
      }

      NodeId = nodeId;
    }

    public NodeUri(Uri uri) : this(uri.ToString(), UriKind.RelativeOrAbsolute)
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

        NodeId = Segments.Skip(IsAbsoluteUri ? 2 : 0).LastOrDefault() ?? string.Empty;
      }
      else
      {
        NodeId = ToString();
      }
    }

    public NodeUri MakeRelativeUri(NodeUri uri)
    {
      return new NodeUri(base.MakeRelativeUri(uri));
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
