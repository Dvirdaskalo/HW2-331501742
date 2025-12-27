using UnityEngine;

public class quits : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
            gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => GameManager.Instance.QuitGame());
    }
}
