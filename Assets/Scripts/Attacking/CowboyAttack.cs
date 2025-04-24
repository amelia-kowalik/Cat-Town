using UnityEngine;

public class CowboyAttack : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float timeToDestroy = 2f;
    private Vector2 facingDirection;
    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movementInput != Vector2.zero)
        {
            facingDirection = movementInput.normalized;
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
    }
    
    public void Shoot()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y) + facingDirection * 0.5f;
        GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Vector2 bulletDirection = facingDirection * bulletSpeed;
            
        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);
            
        Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();
            
        if (TryGetComponent<Cowboy>(out Cowboy cowboy))
        {
            bulletScript.Init(cowboy.GetDamage(),gameObject);
        }
            
            
        Destroy(spawnedBullet, timeToDestroy);
    }
}
