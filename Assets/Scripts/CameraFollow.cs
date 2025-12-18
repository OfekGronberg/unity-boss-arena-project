using UnityEngine;

// Makes CameraRig follow the player position (no rotation)
public class CameraFollow : MonoBehaviour
{
    // The object we want to follow (Player)
    public Transform target;

    void LateUpdate()
    {
        // LateUpdate runs after movement, so the camera feels smoother
        if (target == null) return;

        // Follow only the position (not rotation)
        transform.position = target.position;
    }
}
