using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


public class IsChickenRaw : ConditionTask
{
    public BBParameter<bool> isChickenRaw; // Blackboard variable to check if chicken is raw

    protected override bool OnCheck()
    {
        return isChickenRaw.value; // Returns true if chicken is raw
    }
}
