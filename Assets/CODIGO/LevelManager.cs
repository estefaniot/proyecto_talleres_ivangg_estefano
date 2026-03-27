using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Referencias")]
    public NoteSpawner spawner;      
    public AudioSource musicSource;   
    public ScoreManager scoreManager; 

    [Header("Configuración de Canciones")]
    public AudioClip musica60;  // Ejemplo: Imagine
    public AudioClip musica100; // Ejemplo: Billie Jean
    public AudioClip musica140; // Ejemplo: Beat It

    [Header("Estética (Luces)")]
    public Light luzTeatro; // Arrastra una luz del escenario aquí

    [Header("Estética (Luces)")]
    public Light[] todasLasLuces; // El Array para todas las luces del fondo

    public GameObject mainMenuCanvas; // Arrastra el MainMenuCanvas aquí
    public GameObject gameHUD;        // Tu HUD de puntos y vida

    public static LevelManager instance;
    void Awake() { instance = this; }
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        
        // 1. Detener música y spawner
        if (musicSource != null) musicSource.Stop();
        if (spawner != null) spawner.enabled = false; 

        // 2. Limpiar notas (Ahora que el Tag existe no dará error)
        GameObject[] notas = GameObject.FindGameObjectsWithTag("Note");
        foreach(GameObject n in notas) Destroy(n);

        // 3. UI
        if (gameHUD != null) gameHUD.SetActive(false);
        if (HealthManager.instance != null && HealthManager.instance.panelGameOver != null)
            HealthManager.instance.panelGameOver.SetActive(false);

        if (mainMenuCanvas != null) mainMenuCanvas.SetActive(true);
    }
    
    // --- FUNCIÓN DE HOVER (CUANDO EL RAYO ENTRA) ---
    public void HoverLuz(int dificultad)
    {
        Color colorHover = Color.white;
        
        // Definimos el color según el botón al que apuntamos
        if (dificultad == 1) colorHover = Color.green;
        else if (dificultad == 2) colorHover = Color.blue;
        else if (dificultad == 3) colorHover = Color.red;

        // Cambiamos todas las luces de la lista
        foreach (Light luz in todasLasLuces)
        {
            if (luz != null) 
            {
                luz.color = colorHover;
                luz.intensity = 1.5f; // Sube un poco el brillo al apuntar
            }
        }
    }

    // --- FUNCIÓN DE SALIDA (CUANDO EL RAYO SE QUITA) ---
    public void SalirHover()
    {
        foreach (Light luz in todasLasLuces)
        {
            if (luz != null)
            {
                luz.color = Color.white; // Vuelven a la normalidad
                luz.intensity = 3f;   // Brillo tenue original
            }
        }
    }
    void ConfigurarJuego(float bpm, AudioClip clip, Color corNivel)
    {
        // 1. Activamos el movimiento global de las notas
        NoteMovement.canMove = true; 

        // 2. Opcional: Ajustar la velocidad de las notas según el nivel
        // Si quieres que en difícil vayan más rápido, puedes pasar un valor aquí
        
        spawner.enabled = true; 
        spawner.bpm = bpm;
        
        scoreManager.bpm = bpm;

        musicSource.clip = clip;
        musicSource.Play();

        if(luzTeatro != null) luzTeatro.color = corNivel;

        foreach (Light luz in todasLasLuces)
        {
        if (luz != null) 
        {
            luz.color = corNivel;
            luz.intensity = 100f; // Puedes ajustar la intensidad aquí también
        }
    }
    }
    public void SeleccionarNivel(int dificultad)
    {
        if(HealthManager.instance != null) HealthManager.instance.Heal(100);
        // Reseteamos puntos y combo al empezar
        scoreManager.score = 0;
        scoreManager.ResetCombo();

        if (dificultad == 1) // FÁCIL
        {
            ConfigurarJuego(60f, musica60, Color.green);
        }
        else if (dificultad == 2) // MEDIO
        {
            ConfigurarJuego(100f, musica100, Color.blue);
        }
        else if (dificultad == 3) // DIFÍCIL
        {
            ConfigurarJuego(140f, musica140, Color.red);
        }

        // Ocultamos el menú para jugar
        gameObject.SetActive(false);

        // Mostramos el HUD de juego
        if(gameHUD != null) gameHUD.SetActive(true); // Encendemos la vida y puntos
        gameObject.SetActive(false); // Apagamos el menú
    }
}
