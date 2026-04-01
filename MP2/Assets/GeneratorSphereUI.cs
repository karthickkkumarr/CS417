using UnityEngine;
using UnityEngine.UI;

public class GeneratorSphereUI : MonoBehaviour
{
    public ResourceManager manager;

    public Image blueSphere;
    public Image greenSphere;

    private Color grayColor = new Color(0.4f, 0.4f, 0.4f, 1f);
    private Color blueColor = new Color(0f, 0.4f, 1f, 1f);
    private Color greenColor = new Color(0f, 1f, 0.3f, 1f);

    void Update()
    {
        if (manager == null) return;

        // Blue sphere
        if (manager.generatingBlue)
            blueSphere.color = blueColor;
        else
            blueSphere.color = grayColor;

        // Green sphere
        if (manager.generatingGreen)
            greenSphere.color = greenColor;
        else
            greenSphere.color = grayColor;
    }
}