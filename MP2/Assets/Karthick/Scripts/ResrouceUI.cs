using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public ResourceManager manager;

    public TextMeshProUGUI redText;
    public TextMeshProUGUI blueText;
    public TextMeshProUGUI greenText;

    void Update()
    {
        if (manager == null) return;

        redText.text = "Red Liquid: " + manager.redLiquid.ToString("F0");
        blueText.text = "Blue Liquid: " + manager.blueLiquid.ToString("F0");
        greenText.text = "Green Liquid: " + manager.greenLiquid.ToString("F0");
    }
}