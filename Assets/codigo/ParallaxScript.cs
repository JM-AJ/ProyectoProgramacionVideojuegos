using UnityEngine;



public class ParallaxScript : MonoBehaviour
{
    public Transform cameraTransform;

    private Transform[] layers;
    private Vector3 lastCameraPosition;

    public float[] parallaxMultipliersX = { 0.1f, 0.3f, 0.6f };
    public float[] parallaxMultipliersY = { 0.05f, 0.1f, 0.2f }; // Más sutil que X

    void Start()
    {
        lastCameraPosition = cameraTransform.position;

        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
    }

    void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - lastCameraPosition;

        for (int i = 0; i < layers.Length; i++)
        {
            float multiplierX = i < parallaxMultipliersX.Length ? parallaxMultipliersX[i] : 0.5f;
            float multiplierY = i < parallaxMultipliersY.Length ? parallaxMultipliersY[i] : 0.1f;

            layers[i].position += new Vector3(delta.x * multiplierX, delta.y * multiplierY, 0);
        }

        lastCameraPosition = cameraTransform.position;
    }
}