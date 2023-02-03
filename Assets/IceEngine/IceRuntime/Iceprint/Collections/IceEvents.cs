using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IceEngine
{
    [Serializable] public class SimpleEvent : UnityEvent { }
    [Serializable] public class IntEvent : UnityEvent<int> { }
    [Serializable] public class FloatEvent : UnityEvent<float> { }
    [Serializable] public class StringEvent : UnityEvent<string> { }
}
