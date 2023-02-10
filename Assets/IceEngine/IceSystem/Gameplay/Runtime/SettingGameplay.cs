using UnityEngine;

namespace IceEngine.Internal
{
    public class SettingGameplay : Framework.IceSetting<SettingGameplay>
    {
        #region ThemeColor
        public override Color DefaultThemeColor => new(0.07464188f, 0.1167115f, 0.214792f);
        #endregion

        public Vector2Int mapSize = new Vector2Int(64, 64);
        public int initMoney = 1000;

        public GameObject prefabScv;
        public GameObject prefabGrass;
    }
}