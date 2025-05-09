using UnityEngine;

//Cat Eastwood
public class Expert : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private int damage = 15;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    public void TakeDamage(int takenDamage)
    {
        health -= takenDamage;

        if (health <= 0)
        {
            Die();
        }
    }

    public int GetDamage()
    {
        return damage;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
