using System;
using System.Collections.Generic;

namespace QX.NodeParty.Config
{
  public abstract class ConfigurationData
  {
    public static readonly ConfigurationData Empty = new EmptyConfiguration();

    public abstract ConfigurationData this[string key] { get; }
    public abstract T GetValue<T>();

    public static implicit operator string(ConfigurationData configurationData)
    {
      return configurationData.GetValue<string>();
    }

    public static implicit operator byte (ConfigurationData configurationData)
    {
      return configurationData.GetValue<byte>();
    }

    public static implicit operator int (ConfigurationData configurationData)
    {
      return configurationData.GetValue<int>();
    }

    public static implicit operator short (ConfigurationData configurationData)
    {
      return configurationData.GetValue<short>();
    }

    public static implicit operator long (ConfigurationData configurationData)
    {
      return configurationData.GetValue<long>();
    }

    public static implicit operator float (ConfigurationData configurationData)
    {
      return configurationData.GetValue<float>();
    }

    public static implicit operator decimal (ConfigurationData configurationData)
    {
      return configurationData.GetValue<decimal>();
    }

    public static implicit operator bool (ConfigurationData configurationData)
    {
      return configurationData.GetValue<bool>();
    }

    public static implicit operator TimeSpan (ConfigurationData configurationData)
    {
      return configurationData.GetValue<TimeSpan>();
    }

    public static implicit operator Guid(ConfigurationData configurationData)
    {
      return configurationData.GetValue<Guid>();
    }

    public static implicit operator byte? (ConfigurationData configurationData)
    {
      return configurationData.GetValue<byte?>();
    }

    public static implicit operator int? (ConfigurationData configurationData)
    {
      return configurationData.GetValue<int?>();
    }

    public static implicit operator short? (ConfigurationData configurationData)
    {
      return configurationData.GetValue<short?>();
    }

    public static implicit operator long? (ConfigurationData configurationData)
    {
      return configurationData.GetValue<long?>();
    }

    public static implicit operator float? (ConfigurationData configurationData)
    {
      return configurationData.GetValue<float?>();
    }

    public static implicit operator decimal? (ConfigurationData configurationData)
    {
      return configurationData.GetValue<decimal?>();
    }

    public static implicit operator bool? (ConfigurationData configurationData)
    {
      return configurationData.GetValue<bool?>();
    }

    public static implicit operator TimeSpan? (ConfigurationData configurationData)
    {
      return configurationData.GetValue<TimeSpan?>();
    }

    public static implicit operator Guid?(ConfigurationData configurationData)
    {
      return configurationData.GetValue<Guid?>();
    }

    private class EmptyConfiguration : ConfigurationData
    {
      private static readonly IDictionary<string, ConfigurationData> EmptyConfig = new Dictionary<string, ConfigurationData>(0);

      public override ConfigurationData this[string key]
      {
        get { return this; }
      }

      public override T GetValue<T>()
      {
        return default(T);
      }
    }
  }
}
