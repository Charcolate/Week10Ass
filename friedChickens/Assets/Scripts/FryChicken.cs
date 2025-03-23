using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class FryChicken : ActionTask
{
    public BBParameter<bool> isChickenRaw; // Is the chicken raw?
    public BBParameter<bool> isBurnt; // Is the chicken burnt?
    public BBParameter<float> cookTime = 5f; // Total time to cook the chicken
    public BBParameter<Color> cookedColor; // Final color when fully cooked
    public BBParameter<GameObject> chickenObject; // Chicken's GameObject

    private float timer;
    private Renderer chickenRenderer;
    private Color rawColor;

    protected override void OnExecute()
    {
        if (!isChickenRaw.value)
        {
            Debug.Log("Chicken is already cooked.");
            EndAction(false); // Fail if chicken is not raw
            return;
        }

        // Initialize cooking
        timer = 0f;
        chickenRenderer = chickenObject.value.GetComponent<Renderer>();
        if (chickenRenderer != null)
        {
            rawColor = chickenRenderer.material.color; // Save the raw chicken color
        }
    }

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= cookTime.value)
        {
            // Chicken is fully cooked
            Debug.Log("Chicken is fully cooked!");
            isBurnt.value = false; // Ensure chicken is not burnt
            if (chickenRenderer != null)
            {
                chickenRenderer.material.color = cookedColor.value; // Set to fully cooked color
            }
            EndAction(true); // Success
        }
        else if (isBurnt.value)
        {
            // Chicken is burnt
            Debug.Log("Chicken is burnt! Failure.");
            EndAction(false); // Failure
        }
        else
        {
            // Gradually change the chicken's color
            if (chickenRenderer != null)
            {
                float progress = timer / cookTime.value; // Calculate cooking progress (0 to 1)
                chickenRenderer.material.color = Color.Lerp(rawColor, cookedColor.value, progress);
            }
        }
    }
}
