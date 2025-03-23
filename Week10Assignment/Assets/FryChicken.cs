using NodeCanvas.Framework;
using UnityEngine;

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


public class FryChicken : ActionTask
{
    public BBParameter<bool> isBurnt;

    protected override void OnExecute()
    {
        if (isBurnt.value)
        {
            Debug.Log("Chicken is burnt! Failure.");
            EndAction(false); // Failure
        }
        else
        {
            Debug.Log("Chicken is being fried...");
            EndAction(true); // Success
        }
    }
}
