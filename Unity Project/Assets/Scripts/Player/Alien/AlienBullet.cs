using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class AlienBullet : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticle;

    public AlienConfig config { get; set; }

    public GameObject Alien { get; set; }
    public float AlienHeight { get; set; }
    private Rigidbody rigidBody;

    public float BulletSpeed { get; set; }
    public float Gravity { get; set; }
    private Vector3 velocity;

    private SphereCollider sphereCollider;

    private bool hitSomething = false;
    private Vector3 hitPosition;
    public float ExplosionRadius { get; set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (rigidBody.useGravity)
        {
            rigidBody.useGravity = false;
        }

        sphereCollider = GetComponent<SphereCollider>();
        if (!sphereCollider.isTrigger)
        {
            sphereCollider.isTrigger = true;
        }
    }

    void Update()
    {
        // Sets the Velocity Vector
        velocity = Gravity * Vector3.up * Time.deltaTime; 
        // Moves the Bulet downwards
        rigidBody.AddForce(velocity, ForceMode.Acceleration);
        // Moves the Bullet forward
        rigidBody.velocity = transform.forward * BulletSpeed * Time.deltaTime;

        // Alos hits every "DestroyableObject underneath the Bullet"
        Physics.BoxCast(transform.position, new Vector3(sphereCollider.radius, AlienHeight, sphereCollider.radius), transform.forward, out RaycastHit hitInfo, transform.rotation);

        //TODO: NullReference

        //DestroyableObject hit;
        //if ((hit = hitInfo.transform.gameObject.GetComponent<DestroyableObject>()) != null)
        //{
        //    hit.Break();
        //}
    }

    private void OnTriggerEnter(Collider collision)
    {
        CheckForCollision(collision);
    }

    private void OnTriggerStay(Collider collision)
    {
        CheckForCollision(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        CheckForCollision(collision);
    }

    /// <summary>
    /// Checks if the Bullet didn't have any collisions already
    /// </summary>
    private void CheckForCollision(Collider collision)
    {
        if (gameObject != null && !hitSomething && collision.gameObject != Alien)
        {
            hitSomething = true;

            hitPosition = collision.transform.position;

            HitSomething();
        }
    }

    /// <summary>
    /// Is called when the Bullet collides with anything
    /// </summary>
    private void HitSomething()
    {
        SoundController.PlaySound(SoundController.Sound.AlienBulletExplosion, .5f, transform.position);

        // Checks if a "DestroyableObject" is inside the "explosionRadius"
        //RaycastHit[] hitInfo = Physics.SphereCastAll(transform.position, ExplosionRadius, transform.position, ExplosionRadius);
        Collider[] colliderInfo = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (Collider collider in colliderInfo)
        {
            DestroyableObject _destroyableObject = collider.transform.gameObject.GetComponentInParent<DestroyableObject>();

            // When the Object hit by "OverlapSphere" is a "DestroyableObject"
            if (_destroyableObject != null)
            {
                //Destroy(Instantiate(config.GreenSphere, hitPosition, Quaternion.identity), 10);
                //Debug.DrawLine(hitPosition, collider.transform.position, Color.green, 10);

                //Destroy(Instantiate(config.BlueSphere, transform.position, Quaternion.identity), 10);
                //Debug.DrawLine(transform.position, collider.transform.position, Color.blue, 10);

                // TODO: Collision with Wall not correctly recognized

                //RaycastHit[] hitInfo = Physics.RaycastAll(transform.position, _destroyableObject.transform.position, ExplosionRadius);
                ////Debug.Log(hitInfo.Length);

                //foreach (RaycastHit hit in hitInfo)
                //{
                //    Debug.Log(hit.transform.gameObject);
                //    Destroy(Instantiate(config.RedSphere, transform.position, Quaternion.identity), 10);
                //    Debug.DrawLine(transform.position, hit.point, Color.red, 10);
                //}

                if (config.AttackTest)
                {
                    _destroyableObject.AttackTest();
                }
                else
                {
                    _destroyableObject.Break();
                }
            }
        }

        GameObject explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(ExplosionRadius, ExplosionRadius, ExplosionRadius);

        Destroy(gameObject);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, ExplosionRadius);
    //}
}