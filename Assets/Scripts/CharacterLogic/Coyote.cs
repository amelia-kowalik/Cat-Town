using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Coyote : MonoBehaviour
{
    private const string IsWalking = "isWalking";
    private const string Hurt = "Hurt";
    private const string MoveX = "MoveX";
    private const string MoveY = "MoveY";
    
    [SerializeField] private int health = 30;
    [SerializeField] private int baseDamage = 5;
    [SerializeField] private int maxDamage = 12;
    private Animator _animator;
    private NavMeshAgent _agent;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _lastMoveDirection = Vector2.down;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    
        Vector2 velocity = _agent.velocity;
        float speed = velocity.magnitude;
        _animator.SetBool(IsWalking, speed > 0.05f);

        if (speed > 0.05f)
        {
            Vector2 moveDirection = velocity.normalized;
            _lastMoveDirection = moveDirection;
            
            _animator.SetFloat(MoveX, _lastMoveDirection.x);
            _animator.SetFloat(MoveY, _lastMoveDirection.y);
        }
        else
        {
            _animator.SetFloat(MoveX, _lastMoveDirection.x);
            _animator.SetFloat(MoveY, _lastMoveDirection.y);
        }
        
        if (_agent.velocity.x < -0.1f)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_agent.velocity.x > 0.1f)
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger(Hurt);
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameManager.OnCoyoteDeath?.Invoke();
    }

    public int GetDamage()
    {
        int randomDamage = Random.Range(baseDamage, maxDamage);
        return randomDamage;
    }
    
    
    
}
