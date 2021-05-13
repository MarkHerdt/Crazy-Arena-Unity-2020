using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private int overlaySpeed = 1;

    public AudioSource source;
    public AudioClip gameAudio;

    private static bool startMenuActive = true;
    public bool StartMenuActive { get => startMenuActive; set => startMenuActive = value; }

    private static bool endMenuActive = false;
    public bool EndMenuActive { get => endMenuActive; set => endMenuActive = value; }

    public GameConfig GameConfig { get { return gameConfig; } }
    public PlayerInputManager PlayerInputManager { get => playerInputManager; }

    private List<GameObject> allPlayerJoined = new List<GameObject>();
    public List<GameObject> AllPlayerJoined { get { return allPlayerJoined; } set { allPlayerJoined = value; } }

    private List<Color> colorList = new List<Color>();
    public List<Color> ColorList { get { return colorList; } }
    private Color red = new Color(255, 0, 0, 191);
    private Color green = new Color(0, 255, 0, 191);
    private Color blue = new Color(0, 0, 255, 191);
    private Color yellow = new Color(255, 255, 0, 191);
    private Color purple = new Color(255, 0, 255, 191);

    public int DestroyableObjectCount { get; set; }

    private float time;
    public float Time { get { return time; } set { time = value; } }

    [SerializeField] public int astronautscore { get => AstronautScore; }
    public int AstronautScore { get; set; }

    [SerializeField] public int alienscore { get => AlienScore; }
    public int AlienScore { get; set; }

    private bool endMatch = false;

    private void Awake()
    {
        gameController = this;
        AddColorToList();
        time = gameConfig.PlayTime;
        startMenuActive = true;
        endMenuActive = false;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += ResetId;
        EventController.OnEndMatch += EndMatch;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetId;
        EventController.OnEndMatch -= EndMatch;
    }

    /// <summary>
    /// Adds all available PlayerColors to the List
    /// </summary>
    private void AddColorToList()
    {
        ColorList.Add(red);
        ColorList.Add(green);
        ColorList.Add(blue);
        ColorList.Add(yellow);
        ColorList.Add(purple);
    }

    public void AddScore()
    {

    }

    public void SubtractScore(ref int score, int value)
    {

    }

    private void EndMatch()
    {
        if (!endMatch)
        {
            endMatch = true;

            foreach (GameObject player in allPlayerJoined)
            {
                player.GetComponentInChildren<PlayerInput>().enabled = false;
            }

            FindObjectOfType<UI>().GetComponent<Animation>().Play("CameraOverlay");
        }
    }

    private void ResetId(Scene scene, LoadSceneMode mode)
    {
        PlayerController.Id = 0;
    }
}
