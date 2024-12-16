using System;
using Unity.VisualScripting;
using UnityEngine;

public class CoyoteAttackingComponent : Shooting
{
    [SerializeField]
    private float fireRate = 2f;
    private float fireCooldown = 1f;
    
    public void CoyoteShoot()
    {
        fireCooldown -= Time.deltaTime;
        
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; 
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            KidnapCat(other.gameObject);
        }
    }

    public void KidnapCat(GameObject npc)
    {
        Debug.Log("Kidnap Cat");
        npc.SetActive(false);
    }
    
    
}
