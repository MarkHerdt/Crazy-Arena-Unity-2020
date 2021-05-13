using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private static int id = 0;
    public static int Id { get => id; set => id = value; }
    private int playerId = 0;

    public Animator animator { get; set; }

    [Tooltip("Character Controller Component")]
    [SerializeField] private CharacterController controller;
    public CharacterController Controller { get { return controller; } }

    [Tooltip("ScriptableObject Config File")]
    [SerializeField] private AstronautConfig astronautConfiguration;
    public AstronautConfig AstronautConfiguration { get { return astronautConfiguration; } }
    [Tooltip("ScriptableObject Config File")]
    [SerializeField] private AlienConfig alienConfiguration;
    public AlienConfig AlienConfiguration { get { return alienConfiguration; } }

    [Tooltip("GroundCheck Object in Player")]
    [SerializeField] private Transform groundCheck;
    [Tooltip("Ground Layer")]
    [SerializeField] private LayerMask groundLayer;

    private float movementSpeed = 15f;
    private float smoothRotationTime = .0375f;
    [Tooltip("Gravitational Force")]
    [SerializeField] private float gravity = -9.81f;
    [Tooltip("Radius of GroundCheck Sphere")]
    [SerializeField] private float groundCheckRadius = .5f;

    private float smoothRotationVelocity;
    private Vector2 inputVector = Vector2.zero;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }

        playerId = Id++;
    }

    /// <summary>
    /// Returns the Id of the Player
    /// </summary>
    /// <returns></returns>
    public int GetPlayerId()
    {
        return playerId;
    }

    /// <summary>
    /// Sets the movedirection Vector to the direction of the input
    /// </summary>
    /// <param name="direction"></param>
    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    void Update()
    {
        UpdatePlayerConfig();
        Move();
    }

    /// <summary>
    /// Moves the Player
    /// </summary>
    private void Move()
    {
        if (controller.enabled)
        {
            // Is true when the Sphere under the Player collides with something of the specified Layer
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

            // Resets the velocity while the Player is grounded
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = 0;
            }

            // Sets the Velocity Vector
            velocity.y += gravity * Time.deltaTime;
            // Moves the Player downwards
            controller.Move(velocity * Time.deltaTime);

            if (inputVector.magnitude > .1f)
            {
                //Animation
                animator.SetBool("isIdle", false);
                animator.SetBool("isMoving", true);

                // Faces the Player in the direction the Player is walking
                float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
                // Smoothes the rotation
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothRotationVelocity, smoothRotationTime);
                // Rotates the Player
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Sets the move direction to the direction the Player is looking at
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                // Moves the Player
                controller.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
            }
            else
            {
                if (animator != null)
                {
                    //Animation
                    animator.SetBool("isMoving", false);
                    animator.SetBool("isIdle", true);
                }
            }
        }
    }

    /// <summary>
    /// Disables the "CharacterController"-Component and enables it after the parsed "delay"
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    public IEnumerator SetController(float delay)
    {
        controller.enabled = false;

        yield return new WaitForSeconds(delay);

        controller.enabled = true;
    }


    /// <summary>
    /// Sets the config file for the Player at start of game
    /// </summary>
    /// <param name="file"></param>
    public void SetPlayerConfig(AstronautConfig astronautFile = null , AlienConfig alienFile = null)
    {
        astronautConfiguration = astronautFile;
        alienConfiguration = alienFile;
    }

    /// <summary>
    /// Saves the PlayerConfig values during runtime
    /// </summary>
    private void UpdatePlayerConfig()
    {
        if (astronautConfiguration != null)
        {
            movementSpeed = astronautConfiguration.MovementSpeed;
            smoothRotationTime = astronautConfiguration.SmoothRotationTime;
        }
        if (alienConfiguration != null)
        {
            movementSpeed = alienConfiguration.MovementSpeed;
            smoothRotationTime = alienConfiguration.SmoothRotationTime;
        }
    }

    ///// <summary>
    ///// Visualizes the GroundCheck sphere
    ///// </summary>
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    //}
}