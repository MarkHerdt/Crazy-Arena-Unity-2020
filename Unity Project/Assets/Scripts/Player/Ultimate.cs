using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public bool Astronaut { get; set; }
    public bool Alien { get; set; }

    private float speed = 250;
    private float size = 100;

    private void OnTriggerEnter(Collider other)
    {
        DestroyableObject obj = other.GetComponentInParent<DestroyableObject>();

        if (obj != null)
        {
            if (Astronaut && obj.IsDestroyed)
            {
                obj.Unbreak();
            }
            else if (Alien && !obj.IsDestroyed)
            {
                obj.Break();
            }
        }
    }

    void Update()
    {
        transform.localScale += new Vector3(1, 1, 1) * GameController.gameController.GameConfig.Speed * Time.deltaTime;
        if (transform.localScale.x >= GameController.gameController.GameConfig.UltimateRadius || transform.localScale.y >= GameController.gameController.GameConfig.UltimateRadius || transform.localScale.z >= GameController.gameController.GameConfig.UltimateRadius)
        {
            Destroy(this.gameObject);
        }
    }
}
