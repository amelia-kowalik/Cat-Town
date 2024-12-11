using UnityEngine;

public class Cowboy : MonoBehaviour
{
    private int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Heal(int heal)
    {
        health += heal;
    }

    void Die()
    {
        if (health <= 0)
        {
            //
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
