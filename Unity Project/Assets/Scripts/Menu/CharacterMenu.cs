using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public static CharacterMenu characterMenu;
    public GameObject MenuInScene { get; private set; }

    [SerializeField] private RotateAroundMap camRotation;
    public Vector3 CameraPosition { get; set; }
    public Quaternion CameraRotation { get; set; }

    [Tooltip("Camera in the Scene")]
    [SerializeField] private Camera cam;
    [Tooltip("Canvas Component on this Object")]
    [SerializeField] private Canvas canvas;

    [Tooltip("\"Selection\" Object in this Object")]
    [SerializeField] private GameObject selectionMenu;
    [SerializeField] private GameObject characterSelectionPrefab;
    [SerializeField] private GameObject ui;
    [SerializeField] private Text countdown;

    [SerializeField] private GameObject spawnParticle;


    [Header("Order of the Meshes and the \"Race-enum\" must be the same")]
    [SerializeField] private GameObject[] playerMeshes;
    public enum Race
    {
        Astronaut1,
        Astronaut2,
        Astronaut3,
        Alien1,
        Alien2
    }

    private List<int> selectedCharacter = new List<int>();
    public List<int> SelectedCharacter { get { return selectedCharacter; } set { selectedCharacter = value; } }

    private int astronauts = 0;
    private int aliens = 0;

    IEnumerator startGame;

    private void Awake()
    {
        //Cursor.visible = false;
        characterMenu = this;
        MenuInScene = this.gameObject;

        if (camRotation == null)
        {
            camRotation = FindObjectOfType<RotateAroundMap>();
        }

        if (cam == null)
        {
            cam = GameObject.FindObjectOfType<Camera>();
        }
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        if (canvas.worldCamera == null)
        {
            canvas.worldCamera = cam;
        }

        EventController.CharacterMenuActivate();
    }

    private void Start()
    {
        if (ui.activeSelf)
        {
            ui.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventController.OnPlayerJoined += PlayerJoined;
        EventController.OnSelectCharacter += AddCharacter;
        EventController.OnDeselectCharacter += RemoveCharacter;
    }

    private void OnDisable()
    {
        EventController.OnPlayerJoined -= PlayerJoined;
        EventController.OnSelectCharacter -= AddCharacter;
        EventController.OnDeselectCharacter -= RemoveCharacter;
    }

    /// <summary>
    /// Character selection screen
    /// </summary>
    /// <returns></returns>
    public List<GameObject> CharacterSelection(int index)
    {
        List<GameObject> meshList = new List<GameObject>();

        GameObject characterSelection;

        characterSelection = Instantiate(characterSelectionPrefab, selectionMenu.transform);
        characterSelection.GetComponent<CharacterSelection>().Index = index;

        // Instantiates all available Playermeshes the Player can choose from
        foreach (GameObject playerMesh in playerMeshes)
        {
            GameObject mesh = Instantiate(playerMesh, characterSelection.transform);
            mesh.transform.localScale = new Vector3(25, 25, 25);
            mesh.transform.localPosition = new Vector3(mesh.transform.localPosition.x, mesh.transform.localPosition.y - 50, mesh.transform.localPosition.z - 10);
            meshList.Add(mesh);
            mesh.SetActive(false);
        }

        //Random.Range(0, playerMeshes.Length)
        meshList[0].SetActive(true);
        EventController.CharacterChange();

        return meshList;
    }

    /// <summary>
    /// Changes the Race of the Player
    /// </summary>
    /// <param name="meshList"></param>
    /// <param name="currentIndex"></param>
    /// <param name="value"></param>
    public void ChangeRace(List<GameObject> meshList, int currentIndex, int value)
    {
        int index;

        // Deactivates the currently selected Mesh
        meshList[currentIndex].SetActive(false);

        // Activates the next Mesh
        if (currentIndex + value < 0)
        {
            index = meshList.Count - 1;
            meshList[index].SetActive(true);
        }
        else if (currentIndex + value > meshList.Count - 1)
        {
            index = 0;
            meshList[index].SetActive(true);
        }
        else
        {
            index = currentIndex + value;
            meshList[index].SetActive(true);
        }

        EventController.CharacterChange();
    }

    /// <summary>
    /// Checks if all Player that have joined picked a Character
    /// </summary>
    private void AddCharacter(int index, int character)
    {
        SoundController.PlaySound(SoundController.Sound.Select);

        if ((Race)character == Race.Astronaut1 || (Race)character == Race.Astronaut2 || (Race)character == Race.Astronaut3)
        {
            astronauts++;
        }
        else if ((Race)character == Race.Alien1 || (Race)character == Race.Alien2)
        {
            aliens++;
        }

        if (((astronauts > 0 && aliens > 0) || GameController.gameController.GameConfig.StartGameAlone) && astronauts + aliens == GameController.gameController.AllPlayerJoined.Count)
        {
            startGame = StartCountdown();
            StartCoroutine(startGame);
        }
    }

    private void RemoveCharacter(int index, int character)
    {
        SoundController.PlaySound(SoundController.Sound.Deselect);

        if ((Race)character == Race.Astronaut1 || (Race)character == Race.Astronaut2 || (Race)character == Race.Astronaut3)
        {
            astronauts--;
        }
        else if ((Race)character == Race.Alien1 || (Race)character == Race.Alien2)
        {
            aliens--;
        }

        if (startGame != null)
        {
            StopCoroutine(startGame);
            countdown.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Countdown, when every Player selected a Character, before the Game starts
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartCountdown()
    {
        countdown.text = GameController.gameController.GameConfig.StartCountdown.ToString();
        countdown.gameObject.SetActive(true);

        for (int i = 1; i <= GameController.gameController.GameConfig.StartCountdown; i++)
        {
            SoundController.PlaySound(SoundController.Sound.Beep);
            yield return new WaitForSeconds(1);
            countdown.text = (GameController.gameController.GameConfig.StartCountdown - i).ToString();
        }

        SetCamera();
    }

    /// <summary>
    /// Starts the Game
    /// </summary>
    private void SetCamera()
    {
        GameController.gameController.PlayerInputManager.enabled = false;

        camRotation.StartPosition = camRotation.transform.position;
        camRotation.StartRotation = camRotation.transform.rotation;
        camRotation.AnimationPlaying = true;

        EventController.GameStart();
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        ui.SetActive(true);

        // Searches for every spawned Player on the map
        InputHandler[] playerList = GameObject.FindObjectsOfType<InputHandler>();

        foreach (InputHandler player in playerList)
        {
            player.PlacePlayerInScene();
            Instantiate(spawnParticle, player.transform.position, Quaternion.identity);
        }

        foreach (GameObject player in GameController.gameController.AllPlayerJoined)
        {
            PlayerNumber playerNumber = player.GetComponentInParent<PlayerController>().GetComponentInChildren<PlayerNumber>();

            playerNumber.Background.gameObject.SetActive(true);
            playerNumber.Number.gameObject.SetActive(true);
        }

        startGame = null;

        GameController.gameController.source.clip = GameController.gameController.gameAudio;
        GameController.gameController.source.Play();
    }

    private void PlayerJoined()
    {
        if (startGame != null)
        {
            StopCoroutine(startGame);
            countdown.gameObject.SetActive(false);
        }
    }
}
