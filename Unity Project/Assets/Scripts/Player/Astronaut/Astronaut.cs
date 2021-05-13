using System.Collections;
using UnityEditor;
using UnityEngine;

public class Astronaut : MonoBehaviour
{
    private static Astronaut astronaut;
    private PlayerController playerConrtoller;
    public AstronautConfig Config { get; private set; }

    public Animator animator { get; set; }

    private float lastAttack = 0;
    private float lastBarricadeUsed = 0;

    public BarricadeTrigger trigger { get; set; }

    //public static bool UltimateReady = false;
    public GameObject ultimate;

    private void Awake()
    {
        astronaut = this;
        playerConrtoller = GetComponent<PlayerController>();
        Config = playerConrtoller.AstronautConfiguration;
    }

    private void Update()
    {
        if (Config.WalkThroughBarricades && transform.gameObject.layer != LayerMask.NameToLayer("Astronaut"))
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Astronaut");
        }
        else if (!Config.WalkThroughBarricades && transform.gameObject.layer == LayerMask.NameToLayer("Astronaut"))
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public void Attack()
    {
        // Cooldown
        if (Time.time > (lastAttack <= 0 ? 0 : lastAttack + Config.AttackCooldown))
        {
            SoundController.PlaySound(SoundController.Sound.AstronautAttack, transform.position);

            //Animation
            animator.SetBool("isAttacking", true);
            StartCoroutine(ResetAttack());


            // Stops moving while attacking
            if (Config.StopWhileAttacking)
            {
                StartCoroutine(playerConrtoller.SetController(Config.Delay));
            }

            // Timestamp for the last attack
            lastAttack = Time.time;

            GameObject attack = Instantiate(Config.AttackEffect, transform.position + (transform.forward * Config.AttackRange / 2), transform.rotation);
            attack.transform.localScale = new Vector3(Config.AttackRange, transform.localScale.y, Config.AttackRange / 4);

            Collider[] hitInfo = Physics.OverlapSphere(transform.position, Config.AttackRange);

            foreach (Collider hit in hitInfo)
            {
                Alien alien;
                DestroyableObject destroyableObject;

                // Direction Vector of "hit"-point
                Vector3 direction = hit.transform.position - transform.position;
                // The angle of the Object that was hit, relativ to the Astronaut
                float angle = Vector3.Angle(direction, transform.forward);

                // When a "DestroyableObject" is hit
                if ((destroyableObject = hit.transform.gameObject.GetComponentInParent<DestroyableObject>()) != null)
                {
                    // When the "DestroyableObject" is in the Players attack range
                    if (angle <= Config.AttackAngle)
                    {
                        // Checks if the Players view to the "DestroyableObject" is obstructed
                        Vector3 viewDirection = destroyableObject.transform.position - gameObject.transform.position;
                        Physics.Raycast(transform.position, viewDirection, out RaycastHit inView);

                        // If the first Object hit by the Raycast was a "DestroyableObject", it is in view
                        if (inView.transform.gameObject.GetComponent<DestroyableObject>() != null)
                        {
                            if (Config.AttackTest)
                            {
                                destroyableObject.AttackTest();
                            }
                            else
                            {
                                destroyableObject.Unbreak();
                            }
                        }

                    }
                }
                // When a "Alien" is hit
                if ((alien = hit.transform.gameObject.GetComponent<Alien>()) != null)
                {
                    // When the "Alien" is in the Players attack range
                    if (angle <= Config.AttackAngle)
                    {

                    }
                }
            }
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(.5f);
        animator.SetBool("isAttacking", false);
    }

    public void StopAttackAnim()
    {
        //Animation
        animator.SetBool("isAttacking", false);
    }

    /// <summary>
    /// Places a Barriacade
    /// </summary>
    public void PlaceBarricade()
    {
        // Cooldown for Barricade use || Lets the Player reset Barricades
        if (Time.time > (lastBarricadeUsed <= 0 ? 0 : lastBarricadeUsed + Config.BarricadeCooldown) || (trigger != null && trigger.Barricade.barricadeIsActive))
        {
            bool cooldown = false;

            // Checks if the Player is standing inside a trigger
            if (trigger != null)
            {
                cooldown = trigger.PlaceBarricade(Config.BarricadeDuration);

                // Set the new cooldown time when the Barricade is activated
                if (cooldown)
                {
                    lastBarricadeUsed = Time.time;
                }
            }
        }
    }

    //public void UltimateRdy(GameObject _ultimate)
    //{
    //    UI.ui.AstronautUltimate = true;
    //    ultimate = _ultimate;
    //}

    /// <summary>
    /// Uses the Players Ultimate
    /// </summary>
    public void Ultimate()
    {
        if (GameController.gameController.AstronautScore >= GameController.gameController.GameConfig.UltimateCost)
        {
            SoundController.PlaySound(SoundController.Sound.Ultimate, transform.position);

            Debug.Log("Ultimate");
            //UI ui = FindObjectOfType<UI>();
            //StartCoroutine(ui.SubtractScore(true, false));

            UI.ui.AstronautButton();
            UI.ui.AstronautUltimate = false;
            GameController.gameController.AstronautScore = 0;

            GameObject ultimate = Instantiate(Config.UltimateEffect, transform.position, Quaternion.identity);
            Ultimate ulti = ultimate.GetComponent<Ultimate>();
            ulti.Astronaut = true;

            Astronaut[] tmp = FindObjectsOfType<Astronaut>();
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

#if UNITY_EDITOR
    /// <summary>
    /// Visualizes the attack range cone
    /// </summary>
    [CustomEditor(typeof(Astronaut))]
    public class AttackRange : Editor
    {
        private Transform targetObject;
        private Astronaut astronaut;

        private void Awake()
        {
            astronaut = Astronaut.astronaut;
        }

        private void OnEnable()
        {
            targetObject = (target as Astronaut).transform;
        }

        private void OnSceneGUI()
        {
            Handles.color = new Color(0, 255, 0, .5f);
            // Right sector
            Handles.DrawSolidArc(targetObject.transform.position, -targetObject.transform.up, targetObject.transform.forward, astronaut.Config.AttackAngle, astronaut.Config.AttackRange);
            // Left Sector
            Handles.DrawSolidArc(targetObject.transform.position, targetObject.transform.up, targetObject.transform.forward, astronaut.Config.AttackAngle, astronaut.Config.AttackRange);
        }
    }
#endif
}
