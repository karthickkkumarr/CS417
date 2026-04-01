using UnityEngine;
using TMPro;

public class PressurePlate2 : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public ResourceManager resourceManager;

    public float requiredRed = 100f;
    public float upgradeAmount = 2f;

    public TextMeshPro textDisplay;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (resourceManager == null) return;

        // Not enough red liquid
        if (resourceManager.redLiquid < requiredRed)
        {
            if (textDisplay != null)
                textDisplay.text = "Need 100 Red Liquid";
            return;
        }

        // Spend red liquid
        resourceManager.SpendRed(requiredRed);

        // FIRST TIME → Build generator
        if (!hasSpawned)
        {
            SpawnObject();
            resourceManager.StartRedGeneration();
            hasSpawned = true;

            if (textDisplay != null)
                textDisplay.text = "Red Generator Built!";
        }
        else
        {
            // Upgrade
            resourceManager.AddRedRate(upgradeAmount);

            if (textDisplay != null)
                textDisplay.text = "Upgraded! +" + upgradeAmount + " Red Rate";
        }
    }

    void SpawnObject()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}