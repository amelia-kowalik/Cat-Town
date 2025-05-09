using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;
    private GameObject _owner;

    public void Init(int damageValue, GameObject bulletOwner)
    {
        _damage = damageValue;
        _owner = bulletOwner;
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject == _owner)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            Cowboy player = other.gameObject.GetComponent<Cowboy>();
            if (player != null)
            {
                player.TakeDamage(_damage);
            }
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Coyote enemy = other.gameObject.GetComponent<Coyote>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
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
