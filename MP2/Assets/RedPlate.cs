using UnityEngine;
using TMPro;

public class RedPlate : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public TextMeshPro textDisplay;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {   
    if (!other.CompareTag("Player")) return;
        if (!other.CompareTag("Player")) return;
        if (activated) return;
        if (resourceManager == null) return;

        if (resourceManager.CanAfford(0f, 0f, 0f))
        {
            resourceManager.SpendMultiple(0f, 0f, 0f);
            resourceManager.StartRedGeneration();

            SpawnObject();
            activated = true;

            if (textDisplay != null)
                textDisplay.text = "Red Generator Activated!";
        }
        else
        {
            if (textDisplay != null)
                textDisplay.text = "Need 0 Red Liquid";
        }
    }

    void SpawnObject()
    {
        if (prefabToSpawn != null && spawnPoint != null)
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}