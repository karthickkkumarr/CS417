using UnityEngine;

/// <summary>
/// Monitors resource counters and instantiates trophy prefabs at their spawn points
/// when thresholds are crossed. Trophies only appear once.
/// 
/// SETUP in Inspector:
///   - Set howm many trophies you want (at least 3)
///   - For each trophy entry: assign prefab, spawn point, which resource to watch, and threshold
/// </summary>
public class AchievementManager : MonoBehaviour
{
    public ResourceManager resourceManager;

    [System.Serializable]
    public class TrophyEntry
    {
        public string label;                  // just for your reference in the Inspector
        public GameObject trophyPrefab;       // the 3D trophy object to spawn
        public Transform spawnPoint;          // where to instantiate it
        public ResourceType resource;         // which liquid to watch
        public float threshold;               // amount needed to trigger

        [HideInInspector] public bool triggered = false;
    }

    public enum ResourceType { Red, Blue, Green }

    [Header("Trophy Definitions (set at least 3)")]
    public TrophyEntry[] trophies;

    void Update()
    {
        if (resourceManager == null) return;

        foreach (var trophy in trophies)
        {
            if (trophy.triggered) continue;
            if (trophy.trophyPrefab == null || trophy.spawnPoint == null) continue;

            float current = GetAmount(trophy.resource);
            if (current >= trophy.threshold)
            {
                Instantiate(trophy.trophyPrefab, trophy.spawnPoint.position, trophy.spawnPoint.rotation);
                trophy.triggered = true;
                Debug.Log($"[Achievement] '{trophy.label}' unlocked at {trophy.resource} >= {trophy.threshold}");
            }
        }
    }

    float GetAmount(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Red: return resourceManager.redLiquid;
            case ResourceType.Blue: return resourceManager.blueLiquid;
            case ResourceType.Green: return resourceManager.greenLiquid;
            default: return 0f;
        }
    }
}