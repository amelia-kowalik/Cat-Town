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
        
        CoyoteAttackingComponent attackingComponent = Self.Value.GetComponent<CoyoteAttackingComponent>();

        if (attackingComponent == null)
        {
            Debug.Log("No CoyoteAttackingComponent found."); 
            return Status.Failure;
        }

        if (!attackingComponent.InRange(Player.Value) || !attackingComponent.InSight(Player.Value))
        {
            return Status.Running;
        }
        
        //Debug.Log("Player is in range");
        attackingComponent.CoyoteShoot(Player.Value);
        
        return Status.Running;
    }

    
}

