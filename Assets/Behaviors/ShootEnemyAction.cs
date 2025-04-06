using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

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
        
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(Self.Value.transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            if (expertAttack.InRange(Target.Value) && expertAttack.InSight(Target.Value))
            {
                expertAttack.ExpertFire(closestEnemy);
                return Status.Running;
            }
        }
        
        return Status.Running;
        
    }
    
}

