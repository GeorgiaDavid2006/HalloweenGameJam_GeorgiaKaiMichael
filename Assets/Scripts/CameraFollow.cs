using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform PlayerTransform;

    [SerializeField] public Vector3 CameraOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerTransform = PlayerController.Instance.transform;
    }

    private void FixedUpdate()
    {
        transform.position = PlayerTransform.position + CameraOffset;
    }
}
