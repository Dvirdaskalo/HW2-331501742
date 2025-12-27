using System;
using UnityEngine;

public class deat : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.deathscreen = gameObject;
        gameObject.SetActive(false);
    }
}
