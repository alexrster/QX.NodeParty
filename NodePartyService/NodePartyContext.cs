using System;
using System.Fabric;
using QX.NodeParty.Composition.Bootstrap;

namespace QX.NodeParty.Host.ServiceFabric
{
  internal sealed class NodePartyContext : ServiceContext, INodeRuntime
  {
    public NodePartyContext(NodeContext nodeContext, ICodePackageActivationContext codePackageActivationContext,
      string serviceTypeName, Uri serviceName, byte[] initializationData, Guid partitionId, long replicaOrInstanceId)
      : base(nodeContext, codePackageActivationContext, serviceTypeName, serviceName, initializationData, partitionId, replicaOrInstanceId)
    { }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public object CreateObject(string objectFactoryId)
    {
      throw new NotImplementedException();
    }
  }
}