using UnityEngine;

public class SheriffAttack : BaseAttack
{
    protected override float FireRate => 1f;
    protected override float Range => 2f;
    protected override float BulletSpeed => 5f;
    [SerializeField] private float safeDistance = 0.5f;


    public bool TooClose(GameObject target)
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        return distance <= safeDistance;
    }

    

    public override void TryShoot(GameObject target)
    {
        if (fireCooldown > 0f) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;

        FireBullet(direction, GetComponent<Sheriff>().GetDamage());
        fireCooldown = 1f / FireRate;
    }
}
