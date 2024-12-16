using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cowboy player = other.gameObject.GetComponent<Cowboy>();
            if (player != null)
            {
                player.TakeDamage(gameObject.GetComponent<Coyote>().CoyoteDamage());
            }
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Coyote enemy = other.gameObject.GetComponent<Coyote>();
            if (enemy != null)
            {
                enemy.TakeDamage(gameObject.GetComponent<Cowboy>().CowboyDamage());
            }
        }

        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Bullet hit a NPC");
        }
        
        Destroy(gameObject);
    }
}
