using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int baseDamage = 40;

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
        GameManager.OnCoyoteDeath?.Invoke();
    }

    public int GetDamage()
    {
        int randomDamage = Random.Range(baseDamage, 60);
        return randomDamage;
    }
    
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

    }
}
