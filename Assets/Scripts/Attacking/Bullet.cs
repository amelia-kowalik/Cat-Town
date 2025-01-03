using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private GameObject owner;

    public void Init(int damageValue, GameObject bulletOwner)
    {
        damage = damageValue;
        owner = bulletOwner;
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject == owner)
        {
            return;
        }
        
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
                Debug.Log("Bullet hit an Enemy.");
            }
        }

        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Bullet hit a NPC");
        }
        
        Destroy(gameObject);
    }
}
