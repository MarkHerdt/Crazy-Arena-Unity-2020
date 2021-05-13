using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barricade : MonoBehaviour
{
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Text counter;
    [SerializeField] private Image barricadeImage;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image imageObject;
    [Tooltip("Activates the Barricade at start of Game, without the timer running down the first time it's active")]
    [SerializeField] private bool activeAtStart;

    public Image BarricadeImage { get { return barricadeImage; } }
    public Image ButtonImage { get { return buttonImage; } }
    public Image ImageObject { get { return imageObject; } set { imageObject = value; } }

    private float duration;

    public bool barricadeIsActive { get; private set; }
    private IEnumerator barricadeUp;
    private IEnumerator countdown;
    private IEnumerator barricadeDown;

    private Vector3 initialPosition;
    private Collider barricadeCollider;
    private float barricadeHeight;

    private BarricadeTrigger[] trigger;
    [SerializeField] private List<Light> lights = new List<Light>(); 

    private void Awake()
    {
        initialPosition = gameObject.transform.localPosition;
        barricadeCollider = GetComponentInChildren<Collider>();
        barricadeHeight = barricadeCollider.bounds.size.y;

        trigger = GetComponentsInParent<BarricadeTrigger>();
        foreach (BarricadeTrigger _trigger in trigger)
        {
            lights.Add(_trigger.GetComponentInChildren<Light>());
        }

        icon.gameObject.SetActive(false);
    }

    private void Start()
    {
        ActiveAtStart();

        // Subscriptions
        EventController.OnGameStart += GameStart;
    }


    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        // Unsubscribe
        EventController.OnGameStart -= GameStart;
    }

    /// <summary>
    /// Activates the Barricade 
    /// </summary>
    public void ActivateBarricade(float _duration)
    {
        duration = _duration;

        if (barricadeDown != null)
        {
            StopCoroutine(barricadeDown);
        }
        
        barricadeIsActive = true;

        De_ActivateLights(false);

        barricadeUp = BarricadeUp();
        StartCoroutine(barricadeUp);
    }

    /// <summary>
    /// Moves the Barricade up
    /// </summary>
    /// <returns></returns>
    private IEnumerator BarricadeUp()
    {
        int steps = 10;
        float time = .01f;
        float calc = time / steps;

        SoundController.PlaySound(SoundController.Sound.Barricade, transform.position, 250);

        for (int i = 0; i < steps; i++)
        {
            gameObject.transform.localPosition += new Vector3(0, barricadeHeight / steps, 0);

            // Maximum allowed height
            if (gameObject.transform.localPosition.y >= initialPosition.y + barricadeHeight)
            {
                break;
            }

            yield return new WaitForSeconds(calc);
        }

        countdown = Countdown();
        StartCoroutine(countdown);
        yield return new WaitForSeconds(duration);

        barricadeUp = null;

        DeactivateBarricade();
    }

    /// <summary>
    /// Deactivates the Barricade
    /// </summary>
    public void DeactivateBarricade()
    {
        if (barricadeUp != null)
        {
            StopCoroutine(countdown);
            StopCoroutine(barricadeUp);
        }

        SetSprites(false);
        De_ActivateLights(true);

        barricadeIsActive = false;

        barricadeDown = BarricadeDown();
        StartCoroutine(barricadeDown);
    }

    /// <summary>
    /// Moves the Barricade down
    /// </summary>
    /// <returns></returns>
    private IEnumerator BarricadeDown()
    {
        int steps = 10;
        float time = .01f;
        float calc = time / steps;

        SoundController.PlaySound(SoundController.Sound.Barricade, transform.position, 250);

        for (int i = 0; i < steps; i++)
        {
            gameObject.transform.localPosition += new Vector3(0, -barricadeHeight / steps, 0);

            // Minimum allowed height
            if (gameObject.transform.localPosition.y <= initialPosition.y)
            {
                break;
            }

            yield return new WaitForSeconds(calc);
        }

        barricadeDown = null;
    }

    /// <summary>
    /// Counts down the timer above the Barricade
    /// </summary>
    /// <returns></returns>
    private IEnumerator Countdown()
    {
        float tmpDuration = duration;

        counter.text = duration.ToString();

        SetSprites(true);

        do
        {
            tmpDuration--;
            counter.text = ((int)tmpDuration).ToString();
            yield return new WaitForSeconds(duration / duration);

        } while (tmpDuration > 0);
    }

    /// <summary>
    /// Sets the sprites in the Barricade on and off
    /// </summary>
    /// <param name="active"></param>
    private void SetSprites(bool active)
    {
        if (active)
        {
            counter.gameObject.SetActive(true);
            icon.enabled = true;
            imageObject.sprite = null;
            imageObject.color = new Color32(255, 255, 255, 0);
        }
        else if (!active)
        {
            imageObject.color = new Color32(255, 255, 255, 255);
            imageObject.sprite = barricadeImage.sprite;
            icon.enabled = false;
            counter.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// De/Activates the Lights, depending on the parsed "bool"
    /// </summary>
    /// <param name="activate"></param>
    private void De_ActivateLights(bool activate)
    {
        foreach (Light light in lights)
        {
            if (activate)
            {
                light.enabled = true;
            }
            else if (!activate)
            {
                light.enabled = false;
            }
        }
    }

    /// <summary>
    /// Activates the Icon above the Barricade at start of Game
    /// </summary>
    private void GameStart()
    {
        icon.gameObject.SetActive(true);
    }

    /// <summary>
    /// Shows the standart icon above the Barricade
    /// </summary>
    public void ShowBarricadeIcon()
    {
        ImageObject.sprite = BarricadeImage.sprite;
    }

    /// <summary>
    /// Shows an icon above the Barricade, which button to press, when a player is in the Barricades trigger
    /// </summary>
    public void ShowButtonButtonIcon()
    {
        // When the Barricade is currently not active
        if (!barricadeIsActive)
        {
            ImageObject.sprite = ButtonImage.sprite;
        }
        else if (barricadeIsActive && imageObject.sprite == barricadeImage.sprite)
        {
            ImageObject.sprite = ButtonImage.sprite;
        }
    }

    /// <summary>
    /// Activates the Barricade at start of Game, without the timer running down the first time it's active
    /// </summary>
    private void ActiveAtStart()
    {
        if (activeAtStart)
        {
            barricadeIsActive = true;
            transform.localPosition += new Vector3(0, barricadeHeight, 0);
        }
    }
}