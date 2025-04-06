using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootPlayer", story: "[Self] attacks [Player]", category: "Action", id: "f016db31cfda83aebab05ab4b2806914")]
public partial class ShootPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnStart()
    {
        if (Player.Value == null)
        {
            LogFailure("No agent assigned.");
            return Status.Failure;
        }
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self.Value == null)
        {
            return Status.Failure;
        }
        
        CoyoteAttack attack = Self.Value.GetComponent<CoyoteAttack>();

        if (attack == null)
        {
            Debug.Log("No CoyoteAttackingComponent found."); 
            return Status.Failure;
        }

        if (!attack.InRange(Player.Value) || !attack.InSight(Player.Value))
        {
            return Status.Running;
        }
        
        //Debug.Log("Player is in range");
        attack.CoyoteShoot(Player.Value);
        
        return Status.Running;
    }

    
}

