using IceEngine.Framework;
using Sys = Ice.Gameflow;

namespace IceEngine.IceprintNodes
{
    [IceprintMenuItem("Ice/Gameflow/FlowMsg")]
    public class NodeFlowMsg : IceprintNode
    {
        public string msg;
        // Ports
        [IceprintPort] public void SendMsg() => Sys.SendMsg(msg);
        [IceprintPort] public void SendMsg(string msg) => Sys.SendMsg(msg);
    }
}