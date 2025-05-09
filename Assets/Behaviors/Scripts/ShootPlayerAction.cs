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
    [SerializeReference] public GameObject player;

    protected override Status OnStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
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

        if (!attack.InRange(player) || !attack.InSight(player))
        {
            return Status.Running;
        }
        
        //Debug.Log("Player is in range");
        attack.TryShoot(player);
        
        return Status.Running;
    }

    
}

