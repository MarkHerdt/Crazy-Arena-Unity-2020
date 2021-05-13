using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    void Update()
    {
        transform.Rotate(X * Time.deltaTime, Y * Time.deltaTime, Z * Time.deltaTime);
    }
}
