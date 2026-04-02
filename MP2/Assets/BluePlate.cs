using UnityEngine;
using TMPro;

public class BluePlate : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameObject prefabToSpawn;
    public Transform spawnPoint;
    public TextMeshPro textDisplay;
    public AudioSource unlockSound;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (activated) return;
        if (resourceManager == null) return;

        if (resourceManager.CanAfford(75f, 0f, 0f))
        {
            resourceManager.SpendMultiple(75f, 0f, 0f);
            resourceManager.StartBlueGeneration();

            SpawnObject();
            activated = true;

            if (unlockSound != null)
                unlockSound.Play();

            if (textDisplay != null)
                textDisplay.text = "Blue Generator Activated!";
        }
        else
        {
            if (textDisplay != null)
                textDisplay.text = "Need 140 Red Liquid";
        }
    }

    void SpawnObject()
    {
        if (prefabToSpawn != null && spawnPoint != null)
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}