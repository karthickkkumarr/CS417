using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public Animator cameraAnimator;

    // Call this from your Pressure Plate's OnEnter event
    public void SetCameraState(int stateIndex)
    {
        // Reset all to false first for a clean transition
        cameraAnimator.SetBool("IsEntry", false);
        cameraAnimator.SetBool("IsGen2", false);
        cameraAnimator.SetBool("IsGen3", false);

        // Switch based on the plate stepped on
        switch (stateIndex)
        {
            case 1:
                cameraAnimator.SetBool("IsEntry", true);
                break;
            case 2:
                cameraAnimator.SetBool("IsGen2", true);
                break;
            case 3:
                cameraAnimator.SetBool("IsGen3", true);
                break;
        }
    }
}