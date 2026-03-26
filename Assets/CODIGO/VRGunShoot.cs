using UnityEngine;
using UnityEngine.InputSystem;

public class VRGunShoot : MonoBehaviour
{
    public float shootDistance = 50f;

    void Update(){
    Debug.Log("Update funcionando");

    if (Input.GetMouseButtonDown(0))
    {
        Debug.Log("Intento disparar");
        Shoot();
    }
}

    void Shoot()
{
    Ray ray = new Ray(transform.position, transform.forward);
    Debug.DrawRay(transform.position, transform.forward * shootDistance, Color.red, 2f);

    RaycastHit[] hits = Physics.RaycastAll(ray, shootDistance);

    foreach (RaycastHit hit in hits)
    {
        Debug.Log("Golpeó: " + hit.collider.name);

        NoteMovement note = hit.collider.GetComponent<NoteMovement>();

        if (note != null)
        {
            note.HitByPlayer();
            break;
        }
    }
}
}