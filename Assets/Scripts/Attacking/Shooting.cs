using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private Transform bulletSpawn;
    private float bulletSpeed = 5f;
    private float timeToDestroy = 2f;
    [NonSerialized] public Vector2 facingDirection;

    public void Shoot()
    {
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y) + facingDirection * 0.5f;
            GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Vector2 bulletDirection = facingDirection * bulletSpeed;
            
            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);
            
            Destroy(spawnedBullet, timeToDestroy);
    }
    


} 
