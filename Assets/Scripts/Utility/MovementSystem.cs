 using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystem : MonoBehaviour
{
    private Cowboy _cowboy;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _cowboy = GetComponent<Cowboy>();
    }
    
    void Update()
    {
        _rb.linearVelocity = _moveInput * _cowboy.Stats["walkingSpeed"];
        
        if (_moveInput.x < -0.01f) {
            _spriteRenderer.flipX = true;
        } else if (_moveInput.x > 0.01f) {
            _spriteRenderer.flipX = false;
        }

    }

    public void Move(InputAction.CallbackContext context)
    {
        _animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetFloat("LastInputX", _moveInput.x);
            _animator.SetFloat("LastInputY", _moveInput.y);
        }
        
        _moveInput = context.ReadValue<Vector2>();
        _animator.SetFloat("InputX", _moveInput.x);
        _animator.SetFloat("InputY", _moveInput.y);
    }

}
