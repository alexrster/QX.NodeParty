using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using QX.NodeParty.Runtime.CommandLine;

namespace QX.NodeParty.Runtime.Bootstrap
{
  public class NodeHostSettings
  {
    [StringSingleArg("TypeLoader", "Type resolver class fully qualified type name", Aliases = new[] { "tr" })]
    public string TypeLoader { get; set; }

    [StringSingleArg("TypeActivator", "Type activator class fully qualified type name", Aliases = new[] { "ta", "activator" })]
    public string TypeActivator { get; set; }

    [StringSingleArg("ConfigLoader", "Config Loader class fully qualified type name", Aliases = new[] { "cl" })]
    public string ConfigurationLoader { get; set; }

    [StringSingleArg("InstanceId", "Instance ID of the root Node", Aliases = new[] { "i", "id" }, DefaultValue = "lxcrm")]
    public string InstanceId { get; set; }

    [StringMultiArg("ConfigUri", "Configuration files URIs", Aliases = new[] { "c", "config", "cfg" })]
    public IEnumerable<string> ConfigLocations { get; set; }

    private static void DumpEnvironmentSettings(NameValueCollection environment)
    {
      Debug.Print("Begin Dump environment settings:");
      Debug.Indent();

      foreach (var key in environment.AllKeys)
      {
        var values = environment.GetValues(key);
        if (values.Length == 0)
        {
          continue;
        }
        if (values.Length == 1)
        {
          Debug.Print("{0}: {1}", key, values[0]);
        }
        else
        {
          Debug.Print("{0}:", key);

          Debug.Indent();
          foreach (var value in values)
          {
            Debug.Print(value);
          }
          Debug.Unindent();
          Debug.Print(String.Empty);
        }
      }
      Debug.Unindent();
      Debug.Print("End Dump environment settings");
    }
  }
}
