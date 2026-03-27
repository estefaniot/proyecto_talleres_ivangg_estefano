using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [Header("Ajustes de Vida")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar; // Arrastra tu Slider aquí

    void Awake() { instance = this; }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameOver();
        }
        UpdateUI();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (healthBar != null) healthBar.value = currentHealth / maxHealth;
    }

    [Header("UI de Muerte")]
    public GameObject panelGameOver; // Arrastra el panel aquí

    void GameOver()
    {
        Debug.Log("GAME OVER");
        if (panelGameOver != null) panelGameOver.SetActive(true);
        
        NoteMovement.canMove = false;
        
        // Apagar el spawner para que no salgan más
        if (LevelManager.instance != null) {
            LevelManager.instance.spawner.enabled = false;
            LevelManager.instance.musicSource.Stop();
        }
    }
}