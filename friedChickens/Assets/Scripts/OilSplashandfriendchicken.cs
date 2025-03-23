using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

public class OilSplashandfriendchicken : ActionTask
{
    // Chicken cooking parameters
    public BBParameter<bool> isChickenRaw; // Is the chicken raw?
    public BBParameter<bool> isBurnt; // Is the chicken burnt?
    public BBParameter<float> cookTime = 5f; // Total time to cook the chicken
    public BBParameter<Color> cookedColor; // Final color when fully cooked
    public BBParameter<GameObject> chickenObject; // Chicken's GameObject

    // Oil splash parameters
    public BBParameter<bool> isOilSplashing; // Is oil currently splashing?
    public BBParameter<float> moveDistance = 2f; // Distance to move backwards
    public BBParameter<float> moveSpeed = 3f; // Speed to move backwards
    public BBParameter<GameObject> sophieObject; // Sophie's GameObject
    public BBParameter<Color> lightRedColor; // Color for Sophie when hit
    public BBParameter<float> waitTime = 2f; // Time to wait after getting hit

    // Timers
    private float cookTimer;
    private float oilSplashTimer;
    private const float oilSplashInterval = 3f; // Oil splashes every 3 seconds

    // References
    private Renderer chickenRenderer;
    private Color rawColor;
    private Renderer sophieRenderer;
    private Color sophieOriginalColor;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    protected override void OnExecute()
    {
        // Initialize chicken cooking
        cookTimer = 0f;
        chickenRenderer = chickenObject.value.GetComponent<Renderer>();
        if (chickenRenderer != null)
        {
            rawColor = chickenRenderer.material.color; // Save the raw chicken color
        }

        // Initialize oil splash
        oilSplashTimer = 0f;
        sophieRenderer = sophieObject.value.GetComponent<Renderer>();
        if (sophieRenderer != null)
        {
            sophieOriginalColor = sophieRenderer.material.color;
        }
    }

    protected override void OnUpdate()
    {
        // Handle chicken cooking
        if (isChickenRaw.value)
        {
            cookTimer += Time.deltaTime;

            if (cookTimer >= cookTime.value)
            {
                // Chicken is fully cooked
                Debug.Log("Chicken is fully cooked!");
                isBurnt.value = false; // Ensure chicken is not burnt
                isChickenRaw.value = false; // Set chicken to not raw
                if (chickenRenderer != null)
                {
                    chickenRenderer.material.color = cookedColor.value; // Set to fully cooked color
                }
            }
            else if (isBurnt.value)
            {
                // Chicken is burnt
                Debug.Log("Chicken is burnt! Failure.");
                EndAction(false); // Failure
                return;
            }
            else
            {
                // Gradually change the chicken's color
                if (chickenRenderer != null)
                {
                    float progress = cookTimer / cookTime.value; // Calculate cooking progress (0 to 1)
                    chickenRenderer.material.color = Color.Lerp(rawColor, cookedColor.value, progress);
                }
            }
        }

        // Handle oil splash events
        oilSplashTimer += Time.deltaTime;

        // Trigger oil splash every 3 seconds
        if (oilSplashTimer >= oilSplashInterval)
        {
            oilSplashTimer = 0f; // Reset the timer
            isOilSplashing.value = true; // Set oil splashing to true
            Debug.Log("Oil is splashing!");
        }

        if (isOilSplashing.value)
        {
            // 50% chance to avoid the oil
            if (Random.Range(0, 2) == 0)
            {
                Debug.Log("Sophie is avoiding the oil!");
                startPosition = agent.transform.position;
                targetPosition = startPosition - agent.transform.forward * moveDistance.value;
                isOilSplashing.value = false; // Reset oil splashing
            }
            else
            {
                Debug.Log("Sophie got hit by the oil!");
                HandleOilHit();
                isOilSplashing.value = false; // Reset oil splashing
            }
        }

        // Move Sophie backwards if avoiding oil
        if (Vector3.Distance(agent.transform.position, targetPosition) > 0.1f)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, targetPosition, moveSpeed.value * Time.deltaTime);
        }

        // End the task if the chicken is fully cooked
        if (!isChickenRaw.value)
        {
            EndAction(true); // Success
        }
    }

    private void HandleOilHit()
    {
        // Change Sophie's color to light red
        if (sophieRenderer != null)
        {
            sophieRenderer.material.color = lightRedColor.value;
        }

        // Wait for 2 seconds
        StartCoroutine(WaitAndReset());
    }

    private System.Collections.IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(waitTime.value);

        // Reset Sophie's color
        if (sophieRenderer != null)
        {
            sophieRenderer.material.color = sophieOriginalColor;
        }

        Debug.Log("Sophie's color reset after getting hit.");
    }
}