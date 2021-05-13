using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    private int index;
    private bool inputActive = false;
    private bool characterSelected = false;

    [SerializeField] private GameObject playerPrefab;
    private List<GameObject> meshList;
    private int currentIndex;

    private PlayerController player;
    private PlayerInput playerInput;

    [SerializeField] private AstronautConfig astronautConfig;
    [SerializeField] private AlienConfig alienConfig;

    private Astronaut astronaut;
    private Alien alien;

    private void Awake()
    {
        Instantiate(playerPrefab);
        PairPlayerWithInputDevice();

        if (CharacterMenu.characterMenu != null)
        {
            meshList = CharacterMenu.characterMenu.CharacterSelection(index);
        }

        EventController.PlayerJoined();
    }

    private void Start()
    {
        inputActive = true;
    }

    private void OnEnable()
    {
        EventController.OnCharacterMenuActive += CharacterMenuActive;
    }

    private void OnDisable()
    {
        EventController.OnCharacterMenuActive -= CharacterMenuActive;
    }

    /// <summary>
    /// Is called when the CharacterMenu activates
    /// </summary>
    private void CharacterMenuActive()
    {
        meshList = CharacterMenu.characterMenu.CharacterSelection(index);
    }

    /// <summary>
    /// Pairs the Player with the respective device
    /// </summary>
    private void PairPlayerWithInputDevice()
    {
        GameController.gameController.AllPlayerJoined.Add(this.gameObject);

        playerInput = GetComponent<PlayerInput>();
        PlayerController[] playerController = FindObjectsOfType<PlayerController>();

        // Checks if the "PlayerInput" and the Player have the same index/id
        index = playerInput.playerIndex;
        player = playerController.FirstOrDefault(player => player.GetPlayerId() == index);

        // Makes this GameObject a child of the respective Player and sets its position
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.position = player.transform.position;

        PlayerNumber playerNumber = player.GetComponentInChildren<PlayerNumber>();
        playerNumber.SetNumberAndColor(index);
    }

    /// <summary>
    /// Sets each Players Race and Character mesh
    /// </summary>
    public void PlacePlayerInScene()
    {
        SoundController.PlaySound(SoundController.Sound.PlayerSpawn, transform.position);

        // Sets the Position on the map
        meshList[currentIndex].transform.SetParent(player.gameObject.transform);
        meshList[currentIndex].transform.localScale = new Vector3(1, 1, 1);
        meshList[currentIndex].transform.localRotation = player.transform.rotation;
        meshList[currentIndex].transform.localPosition = new Vector3(0, -1.5f, 0);

        // Sets its Race and gives the Player their respective Scripts
        if ((CharacterMenu.Race)currentIndex == CharacterMenu.Race.Astronaut1 || (CharacterMenu.Race)currentIndex == CharacterMenu.Race.Astronaut2 || (CharacterMenu.Race)currentIndex == CharacterMenu.Race.Astronaut3)
        {
            player.Controller.enabled = false;
            player.transform.position = new Vector3(-425, 23.5f, 25);
            player.Controller.enabled = true;

            player.SetPlayerConfig(astronautConfig, null);
            astronaut = player.gameObject.AddComponent<Astronaut>();
            player.transform.gameObject.layer = LayerMask.NameToLayer("Astronaut");

            //Animator animator = player.gameObject.GetComponent<Animator>();
            //animator.runtimeAnimatorController = Resources.Load("AstronautController") as RuntimeAnimatorController;

            Animator animator = meshList[currentIndex].gameObject.GetComponent<Animator>();

            player.animator = animator;
            astronaut.animator = animator;
        }
        else if ((CharacterMenu.Race)currentIndex == CharacterMenu.Race.Alien1 || (CharacterMenu.Race)currentIndex == CharacterMenu.Race.Alien2)
        {
            player.Controller.enabled = false;
            player.transform.position = new Vector3(425, 23.5f, 25);
            player.transform.localScale = new Vector3(9.375f, 9.375f, 9.375f);
            player.Controller.enabled = true;

            player.SetPlayerConfig(null, alienConfig);
            alien = player.gameObject.AddComponent<Alien>();
            player.transform.gameObject.layer = LayerMask.NameToLayer("Default");

            //Animator animator = player.gameObject.GetComponent<Animator>();
            //animator.runtimeAnimatorController = Resources.Load("AlienController") as RuntimeAnimatorController;

            Animator animator = meshList[currentIndex].gameObject.GetComponent<Animator>();

            player.animator = animator;
            alien.animator = animator;
        }
    }

    /// <summary>
    /// Is called when the Left "D-Pad" Button is pressed
    /// </summary>
    private void OnDPadLeftDown()
    {
        // Only allows this input, if the MainMenu in the Scene is active
        if (CharacterMenu.characterMenu != null && CharacterMenu.characterMenu.MenuInScene.activeSelf && !characterSelected)
        {
            int value = -1;

            CharacterMenu.characterMenu.ChangeRace(meshList, currentIndex, value);

            if (currentIndex + value < 0)
            {
                currentIndex = meshList.Count -1;
            }
            else
            {
                currentIndex += value;
            }

            EventController.CharacterSelectionButtonLeftPressed(index);
        }

    }

    /// <summary>
    /// Is called when the Left "D-Pad" Button is released
    /// </summary>
    private void OnDPadLeftUp()
    {
        // Only allows this input, if the MainMenu in the Scene is active
        if (CharacterMenu.characterMenu != null && CharacterMenu.characterMenu.MenuInScene.activeSelf && !characterSelected)
        {
            EventController.CharacterSelectionButtonLeftReleased(index);
        }
    }

    /// <summary>
    /// Is called when the Right "D-Pad" Button is pressed
    /// </summary>
    private void OnDPadRightDown()
    {
        // Only allows this input, if the MainMenu in the Scene is active
        if (CharacterMenu.characterMenu != null && CharacterMenu.characterMenu.MenuInScene.activeSelf && !characterSelected)
        {
            int value = 1;

            CharacterMenu.characterMenu.ChangeRace(meshList, currentIndex, value);

            if (currentIndex + value > meshList.Count -1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex += value;
            }

            EventController.CharacterSelectionButtonRightPressed(index);
        }

    }

    /// <summary>
    /// Is called when the Right "D-Pad" Button is released
    /// </summary>
    private void OnDPadRightUp()
    {
        // Only allows this input, if the MainMenu in the Scene is active
        if (CharacterMenu.characterMenu != null && CharacterMenu.characterMenu.MenuInScene.activeSelf && !characterSelected)
        {
            EventController.CharacterSelectionButtonRightReleased(index);
        }
    }

    private void OnDPadUp()
    {
        if (GameController.gameController.EndMenuActive || GameController.gameController.StartMenuActive)
        {
            EventController.DPadUp(index, -1);
            //EndMenu.endMenu.MoveIndex(-1);
        }
    }

    private void OnDPadDown()
    {
        if (GameController.gameController.EndMenuActive || GameController.gameController.StartMenuActive)
        {
            EventController.DPadDown(index, +1);
            //EndMenu.endMenu.MoveIndex(+1);
        }
    }

    /// <summary>
    /// Is called when the specified input for movement is used
    /// </summary>
    /// <param name="value"></param>
    private void OnMovement(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>();

        // Only allows this input, if the MainMenu in the Scene is inactive
        if (CharacterMenu.characterMenu != null && !CharacterMenu.characterMenu.MenuInScene.activeSelf && !GameController.gameController.EndMenuActive)
        {
            player.SetInputVector(direction);
        }
    }

    /// <summary>
    /// Is called when the specified Button is pressed
    /// </summary>
    private void OnPrimary()
    {
        if (Intro.intro.ThisObj.activeSelf)
        {
            Intro.intro.CloseIntro();
        }

        // During CharacterSelection
        // Select Character
        if (CharacterMenu.characterMenu != null && CharacterMenu.characterMenu.MenuInScene.activeSelf && inputActive && !characterSelected)
        {
            bool characterAlreadySelected = false;

            foreach (int character in CharacterMenu.characterMenu.SelectedCharacter)
            {
                if (character == currentIndex)
                {
                    characterAlreadySelected = true;
                }
            }

            if (!characterAlreadySelected)
            {
                CharacterMenu.characterMenu.SelectedCharacter.Add(currentIndex);
                EventController.SelectCharacter(index, currentIndex);
                characterSelected = true;
                goto Skip;
            }
        }
        // Deselect Character
        if (CharacterMenu.characterMenu != null && CharacterMenu.characterMenu.MenuInScene.activeSelf && inputActive && characterSelected)
        {
            CharacterMenu.characterMenu.SelectedCharacter.Remove(currentIndex);
            EventController.DeSelectCharacter(index, currentIndex);
            characterSelected = false;
        }
    Skip:;

        // Only allows this input, if the MainMenu in the Scene is inactive
        if (CharacterMenu.characterMenu != null && !CharacterMenu.characterMenu.MenuInScene.activeSelf)
        {
            // Checks if the "Astronaut" script has already been added to the player
            if (astronaut != null && !GameController.gameController.EndMenuActive)
            {
                astronaut.Attack();
            }
            // Checks if the "Alien" script has already been added to the player
            else if (alien != null && !GameController.gameController.EndMenuActive)
            {
                alien.Shoot();
            }
        }

        if (GameController.gameController.EndMenuActive)
        {
            EndMenu.endMenu.PressButton();
        }
    }

    /// <summary>
    /// Is called when the RightTrigger is pressed
    /// </summary>
    private void OnSecondary()
    {
        // Only allows this input, if the MainMenu in the Scene is inactive
        if (CharacterMenu.characterMenu != null && !CharacterMenu.characterMenu.MenuInScene.activeSelf)
        {
            // Checks if the "Astronaut" script has already been added to the player
            if (astronaut != null && !GameController.gameController.EndMenuActive)
            {
                astronaut.PlaceBarricade();
            }
            // Checks if the "Alien" script has already been added to the player
            else if (alien != null && !GameController.gameController.EndMenuActive)
            {
                alien.Teleport();
            }
        }
    }

    /// <summary>
    /// Is called when the LeftTrigger is pressed
    /// </summary>
    private void OnUltimate()
    {
        // Only allows this input, if the MainMenu in the Scene is inactive
        if (CharacterMenu.characterMenu != null && !CharacterMenu.characterMenu.MenuInScene.activeSelf)
        {
            // Checks if the "Astronaut" script has already been added to the player
            if (astronaut != null && !GameController.gameController.EndMenuActive)
            {
                astronaut.Ultimate();
            }
            // Checks if the "Alien" script has already been added to the player
            else if (alien != null && !GameController.gameController.EndMenuActive)
            {
                alien.Ultimate();
            }
        }
    }
}