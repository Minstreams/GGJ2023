using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IceEngine;

public class SimpleLinker : MonoBehaviour
{
    [IceprintPort] public void Input() => output?.Invoke();
    [IceprintPort] public SimpleEvent output;
}
