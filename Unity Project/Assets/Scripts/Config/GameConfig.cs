using UnityEngine;

[CreateAssetMenu(menuName = "GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Menu")]
    [Tooltip("Duration of a Match in Seconds")]
    [SerializeField] private int playTime = 300;
    [Tooltip("Countdown, when every Player selected a Character, before the Game starts")]
    [SerializeField] private int startCountdown = 3;
    [Tooltip("Starts the game even when only one Player joined")]
    [SerializeField] private bool startGameAlone = false;

    [Header("Score")]
    [Tooltip("No win condition, if checked")]
    [SerializeField] private bool endlessGame = false;
    [Tooltip("Slider start at 0%")]
    private bool slider0Prc = false;
    [Tooltip("Slider start at 50%")]
    private bool slider50Prc = true;
    [Tooltip("How many Scorepoints a Team must have to win")]
    [SerializeField] private int scoreToWin = 1000;
    [Tooltip("How fast the ScoreBar is filled")]
    [SerializeField] private int sliderSpeed = 5;

    [Header("Ultimate")]
    [Tooltip("How many Points the Players have to collect to be able to use their Ultimate")]
    [SerializeField] private int ultimateCost = 1000;
    [Tooltip("Size if the Attack")]
    [SerializeField] private float ultimateRadius = 100;
    [Tooltip("Speed with which the Attack grows")]
    [SerializeField] private float speed = 250;

    private void OnValidate()
    {
        // Allows only one Checkbox to be active
        if (slider0Prc)
        {
            slider50Prc = false;
        }
        if (slider50Prc)
        {
            slider0Prc = false;
        }
    }

    public int PlayTime { get => playTime; }
    public int StartCountdown { get => startCountdown; }
    public bool StartGameAlone { get => startGameAlone; }
    public bool EndlessGame { get => endlessGame; }
    public bool Slider0Prc { get => slider0Prc; }
    public bool Slider50Prc { get => slider50Prc; }
    public int ScoreToWin { get => scoreToWin; }
    public int SliderSpeed { get => sliderSpeed; }
    public int UltimateCost { get => ultimateCost; }
    public float UltimateRadius { get => ultimateRadius; }
    public float Speed { get => speed; }
}