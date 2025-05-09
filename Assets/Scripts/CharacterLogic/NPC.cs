using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private int _lives = 1;
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
        _animator.SetBool("isWalking", speed > 0.05f);

        if (speed > 0.05f)
        {
            Vector2 moveDirection = velocity.normalized;
            _lastMoveDirection = moveDirection;
            
            _animator.SetFloat("MoveX", _lastMoveDirection.x);
            _animator.SetFloat("MoveY", _lastMoveDirection.y);
        }
        else
        {
            _animator.SetFloat("moveX", _lastMoveDirection.x);
            _animator.SetFloat("moveY", _lastMoveDirection.y);
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

    public void UpdateLives(int chancesToPunch)
    {
        _lives = chancesToPunch;
    }
    
    private void PunchCoyote(GameObject other)
    {
        _animator.SetTrigger("Punch");
        Coyote coyote = other.GetComponent<Coyote>();
        Debug.Log("Cat punched a coyote");
        coyote.TakeDamage(10);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (_lives > 1)
            {
                PunchCoyote(other.gameObject);
                _lives--; 
            }
            else
            {
                _animator.SetBool("isDead", true);
                Destroy(gameObject); 
            }
        }
    }
}
