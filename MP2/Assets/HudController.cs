using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Controls the HUD display for all three liquid resources.
/// Each resource has a colored circle (Image) and a text counter.
/// Circle is full-color when that liquid is being generated, gray otherwise.
/// Red circle is always visible since red is always the primary resource.
/// </summary>
public class HUDController : MonoBehaviour
{
    [Header("Resource Manager")]
    public ResourceManager manager;

    [Header("Text Counters")]
    public TextMeshProUGUI redText;
    public TextMeshProUGUI blueText;
    public TextMeshProUGUI greenText;

    [Header("Cooldown Text")]
    public TextMeshProUGUI cooldownText; // optional, can leave empty if handled in FlaskInteract

    [Header("Circle Icons")]
    public Image redCircle;
    public Image blueCircle;
    public Image greenCircle;

    [Header("Circle Colors")]
    public Color redActiveColor = new Color(1f, 0.15f, 0.15f, 1f);
    public Color blueActiveColor = new Color(0f, 0.4f, 1f, 1f);
    public Color greenActiveColor = new Color(0f, 1f, 0.3f, 1f);
    public Color inactiveColor = new Color(0.4f, 0.4f, 0.4f, 1f);

    void Update()
    {
        if (manager == null) return;

        // --- Numbers ---
        if (redText != null) redText.text = ": " + manager.redLiquid.ToString("F0");
        if (blueText != null) blueText.text = ": " + manager.blueLiquid.ToString("F0");
        if (greenText != null) greenText.text = ": " + manager.greenLiquid.ToString("F0");

        // --- Circle Colors ---
        // Red is always active (player can always see red resource)
        if (redCircle != null) redCircle.color = redActiveColor;

        if (blueCircle != null) blueCircle.color = manager.generatingBlue ? blueActiveColor : inactiveColor;
        if (greenCircle != null) greenCircle.color = manager.generatingGreen ? greenActiveColor : inactiveColor;
    }
}