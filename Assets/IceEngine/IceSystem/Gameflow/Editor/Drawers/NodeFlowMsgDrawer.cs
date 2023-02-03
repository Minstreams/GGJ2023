using UnityEngine;

using IceEngine;
using IceEngine.IceprintNodes;
using static IceEditor.IceGUI;

namespace IceEditor.Internal
{
    internal class NodeFlowMsgDrawer : Framework.IceprintNodeDrawer<NodeFlowMsg>
    {
        public override void OnGUI_Body(NodeFlowMsg node, Rect rect)
        {
            using (Area(rect))
            {
                TextField(ref node.msg);
            }
        }
    }
}