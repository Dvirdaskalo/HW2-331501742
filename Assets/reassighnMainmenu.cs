using System;
using UnityEngine;

public class reassighnMainmenu : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.MainMenu = gameObject;
    }
}
