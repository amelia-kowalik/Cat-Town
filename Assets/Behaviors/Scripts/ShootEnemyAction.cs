using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootEnemy", story: "[Self] shoots [Target]", category: "Action", id: "f1c3287b767c4184fc805f396024433c")]
public partial class ShootEnemyAction : Action
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
        
        ExpertAttack expertAttack = Self.Value.GetComponent<ExpertAttack>();

        if (Target.Value == null)
        {
            return Status.Running;
        }
        
        if (!expertAttack.InRange(Target.Value) || !expertAttack.InSight(Target.Value))
        {
            return Status.Running;
        }
        
        expertAttack.TryShoot(Target.Value);
        
        return Status.Running;
        
    }

    
}

