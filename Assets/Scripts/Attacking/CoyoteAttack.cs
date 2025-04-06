using System;
using UnityEngine;

public class CoyoteAttack : Shooting
{
    
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCooldown = 0f;
    [SerializeField] float range = 3f;

    private float CheckDistance(GameObject target)
        {
            Vector2 selfPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            return Vector2.Distance(selfPosition, targetPosition);
        }
    
    public bool InRange(GameObject target)
    {
        float distance = CheckDistance(target);
        //Debug.Log($"Distance to target: {distance}");
        return distance <= range;
    }
    
    //checks if something blocks line of sight/shoot
    public bool InSight(GameObject target)
    {
        float distance = CheckDistance(target);
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
                string sortingLayer = objectSpriteRend.sortingLayerName;

                if (sortingLayer == "Obstacles")
                {
                    return false;
                }
            }
        }

        return true;
    }
    
    
    public void CoyoteShoot(GameObject target)
    {
        fireCooldown -= Time.deltaTime;
        
        if (fireCooldown <= 0f)
        {
            facingDirection = (target.transform.position - transform.position).normalized;
            //Debug.Log("Shooting!");
            Shoot();
            fireCooldown = 1f / fireRate; 
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            KidnapCat(other.gameObject);
        }
    }

    public void KidnapCat(GameObject npc)
    {
        Debug.Log("Kidnapped Cat");
        npc.SetActive(false);
        GameManager.OnCatKidnapped?.Invoke();
    }
    
    
}
