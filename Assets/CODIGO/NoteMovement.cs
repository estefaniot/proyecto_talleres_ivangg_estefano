using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float speed = 2f;
    private string currentZone = "None"; // Guarda la zona actual para el puntaje

    void Update()
    {
        // Mueve la nota constantemente
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Actualiza la zona actual basándose en el Tag del disparador
        if(other.CompareTag("Perfect"))
            currentZone = "Perfect";
        else if(other.CompareTag("Good"))
            currentZone = "Good";
        else if(other.CompareTag("Meh"))
            currentZone = "Meh";
        
        // Si llega al final, se destruye sin sumar puntos
        if (other.CompareTag("DestroyZone")) 
        {
            // SI LA NOTA SE PIERDE, EL COMBO VUELVE A CERO
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.ResetCombo();
            }
            Destroy(gameObject);
        }
        Debug.Log("Nota entró en zona: " + currentZone);
    }

    // ESTA ES LA FUNCIÓN QUE DEBES CONECTAR EN EL INSPECTOR
    public void HitByPlayer()
    {
        Debug.Log("HitByPlayer se ejecutó");
        // 1. Suma el puntaje según la zona detectada
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(currentZone);
            Debug.Log("¡CLICK detectado! Zona: " + currentZone);
        }
        
        // 2. Destruye la nota inmediatamente
        Destroy(gameObject);
    }
}