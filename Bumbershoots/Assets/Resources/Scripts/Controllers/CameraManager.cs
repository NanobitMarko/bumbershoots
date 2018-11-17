using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private static CameraManager instance;

    public static CameraManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (target == null) return;
        Vector3 desiredPosition = new Vector3(0f, target.position.y, -10f) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(new Vector3(0f, transform.position.y, -10f), desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
    