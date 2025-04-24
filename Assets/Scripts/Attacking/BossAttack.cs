using UnityEngine;

public class BossAttack : BaseAttack
{
    protected override float FireRate => 1.5f;
    protected override float Range => 5f;
    protected override float BulletSpeed => 5f;
    

    public override void TryShoot(GameObject target)
    {
        if (fireCooldown > 0f) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;

        int damage = GetComponent<Boss>().GetDamage();
        FireBullet(direction, damage);

        fireCooldown = 1f / FireRate;
    }
}
