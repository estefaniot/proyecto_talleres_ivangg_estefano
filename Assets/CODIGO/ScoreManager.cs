using UnityEngine;
using TMPro; // Asegúrate de tener instalado TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText; // Arrastra tu objeto de texto aquí

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(string zone)
    {
        if(zone == "Perfect") score += 200;
        else if(zone == "Good") score += 100;
        else if(zone == "Meh") score += 50;

        UpdateScoreUI();
        Debug.Log("Score: " + score);
    }

    void UpdateScoreUI()
    {
        if(scoreText != null)
            scoreText.text = "Puntaje: " + score;
    }
}