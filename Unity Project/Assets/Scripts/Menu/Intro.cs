using UnityEngine;

public class Intro : MonoBehaviour
{
    public static Intro intro;
    public GameObject ThisObj { get; private set; }

    //[SerializeField] private PlayerInputManager manager;
    [SerializeField] private GameObject characterMenu;

    private void Awake()
    {
        intro = this;
        ThisObj = this.gameObject;

        //manager.enabled = false;
        characterMenu.SetActive(false);
    }

    public void CloseIntro()
    {
        characterMenu.SetActive(true);
        ThisObj.SetActive(false);
    }
}
