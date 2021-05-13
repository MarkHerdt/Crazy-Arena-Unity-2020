using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public int Index { get; set; }

    private PlayerController[] allPlayerInScene;
    private PlayerNumber playerNumber;
    private Image image;
    private Text text;

    [SerializeField] private Image background;
    public Color32 InitialBackgroundColor { get; private set; }
    [SerializeField] private Text race;
    [SerializeField] private float rotationSpeed = .5f;
    [SerializeField] private GameObject buttonLeft;
    [SerializeField] private GameObject buttonRight;

    private Transform[] meshes;
    private GameObject activeMesh;

    private void Awake()
    {
        allPlayerInScene = FindObjectsOfType<PlayerController>();

        playerNumber = GetComponentInChildren<PlayerNumber>();
        background = GetComponent<Image>();
        InitialBackgroundColor = background.color;
        image = playerNumber.GetComponentInChildren<Image>();
        text = playerNumber.GetComponentInChildren<Text>();

        if (race == null)
        {
            race = transform.GetChild(1).GetComponent<Text>();
        }
    }

    private void Start()
    {
        foreach (PlayerController player in allPlayerInScene)
        {
            if (player.GetPlayerId() == Index)
            {
                PlayerNumber _playerNumber = player.GetComponentInChildren<PlayerNumber>();
                Image _image = _playerNumber.GetComponentInChildren<Image>();
                Text _text = _playerNumber.GetComponentInChildren<Text>();

                image.color = _image.color;
                text.text = _text.text;
                text.color = _text.color;

                Transform[] children = _playerNumber.GetComponentsInChildren<Transform>();

                for (int i = 1; i < children.Length; i++)
                {
                    children[i].gameObject.SetActive(false);
                }

                break;
            }
        }
    }

    private void OnEnable()
    {
        // Subscriptions
        EventController.OnCharacterChange += ChangeRaceText;
        EventController.OnCharacterSelectionButtonLeftPressed += ButtonLeftPressed;
        EventController.OnCharacterSelectionButtonLeftReleased += ButtonLeftReleased;
        EventController.OnCharacterSelectionButtonRightPressed += ButtonRightPressed;
        EventController.OnCharacterSelectionButtonRightReleased += ButtonRightReleased;
        EventController.OnSelectCharacter += SelectCharacter;
        EventController.OnDeselectCharacter += DeselectCharacter;
    }

    private void OnDisable()
    {
        // Unsubscribe
        EventController.OnCharacterChange -= ChangeRaceText;
        EventController.OnCharacterSelectionButtonLeftPressed -= ButtonLeftPressed;
        EventController.OnCharacterSelectionButtonLeftReleased -= ButtonLeftReleased;
        EventController.OnCharacterSelectionButtonRightPressed -= ButtonRightPressed;
        EventController.OnCharacterSelectionButtonRightReleased -= ButtonRightReleased;
        EventController.OnSelectCharacter -= SelectCharacter;
        EventController.OnDeselectCharacter -= DeselectCharacter;
    }

    private void Update()
    {
        activeMesh.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }

    /// <summary>
    /// Changes the text of the Race during character selection
    /// </summary>
    /// <param name="mesh"></param>
    private void ChangeRaceText()
    {
        meshes = GetComponentsInChildren<Transform>();

        // Searches for an active mesh in its hierarchy
        foreach (Transform transform in meshes)
        {
            if (transform.GetComponent<RectTransform>() == null && transform.gameObject.activeSelf)
            {
                activeMesh = transform.gameObject;
                race.text = transform.name.Substring(0, transform.name.IndexOf(' '));

                if (race.text == "Astronaut")
                {
                    race.color = new Color32(0, 128, 255, 255);
                }
                else if (race.text == "Alien")
                {
                    race.color = new Color32(128, 0, 0, 255);
                }

                break;
            }
        }
    }

    /// <summary>
    /// Is called when the Player presses the left D-Pad Button
    /// </summary>
    /// <param name="index"></param>
    private void ButtonLeftPressed(int index)
    {
        if (Index == index)
        {
            ButtonDown(buttonLeft);
            SoundController.PlaySound(SoundController.Sound.ButtonHover);
        }
    }

    /// <summary>
    /// Is called when the Player releases the left D-Pad Button
    /// </summary>
    /// <param name="index"></param>
    private void ButtonLeftReleased(int index)
    {
        if (Index == index)
        {
            ButtonUp(buttonLeft);
        }
    }

    /// <summary>
    /// Is called when the Player presses the right D-Pad Button
    /// </summary>
    /// <param name="index"></param>
    private void ButtonRightPressed(int index)
    {
        if (Index == index)
        {
            ButtonDown(buttonRight);
            SoundController.PlaySound(SoundController.Sound.ButtonHover);
        }
    }

    /// <summary>
    /// Is called when the Player releases the right D-Pad Button
    /// </summary>
    /// <param name="index"></param>
    private void ButtonRightReleased(int index)
    {
        if (Index == index)
        {
            ButtonUp(buttonRight);
        }
    }

    /// <summary>
    /// Shrinks the Button while it is pressed
    /// </summary>
    /// <param name="button"></param>
    private void ButtonDown(GameObject button)
    {
        button.transform.localScale -= new Vector3(.5f, .5f, .5f);
    }

    /// <summary>
    /// Sets the Button back to its original size
    /// </summary>
    /// <param name="button"></param>
    private void ButtonUp(GameObject button)
    {
        button.transform.localScale += new Vector3(.5f, .5f, .5f);
    }

    public void SelectCharacter(int index, int character)
    {
        if (index == Index)
        {
            background.color = new Color32(0, 255, 0, 213);
        }
    }

    public void DeselectCharacter(int index, int character)
    {
        if (index == Index)
        {
            background.color = InitialBackgroundColor;
        }
    }
}
