using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI ui;

    public bool AstronautWon { get; set; }
    public bool AlienWon { get; set; }
    public Victory victory;

    [SerializeField] private GameController gameController;
    [SerializeField] private Text time;
    [SerializeField] private Text astronautScore;
    [SerializeField] private Image astronautButton;
    [SerializeField] private Text alienScore;
    [SerializeField] private Image alienButton;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject endMenu;
   
    public EndMenu EndMenuScript { get; private set; }

    [SerializeField] private Slider astronautSlider;
    [SerializeField] private Slider alienSlider;
    [SerializeField] private GameObject ultimatePrefab;

    [SerializeField] private Slider slider;

    public bool AstronautUltimate { get; set; }
    public bool AlienUltimate { get; set; }

    private int tmpAstronautScore = 0;
    private int tmpAlienScore = 0;

    // Speed with which the Slider decreases
    private int steps;

    private IEnumerator setAstronautSlider;
    private IEnumerator setAlienSlider;

    private bool subtractScore;

    private void Awake()
    {
        ui = this;

        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }

        ApplySettings();
    }

    private void Start()
    {
        // Subscriptions
        EventController.OnObjectUnBreak += SetAstronautScore;
        EventController.OnObjectBreak += SetAlienScore;
    }

    private void OnDestroy()
    {
        // Unsubscribe
        EventController.OnObjectUnBreak -= SetAstronautScore;
        EventController.OnObjectBreak -= SetAlienScore;
    }

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        if (gameController.Time > 0)
        {
            gameController.Time -= Time.deltaTime;

            float minutes = Mathf.FloorToInt(gameController.Time / 60);
            float seconds = Mathf.FloorToInt(gameController.Time % 60);
            float milliSeconds = (gameController.Time % 1) * 1000;

            time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            //time.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
        }
        else if (gameController.Time <= 1)
        {
            EventController.EndMatch();
        }

    }

    /// <summary>
    /// Applies all settings for this Object
    /// </summary>
    private void ApplySettings()
    {
        astronautSlider.minValue = 0;
        astronautSlider.maxValue = gameController.GameConfig.UltimateCost;
        astronautSlider.value = astronautSlider.maxValue;

        alienSlider.minValue = 0;
        alienSlider.maxValue = gameController.GameConfig.UltimateCost;
        alienSlider.value = alienSlider.maxValue;

        steps = gameController.GameConfig.SliderSpeed;
    }

    /// <summary>
    /// Sets the Score of the Astronauts
    /// </summary>
    /// <param name="amount"></param>
    private void SetAstronautScore(int amount)
    {
        //tmpAstronautScore += amount;
        gameController.AstronautScore += amount;

        //Animation score = astronautScore.GetComponent<Animation>();

        //if (score.isPlaying)
        //{
        //    score.Stop();
        //}

        //score.Play("Score");
        //astronautScore.text = gameController.AstronautScore.ToString();

        if (gameController.AstronautScore >= gameController.GameConfig.UltimateCost)
        {
            astronautButton.gameObject.SetActive(true);
            //Astronaut.UltimateReady = true;
            FindAllAstronauts();
        }

        //if (setAstronautSlider == null)
        //{
        //    setAstronautSlider = SetAstronautSlider();
        //    StartCoroutine(setAstronautSlider);
        //}
    }

    ///// <summary>
    ///// Adjusts the Astronauts Score-Bar
    ///// </summary>
    ///// <returns></returns>
    //private IEnumerator SetAstronautSlider()
    //{
    //    int score = 0;

    //    score = tmpAstronautScore;
    //    tmpAstronautScore -= score;

    //    for (int i = 0; i < score; i += steps)
    //    {
    //        // If the next step would be less then the sliders min-value
    //        if (astronautSlider.value - steps <= astronautSlider.minValue)
    //        {
    //            int tmp = (int)astronautSlider.value;
    //            FindAllAstronauts();
    //            astronautSlider.value = astronautSlider.maxValue - Mathf.Abs(tmp - steps);
    //        }
    //        else
    //        {
    //            astronautSlider.value -= steps;
    //        }

    //        yield return new WaitForSeconds(.01f);
    //    }

    //    // Start the Coroutine again, if a score was made while this Coroutine was running
    //    if (tmpAstronautScore > 0)
    //    {
    //        setAstronautSlider = SetAstronautSlider();
    //        StartCoroutine(setAstronautSlider);
    //    }

    //    setAstronautSlider = null;
    //}

    /// <summary>
    /// Sets the Score of the Aliens
    /// </summary>
    /// <param name="amount"></param>
    private void SetAlienScore(int amount)
    {
        //tmpAlienScore += amount;
        gameController.AlienScore += amount;

        //Animation score = alienScore.GetComponent<Animation>();

        //if (score.isPlaying)
        //{
        //    score.Stop();
        //}

        //score.Play("Score");
        //alienScore.text = gameController.AlienScore.ToString();

        if (gameController.AlienScore >= gameController.GameConfig.UltimateCost)
        {
            alienButton.gameObject.SetActive(true);
            //Alien.UltimateReady = true;
            FindAllAlien();
        }

        //if (setAstronautSlider == null)
        //{
        //    setAlienSlider = SetAlienSlider();
        //    StartCoroutine(setAlienSlider);
        //}
    }

    ///// <summary>
    ///// Adjusts the Aliens Score-Bar
    ///// </summary>
    ///// <returns></returns>
    //private IEnumerator SetAlienSlider()
    //{
    //    int score = 0;

    //    score = tmpAlienScore;
    //    tmpAlienScore -= score;

    //    for (int i = 0; i < score; i += steps)
    //    {
    //        // If the next step would be less then the sliders min-value
    //        if (alienSlider.value - steps <= alienSlider.minValue)
    //        {
    //            int tmp = (int)alienSlider.value;
    //            FindAllAlien();
    //            alienSlider.value = alienSlider.maxValue - Mathf.Abs(tmp - steps);
    //        }
    //        else
    //        {
    //            alienSlider.value -= steps;
    //        }

    //        yield return new WaitForSeconds(.01f);
    //    }

    //    // Start the Coroutine again, if a score was made while this Coroutine was running
    //    if (tmpAlienScore > 0)
    //    {
    //        setAlienSlider = SetAlienSlider();
    //        StartCoroutine(setAlienSlider);
    //    }

    //    setAlienSlider = null;
    //}

    private void FindAllAstronauts()
    {
        Astronaut[] astronaut = FindObjectsOfType<Astronaut>();

        foreach (Astronaut _astronaut in astronaut)
        {
            if (AstronautUltimate == false)
            {
                GameObject tmp = Instantiate(ultimatePrefab, _astronaut.transform);
                tmp.transform.position = _astronaut.transform.position;
                _astronaut.ultimate = tmp;

            }
        }
        AstronautUltimate = true;
    }

    private void FindAllAlien()
    {
        Alien[] alien = FindObjectsOfType<Alien>();


        foreach (Alien _alien in alien)
        {
            if (AlienUltimate == false)
            {
                GameObject tmp = Instantiate(ultimatePrefab, _alien.transform);
                tmp.transform.position = _alien.transform.position;
                _alien.ultimate = tmp;

            }
        }
        AlienUltimate = true;
    }

    //public IEnumerator SubtractScore(bool? astronaut = null, bool? alien = null)
    //{
    //    for (int i = 0; i < GameController.gameController.GameConfig.UltimateCost; i++)
    //    {
    //        if (astronaut == true)
    //        {
    //            if (GameController.gameController.AstronautScore - GameController.gameController.GameConfig.UltimateCost < GameController.gameController.GameConfig.UltimateCost)
    //            {
    //                astronautButton.gameObject.SetActive(false);
    //            }
    //            astronautScore.text = (--GameController.gameController.AstronautScore).ToString();
    //        }
    //        else if (alien == true)
    //        {
    //            if (GameController.gameController.AlienScore - GameController.gameController.GameConfig.UltimateCost < GameController.gameController.GameConfig.UltimateCost)
    //            {
    //                alienButton.gameObject.SetActive(false);
    //            }
    //            alienScore.text = (--GameController.gameController.AlienScore).ToString();
    //        }

    //        yield return new WaitForSeconds(.001f);
    //    }
    //}

    /// <summary>
    /// Is called at the end of the Animation "CameraOverlay" 
    /// </summary>
    private void PlayCutscene()
    {
        gameMenu.gameObject.SetActive(false);

        PositionPlayer();

        //ShowEndMenu();
    }

    public void PositionPlayer()
    {
        GameObject maincam = FindObjectOfType<Camera>().gameObject;
        maincam.gameObject.transform.parent = victory.cam.transform;
        maincam.transform.SetPositionAndRotation(victory.cam.transform.position, victory.cam.transform.rotation);

        PlayerController[] playerList = FindObjectsOfType<PlayerController>();
        List<GameObject> astronaut = new List<GameObject>();
        List<GameObject> alien = new List<GameObject>();

        foreach (PlayerController player in playerList)
        {
            CharacterController tmp = player.GetComponent<CharacterController>();
            tmp.height = 1;
            tmp.enabled = false;

            if (player.GetComponent<Astronaut>() != null)
            {
                astronaut.Add(player.gameObject);
            }
            else if (player.GetComponent<Alien>() != null)
            {
                alien.Add(player.gameObject);
            }
        }

        if (slider.value < gameController.DestroyableObjectCount / 2)
        {
            Debug.Log("Astronaut");

            foreach (GameObject player in astronaut)
            {
                player.GetComponentInChildren<Animator>().SetBool("win", true);
                player.GetComponent<CharacterController>().height = 3.75f;
            }

            foreach (GameObject player in alien)
            {
                player.GetComponentInChildren<Animator>().SetBool("loose", true);
                player.GetComponent<CharacterController>().height = 3.75f;
            }

            if (astronaut.Count >= 1)
            {
                astronaut[0].transform.parent = victory.win1.transform;
                astronaut[0].transform.SetPositionAndRotation(victory.win1.transform.position, victory.win1.transform.rotation);
            }
            if (astronaut.Count >= 2)
            {
                astronaut[1].transform.parent = victory.win2.transform;
                astronaut[1].transform.SetPositionAndRotation(victory.win2.transform.position, victory.win2.transform.rotation);
            }
            if (astronaut.Count >= 3)
            {
                astronaut[2].transform.parent = victory.win3.transform;
                astronaut[2].transform.SetPositionAndRotation(victory.win3.transform.position, victory.win3.transform.rotation);
            }

            if (alien.Count >= 1)
            {
                alien[0].transform.parent = victory.loose1.transform;
                alien[0].transform.SetPositionAndRotation(victory.loose1.transform.position, victory.loose1.transform.rotation);
            }
            if (alien.Count >= 2)
            {
                alien[1].transform.parent = victory.loose2.transform;
                alien[1].transform.SetPositionAndRotation(victory.loose2.transform.position, victory.loose2.transform.rotation);
            }
        }
        else if (slider.value > gameController.DestroyableObjectCount / 2)
        {
            Debug.Log("Alien");

            foreach (GameObject player in astronaut)
            {
                player.GetComponentInChildren<Animator>().SetBool("loose", true);
                player.GetComponent<CharacterController>().height = 3.75f;
            }

            foreach (GameObject player in alien)
            {
                player.GetComponentInChildren<Animator>().SetBool("win", true);
                player.GetComponent<CharacterController>().height = 3.75f;
            }

            if (alien.Count >= 1)
            {
                alien[0].transform.parent = victory.win1.transform;
                alien[0].transform.SetPositionAndRotation(victory.win1.transform.position, victory.win1.transform.rotation);
            }
            if (alien.Count >= 2)
            {
                alien[1].transform.parent = victory.win3.transform;
                alien[1].transform.SetPositionAndRotation(victory.win3.transform.position, victory.win3.transform.rotation);
            }

            if (astronaut.Count >= 1)
            {
                astronaut[0].transform.parent = victory.loose1.transform;
                astronaut[0].transform.SetPositionAndRotation(victory.loose1.transform.position, victory.loose1.transform.rotation);
            }
            if (astronaut.Count >= 2)
            {
                astronaut[1].transform.parent = victory.loose2.transform;
                astronaut[1].transform.SetPositionAndRotation(victory.loose2.transform.position, victory.loose2.transform.rotation);
            }
            if (astronaut.Count >= 3)
            {
                astronaut[2].transform.parent = victory.loose3.transform;
                astronaut[2].transform.SetPositionAndRotation(victory.loose3.transform.position, victory.loose3.transform.rotation);
            }
        }
        else if (slider.value == gameController.DestroyableObjectCount / 2)
        {
            foreach (GameObject player in astronaut)
            {
                player.GetComponentInChildren<Animator>().SetBool("loose", true);
                player.GetComponent<CharacterController>().height = 3.75f;
            }

            foreach (GameObject player in alien)
            {
                player.GetComponentInChildren<Animator>().SetBool("loose", true);
                player.GetComponent<CharacterController>().height = 3.75f;
            }

            if (playerList.Length >= 1)
            {
                playerList[0].transform.parent = victory.win0.transform;
                playerList[0].transform.SetPositionAndRotation(victory.win0.transform.position, victory.win0.transform.rotation);
            }
            if (playerList.Length >= 2)
            {
                playerList[1].transform.parent = victory.win1.transform;
                playerList[1].transform.SetPositionAndRotation(victory.win1.transform.position, victory.win1.transform.rotation);
            }
            if (playerList.Length >= 3)
            {
                playerList[2].transform.parent = victory.win2.transform;
                playerList[2].transform.SetPositionAndRotation(victory.win2.transform.position, victory.win2.transform.rotation);
            }
            if (playerList.Length >= 4)
            {
                playerList[3].transform.parent = victory.win3.transform;
                playerList[3].transform.SetPositionAndRotation(victory.win3.transform.position, victory.win3.transform.rotation);
            }
            if (playerList.Length >= 5)
            {
                playerList[4].transform.parent = victory.win4.transform;
                playerList[4].transform.SetPositionAndRotation(victory.win4.transform.position, victory.win4.transform.rotation);
            }
        }

        foreach (var player in playerList)
        {
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

    public void ShowEndMenu()
    {
        endMenu.GetComponent<EndMenu>().setActive();
        endMenu.gameObject.SetActive(true);
    }

    public void AstronautButton()
    {
        astronautButton.gameObject.SetActive(false);
    }

    public void AlienButton()
    {
        alienButton.gameObject.SetActive(false);
    }
}