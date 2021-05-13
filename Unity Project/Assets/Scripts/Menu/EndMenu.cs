using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public static EndMenu endMenu;

    private Button[] buttons;
    private Animation[] animations = new Animation[2];

    private int index = 0;
    private ColorBlock initialButtonColor;

    private void Awake()
    {
        endMenu = this;

        buttons = GetComponentsInChildren<Button>();
        initialButtonColor = buttons[0].colors;

        for (int i = 0; i < buttons.Length; i++)
        {
            Animation tmp = buttons[i].GetComponent<Animation>();

            animations[i] = tmp;
        }
    }

    private void OnEnable()
    {
        EventController.OnDPadUp += MoveIndex;
        EventController.OnDPadDown += MoveIndex;

        foreach (GameObject player in GameController.gameController.AllPlayerJoined)
        {
            player.GetComponentInChildren<PlayerInput>().enabled = true;
        }

        for (int i = 0; i < animations.Length; i++)
        {
            if (i == index)
            {
                animations[i].Play("SelectedButton");
            }
            else if (i != index)
            {
                if (animations[i].isPlaying)
                {
                    animations[i].Stop();
                }
            }
        }
    }

    private void OnDisable()
    {
        EventController.OnDPadUp -= MoveIndex;
        EventController.OnDPadDown -= MoveIndex;
    }

    public void MoveIndex(int _index, int direction)
    {
        SoundController.PlaySound(SoundController.Sound.ButtonHover);

        if ((GameController.gameController.AllPlayerJoined.Count % 2 == 0 && _index % 2 == 0) || GameController.gameController.AllPlayerJoined.Count % 2 != 0)
        {
            if (animations[index].isPlaying)
            {
                animations[index].Stop();
                buttons[index].colors = initialButtonColor;
            }

            index += direction;

            if (index < 0)
            {
                index = buttons.Length - 1;
            }
            else if (index > buttons.Length - 1)
            {
                index = 0;
            }

            animations[index].Play("SelectedButton");
        }
    }

    public void PressButton()
    {
        SoundController.PlaySound(SoundController.Sound.ButtonClick);

        buttons[index].onClick.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void setActive()
    {
        GameController.gameController.EndMenuActive = true;
    }
}
