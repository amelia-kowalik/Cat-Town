using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Distance 2D", story: "distance between [Self] and [Target] is [equal] [amount]", category: "Conditions", id: "38c4f1e3590e5966fcceb88fa80afaa2")]
public partial class CheckDistance2DCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Equal;
    [SerializeReference] public BlackboardVariable<float> Amount;

    public override bool IsTrue()
    {
        if (Self.Value == null || Target.Value == null)
        {
            return false;
        } 
        Vector2 transformPosition = new Vector2(Self.Value.transform.position.x, Self.Value.transform.position.y); 
        Vector2 targetPosition = new Vector2(Target.Value.transform.position.x, Target.Value.transform.position.y); 
        
        float distance = Vector2.Distance(transformPosition, targetPosition); 
        return ConditionUtils.Evaluate(distance, Equal, Amount.Value);
    }
}
