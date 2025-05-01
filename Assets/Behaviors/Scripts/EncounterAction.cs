using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Encounter", story: "[Self] encounters an Enemy", category: "Action", id: "7fe6beb16730f8f8f8dd8b8aeea2c9ec")]
public partial class EncounterAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    //protected override Status OnStart() { }

    protected override Status OnUpdate()
    {
        if (Self.Value == null)
        {
            return Status.Failure;
        }
        
        Sheriff sheriff = Self.Value.GetComponent<Sheriff>();
        SheriffAttack sheriffAttack = Self.Value.GetComponent<SheriffAttack>();
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
            if (sheriffAttack.TooClose(closestEnemy))
            {
                sheriff.RunAwayFrom(closestEnemy);
                return Status.Running;
            }

            if (sheriffAttack.InRange(closestEnemy) && sheriffAttack.InSight(closestEnemy))
            {
                sheriffAttack.TryShoot(closestEnemy);
                return Status.Running;
            }

        }
        
        
        return Status.Running;
    }
    
}

