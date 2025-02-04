using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowResoluter : MonoBehaviour
{
    [SerializeField] private Vector2Int screenSize;

    void Awake()
    {
        Screen.SetResolution(screenSize.x, screenSize.y, false, 30);
    }
}
