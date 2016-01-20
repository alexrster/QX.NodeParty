using System;
using System.Diagnostics;
using System.Threading;

namespace QX.NodeParty.Composition.Base
{
  public class Node : INode
  {
    public NodeUri NodeUri { get; private set; }

    public Node(NodeUri nodeUri)
    {
      if (nodeUri == null)
      {
        throw new ArgumentNullException("nodeUri");
      }

      NodeUri = nodeUri;
    }

    public static Mutex AcquireMutex(NodeUri nodeUri, TimeSpan timeout)
    {
      Debug.Print("Acquire Mutex for Node URI '{0}'", nodeUri);
      var mutexName = GetMutexName(nodeUri);

      Debug.Print("Acquire Mutex name: '{0}'", mutexName);
      bool mutexCreated;
      var mutex = new Mutex(false, mutexName, out mutexCreated);

      Debug.Print("Mutex instance resolved - instance created: {0}", mutexCreated);
      try
      {
        Debug.Print("Try to aquire lock on mutex using timeout {0}", timeout);
        if (!mutex.WaitOne(timeout))
        {
          throw new TimeoutException(string.Format("Cannot acquire Mutex for URI '{0}' - there is another Node already exists for the same URI", nodeUri));
        }

        Debug.Print("Mutex successfully acquired!");
        return mutex;
      }
      catch (AbandonedMutexException abandonedMutexException)
      {
        Debug.Print("Mutex Abandoned Exception: {0}", abandonedMutexException.Message);

        mutex.ReleaseMutex();
        return AcquireMutex(nodeUri, timeout);
      }
    }

    public static string GetMutexName(NodeUri nodeUri)
    {
      if (nodeUri.IsAbsoluteUri) return nodeUri.AbsoluteUri;

      return new NodeUri(NodeUri.CreateLocalNodeUri(), nodeUri.LocalPath).AbsoluteUri;
    }
  }
}
