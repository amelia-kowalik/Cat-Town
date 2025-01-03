using System;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private Transform bulletSpawn;
    private float bulletSpeed = 5f;
    private float timeToDestroy = 2f;
    [NonSerialized] public Vector2 facingDirection;

    private void Update()
    {
        float moveOnX = Input.GetAxisRaw("Horizontal");
        float moveOnY = Input.GetAxisRaw("Vertical");

        if (moveOnX != 0 || moveOnY != 0)
        {
            facingDirection = new Vector2(moveOnX, moveOnY).normalized;
        }
    }

    public void Shoot()
    {
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y) + facingDirection * 0.5f;
            GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Vector2 bulletDirection = facingDirection * bulletSpeed;
            
            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);
            
            Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();

            if (TryGetComponent<Coyote>(out Coyote coyote))
            {
                bulletScript.Init(coyote.GetCoyoteDamage(), gameObject);
            }
            else if (TryGetComponent<Cowboy>(out Cowboy cowboy))
            {
                bulletScript.Init(cowboy.GetCowboyDamage(),gameObject);
            }
            
            Destroy(spawnedBullet, timeToDestroy);
    }
    


} 
