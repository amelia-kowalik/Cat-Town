using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private int _lives = 1;
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void updateLives(int chancesToPunch)
    {
        _lives = chancesToPunch;
    }
    
    private void PunchCoyote(GameObject other)
    {
        Coyote coyote = other.GetComponent<Coyote>();
        Debug.Log("Cat punched a coyote");
        coyote.TakeDamage(10);
        
        //punching animation
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (_lives > 1)
            {
                PunchCoyote(other.gameObject);
                _lives--; 
            }
            else
            {
                Destroy(gameObject); 
            }
        }
    }
}
