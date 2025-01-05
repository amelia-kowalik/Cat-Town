using UnityEngine;

public class Coyote : MonoBehaviour
{
    [SerializeField] private int health = 30;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public int GetCoyoteDamage()
    {
        int randomDamage = Random.Range(10, 20);
        return randomDamage;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }
}
