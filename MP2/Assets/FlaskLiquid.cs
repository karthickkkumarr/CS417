using UnityEngine;

public class FlaskLiquid : MonoBehaviour
{
    public enum LiquidType { Red, Blue, Green }
    public LiquidType liquidType;
    public ResourceManager manager;
    public float maxForFull = 150f;

    [Tooltip("Y position of the bottom of the flask in local space.")]
    public float bottomLocalY = -0.145f;

    [Tooltip("Y position of the top of the flask in local space.")]
    public float topLocalY = 0.145f;

    private float originalScaleX;
    private float originalScaleY;
    private float originalScaleZ;

    void Start()
    {
        originalScaleX = transform.localScale.x;
        originalScaleY = transform.localScale.y;
        originalScaleZ = transform.localScale.z;

        SetLiquidHeight(0f);
    }

    void Update()
    {
        if (manager == null) return;

        float currentAmount = 0f;
        switch (liquidType)
        {
            case LiquidType.Red: currentAmount = manager.redLiquid; break;
            case LiquidType.Blue: currentAmount = manager.blueLiquid; break;
            case LiquidType.Green: currentAmount = manager.greenLiquid; break;
        }

        float percent = Mathf.Clamp01(currentAmount / maxForFull);
        SetLiquidHeight(percent);
    }

    void SetLiquidHeight(float percent)
    {
        float totalHeight = topLocalY - bottomLocalY;
        float newHeight = Mathf.Max(totalHeight * percent, 0.0001f);

        // Scale Y proportionally
        float scaleRatio = newHeight / totalHeight;
        transform.localScale = new Vector3(
            originalScaleX,
            originalScaleY * scaleRatio,
            originalScaleZ
        );

        // Cylinder pivot is at center, anchor bottom to bottomLocalY
        float centerY = bottomLocalY + (newHeight / 2f);
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            centerY,
            transform.localPosition.z
        );
    }
}