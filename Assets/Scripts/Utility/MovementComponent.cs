 using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystem : MonoBehaviour
{
    //[SerializeField] float movementSpeed = 3f;
    private Cowboy cowboy;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cowboy = GetComponent<Cowboy>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * cowboy.Stats["walkingSpeed"];
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
