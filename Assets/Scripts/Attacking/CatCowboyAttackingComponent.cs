using Unity.VisualScripting;
using UnityEngine;

public class CatCowboyAttackingComponent : Shooting
{
    [SerializeField] private float radius = 5f;
    [SerializeField] [Range(1f, 360f)] private float angle = 90f;
    [SerializeField] private float fireRate = 2f;
    private float fireCooldown = 0f;
    private Transform target;

    // Update is called once per frame
    void Update()
    {
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


                if (Vector2.Angle(transform.forward, directionToTarget) < angle / 2)
                {

                    RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget);
                    
                    if (raycastHit.collider != null && raycastHit.collider.transform != target)
                    {
                        SpriteRenderer obstacleSpriteRenderer = raycastHit.collider.GetComponent<SpriteRenderer>();

                        if (obstacleSpriteRenderer != null && (obstacleSpriteRenderer.sortingLayerName == "Obstacles" || obstacleSpriteRenderer.sortingLayerName == "Walk behind"))
                        {
                            continue; 
                        }
                    }
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
        fireCooldown -= Time.deltaTime;
        
        if (fireCooldown <= 0f)
        {
            Vector2 facingDirection = (target.position - transform.position).normalized;
            Shoot();
            fireCooldown = 1f / fireRate; 
        }
    }
}
