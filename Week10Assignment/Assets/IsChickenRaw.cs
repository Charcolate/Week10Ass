using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


public class IsChickenRaw : ConditionTask
{
    public BBParameter<bool> isChickenRaw;

    protected override bool OnCheck()
    {
        return isChickenRaw.value;
    }
}
