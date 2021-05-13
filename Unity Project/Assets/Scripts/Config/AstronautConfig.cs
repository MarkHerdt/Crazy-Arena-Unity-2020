using UnityEngine;

[CreateAssetMenu(menuName = "AstronautConfig", fileName = "AstronautConfig ")]
public class AstronautConfig : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 250f;
    [Tooltip("Smoothes the Time, the Player rotates with")]
    [SerializeField] private float smoothRotationTime = .0375f;

    [Header("Attack")]
    [Tooltip("Prefab for Ultimate")]
    [SerializeField] private GameObject ultimateEffect;
    [Tooltip("Prefab for the Attack")]
    [SerializeField] private GameObject attackEffect;
    [Tooltip("Breaks/Unbreaks an Object consecutively (For testing)")]
    [SerializeField] private bool attackTest = false;
    [Tooltip("Ragnge (Radius), of the Attack")]
    [SerializeField] private float attackRange = 37.5f;
    [Tooltip("Angle infront of the Player, at which the Player hits with an attack (Angle = Value * 2)")]
    [Range(0, 90)]
    [SerializeField] private float attackAngle;
    [Tooltip("Time in seconds, the Player has to wait, to use the next attack")]
    [SerializeField] private float attackCooldown = 0.1825f;
    [Tooltip("Stop moving for a set duration, after each attack")]
    [SerializeField] private bool stopWhileAttacking = true;
    [Tooltip("Time in seconds, the Player stops moving after each attack")]
    [SerializeField] private float delay = 0.05f;

    [Header("Barricade")]
    [Tooltip("Time in seconds, the Player has to wait to use the next Barricade")]
    [SerializeField] private float barricadeCooldown = 0f;
    [Tooltip("Time in seconds, the Barricade stays up")]
    [SerializeField] private float barricadeDuration = 10f;
    [Tooltip("Player is able to walk through all Barricades")]
    [SerializeField] private bool walkThroughBarricades = false;
    [Tooltip("Player can reset Barricades (won't reset the cooldown)")]
    [SerializeField] private bool resetBarricades = true;

    public float MovementSpeed { get => movementSpeed; }
    public float SmoothRotationTime { get => smoothRotationTime; }
    public GameObject UltimateEffect { get => ultimateEffect; }
    public GameObject AttackEffect { get => attackEffect; }
    public bool AttackTest { get => attackTest; }
    public float AttackRange { get => attackRange; }
    public float AttackAngle { get => attackAngle; }
    public float AttackCooldown { get => attackCooldown; }
    public bool StopWhileAttacking { get => stopWhileAttacking; }
    public float Delay { get => delay; }
    public float BarricadeCooldown { get => barricadeCooldown; }
    public float BarricadeDuration { get => barricadeDuration; }
    public bool WalkThroughBarricades { get => walkThroughBarricades; }
    public bool ResetBarricades { get => resetBarricades; }
}