using UnityEngine;

public class CoyoteAI : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] Vector2 bodySize = new Vector2(1, 1);
    [SerializeField] LayerMask obstacleMask;
    
    private string npcTag = "NPC";
    private string playerTag = "Player";
    private GameObject player;
    private GameObject currentTarget;
    private CoyoteAttack coyoteAttack;
    
    void Start()
    {
        coyoteAttack = GetComponent<CoyoteAttack>();
        player = GameObject.FindGameObjectWithTag(playerTag);
    }

    
    void Update()
    {
        if (PlayerVisible())
        {
            MoveTowards(player);
            coyoteAttack.TryShoot(player);
        }
        else
        {
            if (currentTarget == null) FindNpc();
            if (currentTarget != null) MoveTowards(currentTarget);

        }
    }

    private bool PlayerVisible()
    {
        return coyoteAttack.InRange(player) && coyoteAttack.InSight(player);
    }

    private void FindNpc()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(npcTag);
        
        float closestDistance = Mathf.Infinity;
        GameObject closestNpc = null;

        foreach (GameObject npc in npcs)
        {
            float distanceToNpc = Vector2.Distance(transform.position, npc.transform.position);

            if (closestNpc == null || distanceToNpc < closestDistance)
            {
                closestNpc = npc;
                closestDistance = distanceToNpc;
            }
        }
        
        currentTarget = closestNpc;
    }

    private void MoveTowards(GameObject target)
    {
        if (target == null) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;
        float distance = moveSpeed * Time.deltaTime;

        // First try direct move
        if (CanMove(direction, distance))
        {
            Move(direction, distance);
            return;
        }

        // Try sliding along Y axis
        Vector2 slideY = new Vector2(0, direction.y).normalized;
        if (Mathf.Abs(direction.y) > 0.1f && CanMove(slideY, distance))
        {
            Move(slideY, distance);
            return;
        }

        // Try sliding along X axis
        Vector2 slideX = new Vector2(direction.x, 0).normalized;
        if (Mathf.Abs(direction.x) > 0.1f && CanMove(slideX, distance))
        {
            Move(slideX, distance);
            return;
        }

        // Blocked in all directions
        Debug.DrawRay(transform.position, direction * distance, Color.red);
    }
    
    private bool CanMove(Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, bodySize, 0f, direction, distance, obstacleMask);
        return hit.collider == null;
    }

    private void Move(Vector2 direction, float distance)
    {
        transform.position += (Vector3)(direction * distance);
        
    }
}
