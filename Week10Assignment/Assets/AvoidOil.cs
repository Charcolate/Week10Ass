using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class AvoidOil : ActionTask
{
    public BBParameter<bool> isOilOnSophie;

    protected override void OnExecute()
    {
        if (isOilOnSophie.value)
        {
            Debug.Log("Oil is on Sophie! Failure.");
            EndAction(false); // Failure
        }
        else
        {
            Debug.Log("Sophie avoided the oil.");
            EndAction(true); // Success
        }
    }
}

