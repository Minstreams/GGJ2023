using UnityEngine;

namespace IceEngine.Internal
{
    public class SettingGameplay : Framework.IceSetting<SettingGameplay>
    {
        #region ThemeColor
        public override Color DefaultThemeColor => new(0.07464188f, 0.1167115f, 0.214792f);
        #endregion
    }
}