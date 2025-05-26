using UnityEngine;

public class ExpertAttack : BaseAttack
{
    protected override float FireRate => 1f;
    protected override float Range => 4f;
    protected override float BulletSpeed => 5f;
    
    

    public override void TryShoot(GameObject target)
    {
        if (fireCooldown > 0f) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;

        FireBullet(direction, GetComponent<Expert>().GetDamage());
        fireCooldown = 1f / FireRate;
    }
}
