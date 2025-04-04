using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private int lives = 1;
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void updateLives(int chancesToPunch)
    {
        lives = chancesToPunch;
    }
    
    private void PunchCoyote(GameObject other)
    {
        Coyote coyote = other.GetComponent<Coyote>();
        
        coyote.TakeDamage(10);
        
        //punching animation
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (lives > 1)
            {
                PunchCoyote(other.gameObject);
                lives--; 
            }
            else
            {
                Destroy(gameObject); 
            }
        }
    }
}
