using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace QX.NodeParty.Runtime.CommandLine
{
  [AttributeUsage(AttributeTargets.Property)]
  public abstract class CommandLineArgumentAttribute : Attribute
  {
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string[] Aliases { get; set; }
    public string DefaultValue { get; set; }
    public bool IsRequired { get; set; }
    public bool SkipPrefix { get; set; }
    public bool IsSwitch { get; set; }
    public bool IsPrivate { get; set; }

    public CommandLineArgumentAttribute(string name, string description)
    {
      Name = name;
      Description = description;
      Aliases = new string[0];
    }

    public IEnumerable<string> GetValues(NameValueCollection settingsCollection)
    {
      var result = (new[] { Name })
        .Union(Aliases)
        .Select(settingsCollection.GetValues)
        .Where(i => i != null)
        .SelectMany(x => x)
        .Where(i => i != null)
        .Distinct()
        .DefaultIfEmpty(DefaultValue);

      //return IsRequired ? result.First(x => x != null) : result.FirstOrDefault();
      return result;
    }

    public abstract CommandLineArgumentBinding CreateArgumentBinding(PropertyInfo propertyInfo);
  }
}
