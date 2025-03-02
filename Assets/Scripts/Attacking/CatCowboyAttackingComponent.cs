using Unity.VisualScripting;
using UnityEngine;

public class CatCowboyAttackingComponent : Shooting
{
    [SerializeField] private float radius = 5f;
    [SerializeField] [Range(1f, 360f)] private float angle = 90f;
    [SerializeField] private float fireRate = 1f;
    private float fireCooldown = 0f;
    private Transform target;


    void Start()
    {
        target = GetCoyoteInSight();
    }
    
    void Update()
    {
        fireCooldown -= Time.deltaTime;

        target = GetCoyoteInSight();

        if (target != null)
        {
            CatCowboyShoot();
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

                //RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget);


                /*if (raycastHit.collider != null && raycastHit.collider.transform != target)
                {
                    SpriteRenderer obstacleSpriteRenderer = raycastHit.collider.GetComponent<SpriteRenderer>();

                    if (obstacleSpriteRenderer != null && (obstacleSpriteRenderer.sortingLayerName == "Obstacles" || obstacleSpriteRenderer.sortingLayerName == "Walk behind"))
                    {
                        continue;
                    }
                }*/
                if (distanceToTarget < closestDistance)
                {
                    closestCoyote = target;
                    closestDistance = distanceToTarget;
                }
            }
        }

        return closestCoyote;
    }

    public void CatCowboyShoot()
    {
        if (fireCooldown <= 0f)
        {
            if (target == null) return;

            Vector2 direction = (target.position - transform.position).normalized;

            // Stwórz pocisk
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
            fireCooldown = 1f / fireRate;
        }
    }
}
