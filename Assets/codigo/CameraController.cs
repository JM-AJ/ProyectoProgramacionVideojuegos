using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform objetive;
    public float cameraSpeed = 0.025f;
    public Vector3 movement;

    void LateUpdate()
    {
        Vector3 positionTo = objetive.position + movement;
        Vector3 positionSlow = Vector3.Lerp(transform.position, positionTo, cameraSpeed);

        transform.position = positionSlow;
    }
}
