using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IceEngine
{
    /// <summary>
    /// 资源
    /// </summary>
    public class Source : Selectable
    {
        [Group("Source")]
        public int money;

        protected override string DisplayDescriptionExtra => $"资源量：{money}";
    }
}
