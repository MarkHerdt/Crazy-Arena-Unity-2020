using UnityEngine;

public class RotateAroundMap : MonoBehaviour
{
    [SerializeField] private CharacterMenu characterMenu;
    [SerializeField] private GameObject level;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector3 rotatePosition;
    [SerializeField] private Vector3 rotateAngle;
 
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public bool AnimationPlaying { get; set; }
    private bool stopPosition;
    private bool stopRotation;
    public Vector3 StartPosition { get; set; }
    public Quaternion StartRotation { get; set; }

    private void Awake()
    {
        if (characterMenu == null)
        {
            characterMenu = FindObjectOfType<CharacterMenu>();
        }

        initialPosition = gameObject.transform.position;
        characterMenu.CameraPosition = initialPosition;
        initialRotation = gameObject.transform.rotation;
        characterMenu.CameraRotation = initialRotation;
    }

    private void Start()
    {
        transform.SetPositionAndRotation(new Vector3(rotatePosition.x, rotatePosition.y, rotatePosition.z), Quaternion.Euler(rotateAngle.x, rotateAngle.y, rotateAngle.z));
    }

    private void FixedUpdate()
    {
        if (!AnimationPlaying)
        {
            transform.RotateAround(level.gameObject.transform.position, Vector3.up, speed * Time.deltaTime);
        }

        if (AnimationPlaying && !stopPosition)
        {
            Vector3 direction = StartPosition - initialPosition;

            transform.position -= direction * 2.5f * Time.deltaTime;

            if (Vector3.Distance(transform.position, initialPosition) < 0.001f)
            {
                stopPosition = true;
            }
        }

        if (AnimationPlaying && !stopRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, 2.5f * Time.deltaTime);


            if (Vector3.Distance(transform.rotation.eulerAngles, initialRotation.eulerAngles) < .1f)
            {
                stopRotation = true;
                characterMenu.StartGame();
            }
        }
    }
}
