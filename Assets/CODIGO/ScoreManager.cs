using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    
    [Header("Configuración de Combo")]
    public int comboCount = 0;
    public int multiplier = 1;
    public TextMeshProUGUI comboText; // Arrastra el nuevo texto aquí

    [Header("Configuración de UI")]
    public TextMeshProUGUI scoreText;

    [Header("Ajustes de Animación (BPM)")]
    public float bpm = 120f;
    private float beatTimer;
    private Vector3 originalComboScale;

    void Awake() { instance = this; }

    void Start()
    {
        if (comboText != null) originalComboScale = comboText.transform.localScale;
        UpdateScoreUI();
    }

    void Update()
    {
        // EFECTO DE AGITAR CON EL BPM
        beatTimer += Time.deltaTime;
        float interval = 60f / bpm;

        if (beatTimer >= interval)
        {
            beatTimer -= interval;
            if (comboCount > 0) ApplyPunchEffect(); // Hace un pequeño brinco en cada beat
        }

        // Suavizado de escala (para que regrese a su tamaño normal)
        if (comboText != null)
            comboText.transform.localScale = Vector3.Lerp(comboText.transform.localScale, originalComboScale, Time.deltaTime * 10f);
    }

    public void AddScore(string zone)
    {
        comboCount++;
        
        // Lógica de multiplicador
        if (comboCount >= 20) multiplier = 4;
        else if (comboCount >= 10) multiplier = 2;
        else multiplier = 1;

        int basePoints = 0;
        if (zone == "Perfect") basePoints = 200;
        else if (zone == "Good") basePoints = 100;
        else if (zone == "Meh") basePoints = 50;

        score += (basePoints * multiplier);
        
        if(multiplier > 1) ApplyBigPunch(); // Se agranda más fuerte al subir de nivel

        UpdateScoreUI();
    }

    public void ResetCombo()
    {
        comboCount = 0;
        multiplier = 1;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "PUNTOS: " + score;
        if (comboText != null)
        {
            comboText.text = "x" + multiplier + " (" + comboCount + ")";
            comboText.enabled = (comboCount > 0); // Oculta el combo si es 0
        }
    }

    void ApplyPunchEffect() {
        if (comboText != null) comboText.transform.localScale = originalComboScale * 1.2f;
    }

    void ApplyBigPunch() {
        if (comboText != null) comboText.transform.localScale = originalComboScale * 1.8f;
    }
}