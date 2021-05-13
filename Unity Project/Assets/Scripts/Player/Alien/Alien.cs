using System.Collections;
using UnityEngine;

public class Alien : MonoBehaviour
{
    private AlienConfig config;
    private PlayerController playerConrtoller;
    private CharacterController controller;

    public Animator animator { get; set; }

    private float alienHeight;

    private float lastShotFired = 0;
    private float lastTeleport = 0;

    private Collider teleport;

    //public static bool UltimateReady = false;
    public GameObject ultimate;

    private void Awake()
    {
        config = GetComponent<PlayerController>().AlienConfiguration;
        playerConrtoller = GetComponent<PlayerController>();
        controller = playerConrtoller.Controller;
    }

    private void Start()
    {
        alienHeight = transform.localScale.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        SaveTeleport(other);
    }

    private void OnTriggerStay(Collider other)
    {
        SaveTeleport(other);
    }

    private void OnTriggerExit(Collider other)
    {
        teleport = null;
    }

    /// <summary>
    /// Saves the Teleport the Player is standing in
    /// </summary>
    /// <param name="collider"></param>
    private void SaveTeleport(Collider collider = null)
    {
        if (collider.transform.gameObject.layer == LayerMask.NameToLayer("Teleport"))
        {
            teleport = collider;
        }
    }

    /// <summary>
    /// Shoot Bullet
    /// </summary>
    public void Shoot()
    {
        // Cooldown
        if (Time.time > (lastShotFired <= 0 ? 0 : lastShotFired + config.BulletCooldown))
        {
            SoundController.PlaySound(SoundController.Sound.AlienAttack, transform.position);

            //Animation
            animator.SetInteger("attack", 1);
            StartCoroutine(ResetAttack());

            // Stops moving while attacking
            if (config.StopWhileAttacking)
            {
                StartCoroutine(playerConrtoller.SetController(config.Delay));
            }

            // Timestamp when the last shot was fired
            lastShotFired = Time.time;

            GameObject bullet = Instantiate(config.AlienBulletPrefab, transform.position, transform.rotation);
            
            // Sets the Bullets Properties
            AlienBullet alienBullet = bullet.GetComponent<AlienBullet>();
            alienBullet.config = config;
            alienBullet.Alien = this.gameObject;
            alienBullet.AlienHeight = alienHeight;
            alienBullet.Gravity = config.BulletGravity;
            alienBullet.BulletSpeed = config.BulletSpeed;
            alienBullet.ExplosionRadius = config.ExplosionRadius;

            Rotate rotation = bullet.GetComponentInChildren<Rotate>();
            rotation.X = config.XRotation;
            rotation.Y = config.YRotation;
            rotation.Z = config.ZRotation;
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetInteger("attack", 0);
    }

    public void StopAttackAnim()
    {
        //Animation
        animator.SetInteger("attack", 0);
    }

    // Teleports the Player to a Teleport point in the direction the Player is looking
    public void Teleport()
    {
        // Cooldown
        if (Time.time > (lastTeleport <= 0 ? 0 : lastTeleport + config.TeleportCooldown))
        {
            // Timestamp for last Teleport used
            lastTeleport = Time.time;

            if (config.LookDirection)
            {
                Ray ray = new Ray(transform.position, transform.forward);

                RaycastHit[] hitInfo = Physics.RaycastAll(ray);
                //// Visualizes the RayCast
                //Debug.DrawRay(transform.position, transform.forward * 1000, Color.red, 5f);

                bool teleportHit = false;

                // TeleportPosition, when the Player hit a Teleporter
                float teleportDistance = float.PositiveInfinity;
                Vector3 teleportPosition = Vector3.zero;

                // TeleportPosition, when the Player hit the MapBounds
                float mapBoundsDistance = float.PositiveInfinity;
                Vector3 mapBoundsPosition = Vector3.zero;

                // Goes through all hits
                foreach (RaycastHit hit in hitInfo)
                {
                    // When a Teleporter was hit
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Teleport"))
                    {
                        teleportHit = true;

                        //// Visualizes where the Teleporter was hit
                        //Destroy(Instantiate(config.redSphere, hit.point, Quaternion.identity), 2.5f);

                        // Calculates the distance from the Player and the HitPoint
                        float tmpTeleportDistance = Vector3.Distance(this.gameObject.transform.position, hit.point);

                        // Saves the distance and the corresponding Teleporter position, if the distance is smaller than the currently saved one
                        if (tmpTeleportDistance < teleportDistance)
                        {
                            teleportDistance = tmpTeleportDistance;
                            teleportPosition = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z);
                        }

                        break;
                    }
                    // When the MapBodunrie was hit
                    if (hit.transform.CompareTag("Map Bounds"))
                    {
                        //// Visualizes where the MapBound was hit
                        //Destroy(Instantiate(config.greenSphere, hit.point, Quaternion.identity), 2.5f);

                        float radius = hit.collider.bounds.size.x > hit.collider.bounds.size.z ? hit.collider.bounds.size.x * 2 : hit.collider.bounds.size.z * 2;

                        Collider[] teleporter = Physics.OverlapSphere(hit.point, radius);
                        //Debug.Log($"Teleporter found: {teleporter.Length}");

                        //// Visualizes the size of "OverlapSphere"
                        //GameObject sphere = Instantiate(config.blueSphere, hit.point, Quaternion.identity);
                        //sphere.transform.localScale = new Vector3(radius, radius, radius);
                        //Destroy(sphere, .25f);

                        // Goes through every found Teleporter
                        foreach (Collider _teleport in teleporter)
                        {
                            if (_teleport.gameObject.layer == LayerMask.NameToLayer("Teleport"))
                            {
                                // Calculates the distance from the HitPoint and the Teleporter
                                float tmpMapBoundsDistance = Vector3.Distance(hit.point, _teleport.transform.gameObject.transform.position);

                                // Saves the distance and the corresponding Teleporter position, if the distance is smaller than the currently saved one
                                // Also looks if the Player is currently standing inside this Teleporter
                                if (tmpMapBoundsDistance < mapBoundsDistance && _teleport != this.teleport)
                                {
                                    mapBoundsDistance = tmpMapBoundsDistance;
                                    mapBoundsPosition = new Vector3(_teleport.transform.position.x, transform.position.y, _teleport.transform.position.z);
                                }
                            }

                        }
                    }
                }

                // When the Player hit a Teleporter
                if (teleportHit)
                {
                    TeleportPlayer(teleportPosition);
                }
                // When the Player hit the MapBounds
                else
                {
                    TeleportPlayer(mapBoundsPosition);
                }
            }
            // Teleports the Player to the other side of the Barricade, the Player is looking at
            else if (config.Blink)
            {
                Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);

                Barricade barricade;

                if ((barricade = hit.transform.gameObject.GetComponentInParent<Barricade>()) != null)
                {
                    Vector3 forward = barricade.transform.forward - gameObject.transform.forward;
                    Vector3 right = barricade.transform.right - gameObject.transform.right;

                    Vector3 teleportPosition = barricade.transform.position - forward - right;
                    //Vector3 teleportPosition = barricade.transform.position - (forward * (barricade.transform.localScale.x + transform.localScale.z)) - right;

                    TeleportPlayer(teleportPosition);
                }
            }
            // Teleports the Player to a fixed position on the Map
            else if (config.FixedPoint)
            {
                TeleportPlayer(config.TeleportPosition);
            }
        }
    }

    /// <summary>
    /// Teleports the Player
    /// </summary>
    private void TeleportPlayer(Vector3 teleportPosition)
    {
        Instantiate(config.TeleportEffect, transform.position, Quaternion.identity);

        // Character Controller needs to be turned off, before setting the new position
        controller.enabled = false;
        gameObject.transform.position = teleportPosition;
        controller.enabled = true;

        Instantiate(config.TeleportEffect, transform.position, Quaternion.identity);
    }

    //public void UltimateRdy(GameObject _ultimate)
    //{
    //    UI.ui.AlienUltimate = true;
    //    ultimate = _ultimate;
    //}

    /// <summary>
    /// Uses the Players Ultimate
    /// </summary>
    public void Ultimate()
    {
        if (GameController.gameController.AlienScore >= GameController.gameController.GameConfig.UltimateCost)
        {

            SoundController.PlaySound(SoundController.Sound.Ultimate, transform.position);

            Debug.Log("Ultimate");
            //UI ui = FindObjectOfType<UI>();
            //StartCoroutine(ui.SubtractScore(false, true));

            UI.ui.AlienButton();
            UI.ui.AlienUltimate = false;
            GameController.gameController.AlienScore = 0;

            GameObject ultimate = Instantiate(config.UltimateEffect, transform.position, Quaternion.identity);
            Ultimate ulti = ultimate.GetComponent<Ultimate>();
            ulti.Alien = true;

            Alien[] tmp = FindObjectsOfType<Alien>();
            for (int i = 0; i < tmp.Length; i++)
            {
                UltimateRdy _tmp = tmp[i].GetComponentInChildren<UltimateRdy>();
                if (_tmp != null)
                {
                    Destroy(_tmp.gameObject);
                }
            }
            //Destroy(this.ultimate.gameObject);
        }
    }
}