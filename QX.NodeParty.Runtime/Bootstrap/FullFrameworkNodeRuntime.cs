using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using QX.NodeParty.Composition.Bootstrap;

namespace QX.NodeParty.Runtime.Bootstrap
{
  public sealed class FullFrameworkNodeRuntime : INodeRuntime
  {
    private readonly IObjectFactoryProvider _objectFactoryProvider;

    public TextWriter Out { get; set; }
    public TextWriter Error { get; set; }

    private FullFrameworkNodeRuntime(AppDomain appDomain, IObjectFactoryProvider objectFactoryProvider)
    {
      Debug.IndentSize = 2;
      Debug.Print("Initialize Node Runtime Environment");
      _objectFactoryProvider = new CachedObjectFactoryProvider(objectFactoryProvider);

      appDomain.AssemblyLoad += AppDomainAssemblyLoad;
      appDomain.AssemblyResolve += AppDomainAssemblyResolve;
      appDomain.DomainUnload += AppDomainUnload;
      appDomain.FirstChanceException += AppDomainFirstChanceException;
      appDomain.ProcessExit += AppDomainProcessExit;
      appDomain.ReflectionOnlyAssemblyResolve += AppDomainReflectionOnlyAssemblyResolve;
      appDomain.ResourceResolve += AppDomainResourceResolve;
      appDomain.TypeResolve += AppDomainTypeResolve;
      appDomain.UnhandledException += AppDomainUnhandledException;
    }

    public static INodeRuntime CreateFromCurrentAppDomain()
    {
      Debug.Print("Create Full Framework Node Runtime using current AppDomain");
      return new FullFrameworkNodeRuntime(AppDomain.CurrentDomain, new FullFrameworkObjectFactoryProvider());
    }

    public object CreateObject(string objectFactoryId)
    {
      Debug.Print("Get object factory by ID '{0}'", objectFactoryId);
      var factory = _objectFactoryProvider.GetObjectFactory(objectFactoryId);

      Debug.Print("Create new object instance using factory '{0}'", factory);
      return factory();
    }

    private void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      Debug.IndentLevel = 0;
      Debug.Fail(string.Format("Application Domain: unhandled exception occured in '{0}'", sender), e.ExceptionObject.ToString());
    }

    private Assembly AppDomainTypeResolve(object sender, ResolveEventArgs args)
    {
      Debug.Print("Application Domain: default type resolver has been failed to resolve type - trying to resolve type '{0}' from assembly '{1}' requested by '{2}' manually", args.Name, args.RequestingAssembly, sender);
      return Assembly.LoadFrom(args.Name);
    }

    private Assembly AppDomainResourceResolve(object sender, ResolveEventArgs args)
    {
      Debug.Print("Application Domain: default resource resolver has been failed to resolve resource - trying to resolve resource '{0}' from assembly '{1}' requested by '{2}' manually", args.Name, args.RequestingAssembly, sender);
      return Assembly.LoadFrom(args.Name);
    }

    private Assembly AppDomainAssemblyResolve(object sender, ResolveEventArgs args)
    {
      Debug.Print("Application Domain: default assembly resolver has been failed to resolve assembly - trying to resolve assembly '{0}' from assembly '{1}' requested by '{2}' manually", args.Name, args.RequestingAssembly, sender);
      return Assembly.LoadFrom(args.Name);
    }

    private Assembly AppDomainReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
    {
      Debug.Print("Application Domain: default reflection only assembly resolver has been failed to resolve assembly - trying to resolve assembly '{0}' from assembly '{1}' requested by '{2}' manually", args.Name, args.RequestingAssembly, sender);
      return Assembly.ReflectionOnlyLoadFrom(args.Name);
    }

    private void AppDomainFirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
    {
      Debug.IndentLevel = 0;
      Debug.Fail(string.Format("Application Domain: first chance exception occured in '{0}'", sender), e.Exception.ToString());
    }

    private void AppDomainProcessExit(object sender, EventArgs e)
    {
      Debug.Print("Hosting process terminating");
    }

    private void AppDomainUnload(object sender, EventArgs e)
    {
      Debug.Print("Application Domain terminating");
    }

    private void AppDomainAssemblyLoad(object sender, AssemblyLoadEventArgs args)
    {
      Debug.Print("Application Domaing: loaded assembly '{0}'", args.LoadedAssembly.FullName);
    }

    public void Dispose()
    { }
  }
}
