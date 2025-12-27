using System;
using UnityEngine;

public class restartbt : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
            gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => GameManager.Instance.RestartGame());
    }
}
