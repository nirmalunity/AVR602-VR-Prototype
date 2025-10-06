using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    public Transform xrCamera;
    public float distance = 2f;

    void LateUpdate()
    {
        transform.position = xrCamera.position + xrCamera.forward * distance;

        transform.rotation = Quaternion.LookRotation(transform.position - xrCamera.position);
    }
}
