using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Attach to Red Beaker.
/// - Player must be within interactRange of the flask
/// - Press the assigned button (A or B on controller) to add red liquid
/// - Cooldown enforced between clicks, shown on a radial fill Image
/// - Locks out once the red generator is running
/// </summary>
public class FlaskInteract : MonoBehaviour
{
    [Header("References")]
    public ResourceManager resourceManager;
    public Transform playerTransform; // drag in your XR Origin or Camera Offset

    [Header("Input")]
    [Tooltip("Click the + and assign your A/B button action from your Input Action Asset")]
    public InputActionReference clickAction;

    [Header("Cooldown Radial Bar")]
    [Tooltip("Circular Image on HUD. Set Image Type=Filled, Fill Method=Radial 360, Fill Origin=Top")]
    public Image cooldownRadial;
    public Color readyColor = new Color(0.2f, 0.8f, 0.2f, 1f);
    public Color cooldownColor = new Color(0.9f, 0.3f, 0.1f, 1f);

    [Header("Settings")]
    public float clickAmount = 1f;
    public float cooldownDuration = 1.5f;
    public float interactRange = 2f; // metres from flask centre

    private float cooldownRemaining = 0f;
    private bool onCooldown = false;

    void OnEnable()
    {
        if (clickAction != null)
        {
            clickAction.action.Enable();
            clickAction.action.performed += OnButtonPressed;
        }
    }

    void OnDisable()
    {
        if (clickAction != null)
            clickAction.action.performed -= OnButtonPressed;
    }

    void Update()
    {
        // Count down cooldown
        if (onCooldown)
        {
            cooldownRemaining -= Time.deltaTime;
            if (cooldownRemaining <= 0f)
            {
                cooldownRemaining = 0f;
                onCooldown = false;
            }
        }

        // Update radial bar
        if (cooldownRadial != null)
        {
            if (resourceManager != null && resourceManager.generatingRed)
            {
                // Generator running — hide bar
                cooldownRadial.fillAmount = 0f;
                cooldownRadial.color = new Color(0.4f, 0.4f, 0.4f, 0.3f);
            }
            else if (onCooldown)
            {
                cooldownRadial.fillAmount = cooldownRemaining / cooldownDuration;
                cooldownRadial.color = cooldownColor;
            }
            else
            {
                cooldownRadial.fillAmount = 1f;
                cooldownRadial.color = readyColor;
            }
        }
    }

    void OnButtonPressed(InputAction.CallbackContext ctx)
    {
        if (resourceManager == null) return;

        // Lock out once red generator is active
        if (resourceManager.generatingRed) return;

        // Block if on cooldown
        if (onCooldown) return;

        // Check player is within range
        if (playerTransform == null) return;
        float dist = Vector3.Distance(playerTransform.position, transform.position);
        if (dist > interactRange) return;

        // Add liquid and start cooldown
        resourceManager.redLiquid += clickAmount;
        onCooldown = true;
        cooldownRemaining = cooldownDuration;
    }
}