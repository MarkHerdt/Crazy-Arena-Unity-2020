using UnityEngine;
using UnityEngine.UI;

public class PlayerNumber : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image background;
    [SerializeField] private Text number;
    [SerializeField] private bool rotateTowards;
    
    public Image Background { get => background; }
    public Text Number { get => number; }

    private Camera cam;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();

        if (canvas == null)
        {
            canvas = GetComponent<Canvas>(); 
        }
        canvas.worldCamera = cam;

        if (background == null)
        {
            background = GetComponentInChildren<Image>();
        }

        if (number == null)
        {
            number = GetComponentInChildren<Text>();
        }
    }

    void Update()
    {
        if (rotateTowards)
        {
            transform.LookAt(cam.transform, Vector3.forward);
        }
    }

    
    public void SetNumberAndColor(int index)
    {
        int randomColor = Random.Range(0, GameController.gameController.ColorList.Count - 1);

        Color32 tmpColor = GameController.gameController.ColorList[randomColor];

        tmpColor.a = 64;
        background.color = tmpColor;

        tmpColor.a = 191;
        number.color = tmpColor;
        number.text = $"P{index + 1}";

        GameController.gameController.ColorList.RemoveAt(randomColor);
    }
}
