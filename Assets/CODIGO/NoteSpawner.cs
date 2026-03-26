using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Configuración de Notas")]
    public GameObject notePrefab;
    public Transform[] spawnPoints;

    [Header("Ritmo de la Música")]
    public float bpm = 120f;
    [Range(0f, 1f)] 
    public float chanceToSpawn = 0.5f; // 0.5 significa que solo sale nota en el 50% de los beats

    private float beatInterval;
    private float timer;

    void Update()
    {
    timer += Time.deltaTime;
    beatInterval = 60f / bpm; // Lo recalculamos por si cambias el BPM en juego

    if (timer >= beatInterval)
    {
        timer -= beatInterval;
        
        // Solo spawnea si el número aleatorio es menor a la probabilidad
        if (Random.value <= chanceToSpawn)
        {
            SpawnNote();
        }
    }
}

    void SpawnNote()
    {
        if (spawnPoints.Length == 0) return;

        // Elegimos un carril aleatorio
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Creamos la nota
        Instantiate(notePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}