using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGameOver : MonoBehaviour
{
    public static PanelGameOver Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
}
