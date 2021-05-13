using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button start;
    [SerializeField] private Button controls;
    [SerializeField] private Button back;

    [SerializeField] private RotateAroundMap cam; 
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private CharacterMenu characterMenu;
    [SerializeField] private UI ui;

    public Vector3 CameraPosition { get; set; }
    public Quaternion CameraRotation { get; set; }

    private void Awake()
    {
        if (cam == null)
        {
            cam = FindObjectOfType<RotateAroundMap>();
        }

        if (characterMenu == null)
        {
            characterMenu = FindObjectOfType<CharacterMenu>();
        }
        characterMenu.gameObject.SetActive(false);

        if (ui == null)
        {
            ui = FindObjectOfType<UI>();
        }
        ui.gameObject.SetActive(false);
    }

    private void Start()
    {
        controlsMenu.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //EventController.eventController.OnDPadUp += MoveIndex;
        //EventController.eventController.OnDPadDown += MoveIndex;
    }

    private void OnDisable()
    {
        //EventController.eventController.OnDPadUp -= MoveIndex;
        //EventController.eventController.OnDPadDown -= MoveIndex;
    }

    public void StartGame()
    {
        characterMenu.gameObject.SetActive(true);
        cam.enabled = false;
        cam.transform.SetPositionAndRotation(CameraPosition, CameraRotation);
        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    public void OpenControls()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void CloseControls()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}