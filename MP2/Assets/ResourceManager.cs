using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceManager : MonoBehaviour
{

    public Animator cameraAnimator;
    // =============================
    // LIQUID AMOUNTS
    // =============================
    public float redLiquid = 0f;
    public float blueLiquid = 0f;
    public float greenLiquid = 0f;

    // =============================
    // GENERATION RATES
    // =============================
    public float redRate = 10f;
    public float blueRate = 10f;
    public float greenRate = 10f;

    // =============================
    // GENERATION STATES
    // =============================
    public bool generatingRed = false;
    public bool generatingBlue = false;
    public bool generatingGreen = false;

    // =============================
    // PARTICLES
    // =============================
    public ParticleSystem redParticles;
    public ParticleSystem blueParticles;
    public ParticleSystem greenParticles;





    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            redLiquid += 10f;
            Debug.Log("Add red liquid: " + redLiquid);
        }

        // Red generation
        if (generatingRed)
        {
            redLiquid += redRate * Time.deltaTime;

            if (redParticles != null)
            {
                var emission = redParticles.emission;
                emission.rateOverTime = redRate * 2f;

                if (!redParticles.isPlaying)
                    redParticles.Play();
            }
        }
        else
        {
            if (redParticles != null && redParticles.isPlaying)
                redParticles.Stop();
        }

        // Blue generation
        if (generatingBlue)
        {
            blueLiquid += blueRate * Time.deltaTime;

            if (blueParticles != null)
            {
                var emission = blueParticles.emission;
                emission.rateOverTime = blueRate * 2f;

                if (!blueParticles.isPlaying)
                    blueParticles.Play();
            }
        }
        else
        {
            if (blueParticles != null && blueParticles.isPlaying)
                blueParticles.Stop();
        }

        // Green generation
        if (generatingGreen)
        {
            greenLiquid += greenRate * Time.deltaTime;

            if (greenParticles != null)
            {
                var emission = greenParticles.emission;
                emission.rateOverTime = greenRate * 2f;

                if (!greenParticles.isPlaying)
                    greenParticles.Play();
            }
        }
        else
        {
            if (greenParticles != null && greenParticles.isPlaying)
                greenParticles.Stop();
        }

        // Cap all resources at 150
        redLiquid = Mathf.Min(redLiquid, 150f);
        blueLiquid = Mathf.Min(blueLiquid, 150f);
        greenLiquid = Mathf.Min(greenLiquid, 150f);
    }

    // =============================
    // START GENERATION
    // =============================
    public void StartRedGeneration()
    {
        generatingRed = true;
        Debug.Log("Red generation started");

        UpdateCameraAnimation();
    }

    public void StartBlueGeneration()
    {
        generatingBlue = true;
        if (blueRate == 0) blueRate = 10f;
        Debug.Log("Blue generation started");

        UpdateCameraAnimation();
    }

    public void StartGreenGeneration()
    {
        generatingGreen = true;
        if (greenRate == 0) greenRate = 10f;
        Debug.Log("Green generation started");

        UpdateCameraAnimation();
    }

    public void UpdateCameraAnimation()
    {
        if (cameraAnimator == null) return;

        int activeCount = 0;

        if (generatingRed) activeCount++;
        if (generatingBlue) activeCount++;
        if (generatingGreen) activeCount++;

        // This will send 1, 2, or 3 to the Animator based on how many plates are pressed
        cameraAnimator.SetInteger("GenLevel", activeCount);

        // FIX 2: This will tell us if the math is actually working
        Debug.Log("⚙️ Attempting to set GenLevel Animator parameter to: " + activeCount);

        // FIX 3: Applies the integer to the Animator
        cameraAnimator.SetInteger("GenLevel", activeCount);
    }

    // =============================
    // UPGRADE RATES
    // =============================
    public void AddRedRate(float amount) { redRate += amount; }
    public void AddBlueRate(float amount) { blueRate += amount; }
    public void AddGreenRate(float amount) { greenRate += amount; }

    // =============================
    // SPENDING METHODS
    // =============================
    public bool SpendRed(float amount)
    {
        if (redLiquid >= amount) { redLiquid -= amount; return true; }
        return false;
    }

    public bool SpendBlue(float amount)
    {
        if (blueLiquid >= amount) { blueLiquid -= amount; return true; }
        return false;
    }

    public bool SpendGreen(float amount)
    {
        if (greenLiquid >= amount) { greenLiquid -= amount; return true; }
        return false;
    }

    // =============================
    // MULTI-LIQUID CHECK
    // =============================
    public bool CanAfford(float redCost, float blueCost, float greenCost)
    {
        return redLiquid >= redCost &&
               blueLiquid >= blueCost &&
               greenLiquid >= greenCost;
    }

    public void SpendMultiple(float redCost, float blueCost, float greenCost)
    {
        redLiquid -= redCost;
        blueLiquid -= blueCost;
        greenLiquid -= greenCost;
    }


}