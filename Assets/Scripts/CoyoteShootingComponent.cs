using UnityEngine;

public class CoyoteShootingComponent : Shooting
{
    private float fireRate = 2f;
    private float fireCooldown = 0f;
    
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; 
        }
    }
    
    
}
