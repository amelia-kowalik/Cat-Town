using UnityEngine;

public class CowboyAttack : Shooting
{
    
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
}
