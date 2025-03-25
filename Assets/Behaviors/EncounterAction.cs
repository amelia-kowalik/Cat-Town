using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Encounter", story: "[Self] encounters a(n) [Target]", category: "Action", id: "7fe6beb16730f8f8f8dd8b8aeea2c9ec")]
public partial class EncounterAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    protected override Status OnStart()
    {
        if (Target.Value == null)
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
        
        Sheriff sheriff = Self.Value.GetComponent<Sheriff>();
        SheriffAttacking sheriffAttacking = Self.Value.GetComponent<SheriffAttacking>();
        
        if (sheriffAttacking == null)
        {
            Debug.Log("No SheriffAttack found."); 
            return Status.Failure;
        }

        if (!sheriffAttacking.InRange(Target.Value) || !sheriffAttacking.InSight(Target.Value))
        {
            return Status.Running;
        }
        else if (sheriffAttacking.TooClose(Target.Value))
        {
            sheriff.RunAwayFrom(Target.Value);
            return Status.Running;
        }
        
        
        sheriffAttacking.SheriffFire(Target.Value);
        
        
        return Status.Running;
    }
    
}

