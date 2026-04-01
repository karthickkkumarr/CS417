using UnityEngine;
using TMPro;

public class GreenPlate : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public TextMeshPro textDisplay;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (activated) return;
        if (resourceManager == null) return;

        if (resourceManager.CanAfford(75f, 75f, 0f))
        {
            resourceManager.SpendMultiple(75f, 75f, 0f);
            resourceManager.StartGreenGeneration();

            SpawnObject();
            activated = true;

            if (textDisplay != null)
                textDisplay.text = "Green Generator Activated!";
        }
        else
        {
            if (textDisplay != null)
                textDisplay.text = "Need 140 Red + 140 Blue";
        }
    }

    void SpawnObject()
    {
        if (prefabToSpawn != null && spawnPoint != null)
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}