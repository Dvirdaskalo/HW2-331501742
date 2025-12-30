using System;
using UnityEngine;

public class reassighnRestartFunc : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
            gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => GameManager.Instance.RestartGame());
    }
}
