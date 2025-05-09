using UnityEngine;

public class Sheriff : MonoBehaviour
{
    [SerializeField] private float runningSpeed = 2f;
    [SerializeField] private int damage = 10;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    public void RunAwayFrom(GameObject target)
    {
        Vector2 direction = (transform.position - target.transform.position).normalized;
        _rb.linearVelocity = direction * runningSpeed;
    }

    public int GetDamage()
    {
        return damage;
    }
}
