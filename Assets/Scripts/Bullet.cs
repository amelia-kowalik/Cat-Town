using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 10;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cowboy player = other.gameObject.GetComponent<Cowboy>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Coyote enemy = other.gameObject.GetComponent<Coyote>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        
        Destroy(gameObject);
    }
}
