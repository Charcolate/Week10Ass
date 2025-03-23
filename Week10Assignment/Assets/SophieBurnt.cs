using NodeCanvas.Framework;
using UnityEngine;


public class SophieBurned : ConditionTask
{
    public BBParameter<bool> sophieBurned;

    protected override bool OnCheck()
    {
        return sophieBurned.value; // True if Sophie got burned
    }
}
