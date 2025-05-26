using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootPlayer", story: "[Self] attacks player", category: "Action", id: "f016db31cfda83aebab05ab4b2806914")]
public partial class ShootPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    private GameObject _player; 
    private CoyoteAttack _coyoteAttack;
    private BossAttack _bossAttack;
    
    protected override Status OnStart()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player == null)
        {
            LogFailure("No agent assigned.");
            return Status.Failure;
        }
        
        _coyoteAttack = Self.Value.GetComponent<CoyoteAttack>();
        _bossAttack = Self.Value.GetComponent<BossAttack>();
        
        if (_coyoteAttack == null && _bossAttack == null)
        {
            Debug.LogWarning("Neither CoyoteAttack nor BossAttack found on Self.");
            return Status.Failure;
        }
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self.Value == null || _player == null)
        {
            return Status.Failure;
        }

        if (_coyoteAttack != null)
        {
            if (_coyoteAttack.InRange(_player) && _coyoteAttack.InSight(_player))
            {
                _coyoteAttack.TryShoot(_player);
            }
        }
        else if (_bossAttack != null)
        {
            if (_bossAttack.InRange(_player) && _bossAttack.InSight(_player))
            {
                _bossAttack.TryShoot(_player);
            }
        }
        
        return Status.Running;
    }
}

