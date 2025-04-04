using UnityEngine;

public class Coyote : MonoBehaviour
{
    [SerializeField] private int health = 30;
    [SerializeField] private int baseDamage = 10;

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

    public int GetDamage()
    {
        int randomDamage = Random.Range(baseDamage, 20);
        return randomDamage;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }
}
