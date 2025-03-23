using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


public class IsOilSplashing : ConditionTask
{
    public BBParameter<bool> isOilSplashing;

    protected override bool OnCheck()
    {
        return isOilSplashing.value;
    }
}
