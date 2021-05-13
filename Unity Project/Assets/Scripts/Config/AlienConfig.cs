using UnityEngine;

[CreateAssetMenu(menuName = "AlienConfig", fileName = "AlienConfig")]
public class AlienConfig : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 250f;
    [Tooltip("Smoothes the Time, the Player rotates with")]
    [SerializeField] private float smoothRotationTime = .0375f;

    [Header("Bullet")]
    [Tooltip("Prefab for Ultimate")]
    [SerializeField] private GameObject ultimateEffect;
    [Tooltip("Prefab for the Attack")]
    [SerializeField] private GameObject alienBulletPrefab;
    [Tooltip("Breaks/Unbreaks an Object consecutively (For testing)")]
    [SerializeField] private bool attackTest = false;
    [SerializeField] private float bulletSpeed = 50000f;
    [Tooltip("Value must be negative")]
    [SerializeField] private float bulletGravity = -187500f;
    [Tooltip("Time in seconds, the Player has to wait, to use the next attack")]
    [SerializeField] private float bulletCooldown = .365f;
    [SerializeField] private float explosionRadius = 37.5f;
    [Tooltip("Stop moving for a set duration, after each attack")]
    [SerializeField] private bool stopWhileAttacking = true;
    [Tooltip("Time in seconds, the Player stops moving after each attack")]
    [SerializeField] private float delay = .05f;

    [Header("Bullet Rotationspeed")]
    [SerializeField] private float xRotation = 100f;
    [SerializeField] private float yRotation = 100f;
    [SerializeField] private float zRotation = 100f;

    [Header("Teleport")]
    [Tooltip("Teleport Prefab")]
    [SerializeField] private GameObject teleportEffect;
    [Tooltip("Time in seconds, the Player has to wait to use the next Teleport")]
    [SerializeField] private float teleportCooldown = 30f;
    [Header("Teleport Types")]
    [Tooltip("Teleports the Player to the closest Teleport room in the direction the Player is looking")]
    [SerializeField] private bool lookDirection = false;
    [Tooltip("Teleports the Player to the other side of the Barriacde the Player is looking at")]
    [SerializeField] private bool blink = false;
    [Tooltip("Teleports the Player to a fixed Position")]
    [SerializeField] private bool fixedPoint = false;
    [Tooltip("Position, the Player will be Teleported to")]
    [SerializeField] private Vector3 teleportPosition = new Vector3(425f, 22.5f, 25f);

    private void OnValidate()
    {
        // Allows only one Checkbox to be active
        if (lookDirection)
        {
            blink = false;
            fixedPoint = false;
        }
        if (fixedPoint)
        {
            lookDirection = false;
            blink = false;
        }
        if (blink)
        {
            lookDirection = false;
            fixedPoint = false;
        }

    }

    [Header("Test")]
    [SerializeField] private GameObject redSphere;
    [SerializeField] private GameObject greenSphere;
    [SerializeField] private GameObject blueSphere;

    public float MovementSpeed { get => movementSpeed; }
    public float SmoothRotationTime { get => smoothRotationTime; }
    public GameObject UltimateEffect { get => ultimateEffect; }
    public GameObject AlienBulletPrefab { get => alienBulletPrefab; }
    public bool AttackTest { get => attackTest; }
    public float BulletSpeed { get => bulletSpeed; }
    public float BulletGravity { get => bulletGravity; }
    public float BulletCooldown { get => bulletCooldown; }
    public float ExplosionRadius { get => explosionRadius; }
    public bool StopWhileAttacking { get => stopWhileAttacking; }
    public float Delay { get => delay; }
    public float XRotation { get => xRotation; }
    public float YRotation { get => yRotation; }
    public float ZRotation { get => zRotation; }
    public GameObject TeleportEffect { get => teleportEffect; }
    public float TeleportCooldown { get => teleportCooldown; }
    public bool LookDirection { get => lookDirection; }
    public bool Blink { get => blink; }
    public bool FixedPoint { get => fixedPoint; }
    public Vector3 TeleportPosition { get => teleportPosition; }
    public GameObject RedSphere { get => redSphere; }
    public GameObject GreenSphere { get => greenSphere; }
    public GameObject BlueSphere { get => blueSphere; }
}
