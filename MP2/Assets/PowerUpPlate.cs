using UnityEngine;
using TMPro;

/// <summary>
/// Attach to a plate in the scene for each generator (red, blue, green).
/// When the player steps on it, spends 25 of the matching liquid and
/// multiplies that generator's rate by 1.5x. Can only be purchased once.
/// </summary>
public class PowerUpPlate : MonoBehaviour
{
    public enum LiquidType { Red, Blue, Green }

    [Header("References")]
    public ResourceManager resourceManager;
    public TextMeshPro textDisplay;

    [Header("Settings")]
    public LiquidType liquidType;
    public float cost = 50f;
    public float multiplier = 3f;

    private bool purchased = false;

    void Start()
    {
        if (textDisplay != null)
        {
            textDisplay.text = "Power-Up: " + liquidType.ToString() + "\nCost: " + cost + " " + liquidType.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (purchased) return;
        if (resourceManager == null) return;

        // Check if generator is even active first
        if (!IsGeneratorActive())
        {
            if (textDisplay != null)
                textDisplay.text = "Activate " + liquidType.ToString() + " generator first!";
            return;
        }

        // Check if player can afford it
        if (!CanAfford())
        {
            if (textDisplay != null)
                textDisplay.text = "Need " + cost + " " + liquidType.ToString() + " liquid!";
            return;
        }

        // Spend the liquid and apply the multiplier
        SpendLiquid();
        ApplyMultiplier();
        purchased = true;

        if (textDisplay != null)
            textDisplay.text = liquidType.ToString() + " Power-Up Activated!\n+" + ((multiplier - 1f) * 100f) + "% rate";
    }

    bool IsGeneratorActive()
    {
        switch (liquidType)
        {
            case LiquidType.Red: return resourceManager.generatingRed;
            case LiquidType.Blue: return resourceManager.generatingBlue;
            case LiquidType.Green: return resourceManager.generatingGreen;
            default: return false;
        }
    }

    bool CanAfford()
    {
        switch (liquidType)
        {
            case LiquidType.Red: return resourceManager.redLiquid >= cost;
            case LiquidType.Blue: return resourceManager.blueLiquid >= cost;
            case LiquidType.Green: return resourceManager.greenLiquid >= cost;
            default: return false;
        }
    }

    void SpendLiquid()
    {
        switch (liquidType)
        {
            case LiquidType.Red: resourceManager.SpendRed(cost); break;
            case LiquidType.Blue: resourceManager.SpendBlue(cost); break;
            case LiquidType.Green: resourceManager.SpendGreen(cost); break;
        }
    }

    void ApplyMultiplier()
    {
        switch (liquidType)
        {
            case LiquidType.Red:
                resourceManager.redRate *= multiplier;
                break;
            case LiquidType.Blue:
                resourceManager.blueRate *= multiplier;
                break;
            case LiquidType.Green:
                resourceManager.greenRate *= multiplier;
                break;
        }
    }
}