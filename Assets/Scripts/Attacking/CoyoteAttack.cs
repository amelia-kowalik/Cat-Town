using System;
using UnityEngine;

public class CoyoteAttack : BaseAttack
{
    public static Action OnCatKidnapped;

    protected override float FireRate => 1f;
    protected override float Range => 3f;
    protected override float BulletSpeed => 5f;

    
    
    public override void TryShoot(GameObject target)
    {
        if (fireCooldown > 0f) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;

        int damage = GetComponent<Coyote>().GetDamage();
        FireBullet(direction, damage);

        fireCooldown = 1f / FireRate;
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
        OnCatKidnapped?.Invoke();
    }

}

