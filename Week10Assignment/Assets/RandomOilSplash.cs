using NodeCanvas.Framework;
using UnityEngine;

public class RandomOilSplash : ActionTask
{
    public BBParameter<bool> oilSplashing; 
    public BBParameter<float> minInterval;
    public BBParameter<float> maxInterval;
    public BBParameter<float> splashDuration;

    private float nextSplashTime;
    private bool isSplashing;

    protected override void OnExecute()
    {
        nextSplashTime = Time.time + Random.Range(minInterval.value, maxInterval.value);
        isSplashing = false;
    }

    protected override void OnUpdate()
    {
        if (!isSplashing && Time.time >= nextSplashTime)
        {
            oilSplashing.value = true;
            isSplashing = true;
            nextSplashTime = Time.time + splashDuration.value; // Oil splashing duration
        }
        else if (isSplashing && Time.time >= nextSplashTime)
        {
            oilSplashing.value = false;
            isSplashing = false;
            nextSplashTime = Time.time + Random.Range(minInterval.value, maxInterval.value); // Next random splash
        }
    }
}
