using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    public float speed = 2f;
    private string currentZone = "None";
    
    // Nueva variable estática para controlar el movimiento global de todas las notas
    public static bool canMove = false; 

    void Update()
    {
        // Solo se mueve si canMove es true
        if (canMove)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Perfect"))
            currentZone = "Perfect";
        else if(other.CompareTag("Good"))
            currentZone = "Good";
        else if(other.CompareTag("Meh"))
            currentZone = "Meh";
        
        if (other.CompareTag("DestroyZone")) 
        {
            if (ScoreManager.instance != null) ScoreManager.instance.ResetCombo();

            // --- NUEVO: RESTAR VIDA ---
            if (HealthManager.instance != null)
            {
                HealthManager.instance.TakeDamage(10); // Quita 10 de vida
            }
            // --------------------------

            Destroy(gameObject);
        }
    }

    public void HitByPlayer()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(currentZone);
        }
        Destroy(gameObject);
    }
}