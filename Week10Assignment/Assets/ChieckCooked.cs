using NodeCanvas.Framework;
using UnityEngine;


public class ChickenCooked : ConditionTask
{
    public BBParameter<bool> chickenCooked;

    protected override bool OnCheck()
    {
        return chickenCooked.value; // True if chicken is fully cooked
    }
}
