using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    protected abstract float FireRate { get; }
    protected abstract float Range { get; }
    protected abstract float BulletSpeed { get; }
    [SerializeField] protected GameObject bulletPrefab;
    protected float fireCooldown = 0f;
    protected Vector2 facingDirection;

    protected virtual void Update()
    {
        fireCooldown -= Time.deltaTime;
        
        float moveOnX = Input.GetAxisRaw("Horizontal");
        float moveOnY = Input.GetAxisRaw("Vertical");

        if (moveOnX != 0 || moveOnY != 0)
        {
            facingDirection = new Vector2(moveOnX, moveOnY).normalized;
        }
    }
    
    public bool InRange(GameObject target)
    {
        return Vector2.Distance(transform.position, target.transform.position) <= Range;
    }

    public bool InSight(GameObject target)
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = (target.transform.position - transform.position).normalized;
        
        //everything that raycast hits
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distance);

        
        foreach (var hit in hits)
        {
            //don't react to own collider
            if (hit.collider.gameObject == gameObject) continue;

            //checks if sprite is on an obstacle layer 
            SpriteRenderer objectSpriteRend = hit.collider.GetComponent<SpriteRenderer>();

            if (objectSpriteRend != null)
            {
                if (objectSpriteRend.sortingLayerName == "Obstacles")
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    protected void FireBullet(Vector2 direction, int damage)
    {
        Vector2 spawnPos = (Vector2)transform.position + direction * 0.5f;
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * BulletSpeed, ForceMode2D.Impulse);

        bullet.GetComponent<Bullet>().Init(damage, gameObject);
        Destroy(bullet, 2f);
    }

    public abstract void TryShoot(GameObject target);
}
