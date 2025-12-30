using UnityEngine;

public class reassighnQuitFunc : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
            gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => GameManager.Instance.QuitGame());
    }
}
