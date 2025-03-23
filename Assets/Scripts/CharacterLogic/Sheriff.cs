using UnityEngine;

public class Sheriff : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RunAwayFrom(GameObject target)
    {
        Vector2 direction = (transform.position - target.transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }
}
