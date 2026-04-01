using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceManager : MonoBehaviour
{
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

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            redLiquid += 10f;
            Debug.Log("Add red liquid: " + redLiquid);
        }

        if (generatingRed)
            redLiquid += redRate * Time.deltaTime;

        if (generatingBlue)
            blueLiquid += blueRate * Time.deltaTime;

        if (generatingGreen)
            greenLiquid += greenRate * Time.deltaTime;

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
    }

    public void StartBlueGeneration()
    {
        generatingBlue = true;
        if (blueRate == 0) blueRate = 10f;
    }

    public void StartGreenGeneration()
    {
        generatingGreen = true;
        if (greenRate == 0) greenRate = 10f;
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