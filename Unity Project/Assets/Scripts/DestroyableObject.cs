using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private GameObject astroanutIcon;
    [SerializeField] private GameObject alienIcion;
    //BoxCollider[] collider;
    //private float colliderx;
    //private float colliderz;
    //private float colliderHeight = 0;


    [SerializeField] private GameObject unbrokenPrefab;
    [SerializeField] private GameObject brokenPrefab;
    [SerializeField] private GameObject unBreakEffect;
    [SerializeField] private GameObject breakEffect;

    [Tooltip("The amount of points the Player gets, when breaking/unbreaking this Object")]
    [SerializeField] private int points = 50;

    private bool instantiated = false;
    Rigidbody rb;
    public bool IsDestroyed { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        RigidBodySettings();

        //collider = GetComponentsInChildren<BoxCollider>();

        //foreach (BoxCollider collider in collider)
        //{
        //    collider.size = new Vector3(collider.size.x, collider.size.y * 5, collider.size.z);
        //}

        //if (test != null)
        //{
        //    test.transform.localPosition = new Vector3(colliderx, colliderHeight, colliderz);
        //}

        unbrokenPrefab.SetActive(true);
        brokenPrefab.SetActive(false);

        astroanutIcon.SetActive(false);
        alienIcion.SetActive(false);
    }

    private void Start()
    {
        GameController.gameController.DestroyableObjectCount++;


        unbrokenPrefab.SetActive(true);
        brokenPrefab.SetActive(false);

        astroanutIcon.SetActive(false);
        alienIcion.SetActive(false);
        //Unbreak();

        EventController.OnEndMatch += HideIcons;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        EventController.OnEndMatch -= HideIcons;
    }

    /// <summary>
    /// Applies all RigidBody settings needed for this Object
    /// </summary>
    private void RigidBodySettings()
    {
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// Sets the Object to its "unbroken" state
    /// </summary>
    public void Unbreak()
    {
        astroanutIcon.SetActive(false);
        alienIcion.SetActive(true);

        unbrokenPrefab.SetActive(true);
        brokenPrefab.SetActive(false);

        if (IsDestroyed)
        {
            if (instantiated)
            {
                SoundController.PlaySound(SoundController.Sound.Hit, transform.position);

                EventController.ObjectUnBreak(points);
                Instantiate(unBreakEffect, transform.position, Quaternion.identity);
            }
        }

        IsDestroyed = false;
        instantiated = true;
    }

    /// <summary>
    /// Sets the Object to its "broken" state
    /// </summary>
    public void Break()
    {
        astroanutIcon.SetActive(true);
        alienIcion.SetActive(false);

        unbrokenPrefab.SetActive(false);
        brokenPrefab.SetActive(true);

        if (!IsDestroyed)
        {
            if (instantiated)
            {
                SoundController.PlaySound(SoundController.Sound.Hit, transform.position);

                EventController.ObjectBreak(points);
                Instantiate(breakEffect, transform.position, Quaternion.identity);
            }
        }

        IsDestroyed = true;
        instantiated = true;
    }

    /// <summary>
    /// Breaks/Unbreaks the Object on each hit (just for testing)
    /// </summary>
    public void AttackTest()
    {
        if (IsDestroyed)
        {
            Unbreak();
        }
        else
        {
            Break();
        }
    }

    private void HideIcons()
    {
        astroanutIcon.SetActive(false);
        alienIcion.SetActive(false);
    }
}