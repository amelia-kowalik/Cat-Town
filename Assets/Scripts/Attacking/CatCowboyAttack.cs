using Unity.VisualScripting;
using UnityEngine;

public class CatCowboyAttack : BaseAttack
{
    [SerializeField] private float radius = 5f;
    [SerializeField] [Range(1f, 360f)] private float angle = 90f; 
    protected override float FireRate => 1f;
    protected override float Range => 0f;
    protected override float BulletSpeed => 4f;
    private Transform target;


    void Start()
    {
        target = GetCoyoteInSight();
    }
    
    protected override void Update()
    {
        base.Update();

        target = GetCoyoteInSight();

        if (target != null)
        {
            TryShoot(target.gameObject);
        }
    }
    
    public Transform GetCoyoteInSight()
    {
        //creates a radius around object
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        Transform closestCoyote = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            Transform target = hit.transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            float distanceToTarget = Vector2.Distance(transform.position, target.position);


            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                if (distanceToTarget < closestDistance)
                {
                    closestCoyote = target;
                    closestDistance = distanceToTarget;
                }
            }
        }

        return closestCoyote;
    }

    public override void TryShoot(GameObject target)
    {
        
        if (fireCooldown > 0f) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;

        int damage = GetComponent<CatCowboy>().GetDamage();
        FireBullet(direction, damage);

        fireCooldown = 1f / FireRate;
        }
}

