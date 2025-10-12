using UnityEngine;

public class StumpFall : MonoBehaviour
{
    public float fallenThreshold = 60f;

    void Update()
    {
        float tiltAngle = Vector3.Angle(Vector3.up, transform.up);
        if (tiltAngle > fallenThreshold)
        {
            Debug.Log(gameObject.name + " has fallen!");
            StumpManager.Instance.OnStumpFallen(this);
        }
    }
}
