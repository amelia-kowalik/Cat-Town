using UnityEngine;

public class SheriffAttacking : MonoBehaviour
{
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCooldown = 1f;
    [SerializeField] float range = 3f;
    
    
    public bool InRange(GameObject target)
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        //Debug.Log($"Distance to target: {distance}");
        return distance <= range;
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
                string sortingLayer = objectSpriteRend.sortingLayerName;

                if (sortingLayer == "Obstacles")
                {
                    return false;
                }
            }
        }

        return true;
    }

    
}
