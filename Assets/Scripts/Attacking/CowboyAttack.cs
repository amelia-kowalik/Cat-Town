using System.Collections;
using UnityEngine;

public class CowboyAttack : MonoBehaviour
{
    private const string IsAttacking = "isAttacking";
    private const string LastInputX = "LastInputX";
    private const string LastInputY = "LastInputY";

    
    [SerializeField] public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float timeToDestroy = 2f;
    private Vector2 _facingDirection;
    private Animator _animator;
    private CharacterSFXManager _sfxManager;

    void Start()
    {  
        _sfxManager = GetComponent<CharacterSFXManager>();
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movementInput != Vector2.zero)
        {
            _facingDirection = movementInput.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Shoot();
        }
    }
    
    public void Shoot()
    {
        _animator.SetBool(IsAttacking, true);
        _animator.SetFloat(LastInputX, _facingDirection.x);
        _animator.SetFloat(LastInputY, _facingDirection.y);
        _sfxManager.PlaySound(0);
        
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y) + _facingDirection * 0.5f;
        GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Vector2 bulletDirection = _facingDirection * bulletSpeed;
            
        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(bulletDirection, ForceMode2D.Impulse);
            
        Bullet bulletScript = spawnedBullet.GetComponent<Bullet>();
            
        if (TryGetComponent<Cowboy>(out Cowboy cowboy))
        {
            bulletScript.Init(cowboy.GetDamage(),gameObject);
        }
        
        Destroy(spawnedBullet, timeToDestroy);
    }

    public void EndAttack()
    {
        _animator.SetBool(IsAttacking, false);
    }
}
