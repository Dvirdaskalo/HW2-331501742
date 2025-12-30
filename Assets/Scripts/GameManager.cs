using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject HUD;
    public GameObject Player;
    [SerializeField] public GameObject deathscreen;
    [SerializeField]
    public GameObject MainMenu;
    public static GameManager Instance { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);

        PauseGame();

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        MainMenu.SetActive(false);
        HUD.SetActive(true);
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        MainMenu.SetActive(true);
        HUD.SetActive(false);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(RestartCoroutine());
    }
    public IEnumerator RestartCoroutine()
    {
        // Start loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Scene is loaded, now you can safely deactivate the HUD
        if (MainMenu != null)
            MainMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
