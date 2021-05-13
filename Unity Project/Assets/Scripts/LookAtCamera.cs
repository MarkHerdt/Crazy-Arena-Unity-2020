using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        transform.LookAt(cam.transform, Vector3.forward);
    }
}
