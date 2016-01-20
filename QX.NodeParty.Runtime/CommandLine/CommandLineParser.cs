using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace QX.NodeParty.Runtime.CommandLine
{
  public static class CommandLineParser
  {
    public static readonly string[] Prefixes = {"/", "--"};
    public static readonly string[] Delimiters = {"=", ":"};
    public static readonly Regex ArgumentParseRegex;

    static CommandLineParser()
    {
      var prefixPatterns = new []
      {
        FormatIfNotEmpty(string.Concat(Prefixes.Where(x => x.Length == 1).Where(x => !string.IsNullOrEmpty(x)).Select(Regex.Escape))),
        string.Join("|", Prefixes.Where(x => x.Length > 1).Select(Regex.Escape).Select(x => string.Format("({0})", x)))
      }.Where(x => !string.IsNullOrWhiteSpace(x));

      var delimiterPatterns = new[]
      {
        FormatIfNotEmpty(string.Concat(Delimiters.Where(x => x.Length == 1).Where(x => !string.IsNullOrEmpty(x)).Select(Regex.Escape))),
        string.Join("|", Delimiters.Where(x => x.Length > 1).Select(Regex.Escape).Select(x => string.Format("({0})", x)))
      }.Where(x => !string.IsNullOrWhiteSpace(x));

      ArgumentParseRegex = new Regex(string.Format(
        @"^(((?:{0}){{1}}(?<key>(?=[\w]+)[\w\d\s\-+.]+))|(?<key>(?=([\-\\+_/])+)[\w\d\s\-+]+))(?:{1}{{1}}(?<value>.+))?$",
        string.Join("|", prefixPatterns),
        string.Join("|", delimiterPatterns)
        ));
    }

    private static string FormatIfNotEmpty(string str)
    {
      return !string.IsNullOrEmpty(str) ? string.Format("[{0}]", str) : string.Empty;
    }

    public static TModel Parse<TModel>(string[] args)
      where TModel : class, new()
    {
      var arguments = new NameValueCollection(StringComparer.InvariantCulture);
      foreach (var arg in args)
      {
        var match = ArgumentParseRegex.Match(arg);
        if (match.Success)
        {
          arguments.Add(match.Groups["key"].Value, match.Groups["value"].Success ? match.Groups["value"].Value : string.Empty);
        }
        else
        {
          Debug.Print("WARN: Cannot parse argument '{0}'", arg);
        }
      }

      return ModelCache<TModel>.Create(arguments);
    }

    public static void PrintUsage<TModel>(TextWriter @out)
      where TModel : class, new()
    {
      ModelCache<TModel>.PrintUsage(@out);
    }

    private static class ModelCache<TModel>
      where TModel : class, new()
    {
      private static readonly IEnumerable<CommandLineArgumentBinding> Bindings;

      static ModelCache()
      {
        Debug.Print("Loading settings type '{0}'", typeof(TModel).FullName);
        Debug.Indent();

        Bindings = typeof (TModel).GetProperties()
          .Select(x => x.CreateBinding())
          .Where(x => x != null)
          .ToArray();

        Debug.Unindent();
        Debug.Print("Successfully loaded settings type '{0}'", typeof(TModel).FullName);
      }

      public static TModel Create(NameValueCollection arguments)
      {
        return Bindings.Aggregate(new TModel(), (model, binding) => binding.Bind(model, arguments));
      }

      public static void PrintUsage(TextWriter @out)
      {
        var appName = AppDomain.CurrentDomain.FriendlyName;

        @out.WriteLine();
        @out.WriteLine("Usage:\r\n\t{0} <parameters>", appName);
        @out.WriteLine();

        @out.WriteLine("Parameters:");
        foreach (var item in Bindings.Where(x => !x.ArgumentInfo.IsPrivate))
        {
          var names = Prefixes.Select(x => x + item.ArgumentInfo.Name).Union(item.ArgumentInfo.SkipPrefix ? item.ArgumentInfo.Aliases : item.ArgumentInfo.Aliases.SelectMany(x => Prefixes.Select(p => p + x)));
          var values = !item.ArgumentInfo.IsSwitch ? Delimiters.Select(x => x + item.ArgumentInfo.Name) : new[] { string.Empty };
          var usages = names.SelectMany(x => values.Select(v => string.Format(item.ArgumentInfo.IsRequired ? @"<{0}>" : @"[{0}]", x + v)));

          @out.WriteLine("\t{0} - {1}:", item.ArgumentInfo.Name, item.ArgumentInfo.IsRequired ? "required" : "optional");
          @out.WriteLine("\t\tUsage:       ({0})", string.Join(" | ", usages));
          @out.WriteLine("\t\tDescription: {0}", item.ArgumentInfo.Description);
          @out.WriteLine();
        }

        @out.WriteLine();
        @out.WriteLine(@"
---
Example 1: To run Process process for all organizations configured in file 'Configuration\organizations.xml' run the following command:
           
           > {0} -process=ImportDailyTradingValuesProcess -orgs=Configuration\organizations.xml

---
Example 2: To run Process process only for organizations A and B configured in file 'Configuration\organizations.xml', run the following command:
           
           > {0} /P:Process /O:Configuration\organizations.xml +o:A +o:B

---
Example 3: To run Process process for all organizations except Test and Dev configured in file 'Configuration\organizations.xml', run the following command:
           
           > {0} /P:Process /O:Configuration\organizations.xml -o:Test -o:Dev


", appName);
      }
    }
  }
}
