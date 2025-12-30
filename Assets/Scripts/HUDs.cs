using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private TMP_Text FinalScoreText;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField] private GameObject deathscreen;
    private 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore(0);
    }
    void Awake()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.HUD = gameObject;
    }
    public void UpdateScore(int score)
    {
        ScoreText.text = $"Score: {score}";
    }

    public void died(int score)
    {
        Time.timeScale = 0;
        mainMenu.SetActive(false);
        deathscreen.SetActive(true);
        FinalScoreText.text= "Your final score is: " + score.ToString()+" even a child could do better!";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
