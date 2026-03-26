using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform[] spawnPoints;

    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating("SpawnNote", 1f, spawnInterval);
    }

    void SpawnNote()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
    }
}