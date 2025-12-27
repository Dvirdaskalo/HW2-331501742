using System;
using UnityEngine;

public class mainmenu : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.MainMenu = gameObject;
    }
}
