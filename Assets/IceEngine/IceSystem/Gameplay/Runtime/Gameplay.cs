using IceEngine;

namespace Ice
{
    public sealed class Gameplay : IceEngine.Framework.IceSystem<IceEngine.Internal.SettingGameplay>
    {
        #region 玩家数据
        // TODO: 存档问题
        #endregion

        public static GMap map;

        #region 选择
        public static Selectable SelectedObject { get; set; }
        public static void SelectObject(Selectable obj)
        {
            SelectedObject = obj;
            PanelSelectedObject.UpdateContent();
        }
        #endregion
    }
}