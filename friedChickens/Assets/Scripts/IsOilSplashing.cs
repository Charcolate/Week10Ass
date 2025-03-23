using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class IsOilSplashing : ConditionTask
{
    public BBParameter<bool> isOilSplashing; // Blackboard variable to check if oil is splashing

    protected override bool OnCheck()
    {
        return isOilSplashing.value; // Returns true if oil is splashing
    }
}
