using System;
using UnityEngine;

public class CoyoteAttack : BaseAttack
{
    private const string IsAttacking = "isAttacking";
    
    protected override float FireRate => 1f;
    protected override float Range => 3f;
    protected override float BulletSpeed => 5f;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public override void TryShoot(GameObject target)
    {
        _animator.SetBool(IsAttacking, true);
        
        if (fireCooldown > 0f) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;

        int damage = GetComponent<Coyote>().GetDamage();
        FireBullet(direction, damage);

        fireCooldown = 1f / FireRate;
    }

    public void EndCoyoteAttack()
    {
        _animator.SetBool(IsAttacking, false);
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

