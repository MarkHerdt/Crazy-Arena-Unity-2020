using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    private Slider[] slider;
    [SerializeField] private Slider astronautSlider;
    [SerializeField] private Slider alienSlider;
    [SerializeField] private Slider slider50Prc;
    [SerializeField] private GameObject background50Prc;

    private int astronautScore;
    private int alienScore;

    private IEnumerator setAstronautSlider;
    private IEnumerator setAlienSlider;
    private IEnumerator astronautSlider50Prc;
    private IEnumerator alienSlider50Prc;

    private int objectsDestroyed = 0;

    private void Awake()
    {
        slider = gameMenu.gameObject.GetComponentsInChildren<Slider>();
    }

    private void Start()
    {
        //foreach (Slider _slider in slider)
        //{
        //    _slider.minValue = 0;

        //    if (GameController.gameController.GameConfig.Slider0Prc)
        //    {
        //        _slider.maxValue = GameController.gameController.GameConfig.ScoreToWin;
        //        _slider.value = _slider.minValue;
        //    }
        //    else if (GameController.gameController.GameConfig.Slider50Prc)
        //    {
        //        _slider.maxValue = GameController.gameController.DestroyableObjectCount;
        //        _slider.value = _slider.maxValue / 2;
        //    }
        //}

        DestroyableObject[] obj = FindObjectsOfType<DestroyableObject>();

        int broken = 0;
        int unbroken = 0;
        for (int i = 0; i < obj.Length; i++)
        {
            
            int random = Random.Range(0, 100);


            if (random <= 50)
            {
                if (broken < obj.Length / 2)
                {
                    broken++;
                    obj[i].IsDestroyed = false;
                    obj[i].Break();
                }
            }
            else if (random > 50)
            {
                if (unbroken < obj.Length / 2)
                {
                    
                    unbroken++;
                    obj[i].IsDestroyed = true;
                    obj[i].Unbreak();
                }
            }
        }

        objectsDestroyed = broken;

        astronautSlider.gameObject.SetActive(false);
        alienSlider.gameObject.SetActive(false);
        background50Prc.SetActive(true);
        slider50Prc.gameObject.SetActive(true);

        slider50Prc.minValue = 0;
        slider50Prc.maxValue = obj.Length;
        slider50Prc.value = objectsDestroyed;

        GameController.gameController.AstronautScore = 0;
        GameController.gameController.AlienScore = 0;
    }

    private void OnEnable()
    {
        EventController.OnObjectUnBreak += AstronautSlider50Prc;
        EventController.OnObjectBreak += AlienSlider50Prc;


    }

    private void OnDisable()
    {
        if (GameController.gameController.GameConfig.Slider0Prc)
        {
            EventController.OnObjectUnBreak -= AstronautScore;
            EventController.OnObjectBreak -= AlienScore;
        }

        if (GameController.gameController.GameConfig.Slider50Prc)
        {
            EventController.OnObjectUnBreak -= AstronautSlider50Prc;
            EventController.OnObjectBreak -= AlienSlider50Prc;
        }
    }

    /// <summary>
    /// Adds ScorePoints to the Astronauts team and starts the Coroutine to adjust the Slider
    /// </summary>
    /// <param name="value"></param>
    private void AstronautScore(int value)
    {
        astronautScore += value;

        if (setAstronautSlider == null)
        {
            setAstronautSlider = SetAstronautSlider();
            StartCoroutine(setAstronautSlider);
        }
        Debug.Log("AstronautScore");
    }

    /// <summary>
    /// Adds ScorePOints to the Alines team and starts the Coroutine to adjust the Slider
    /// </summary>
    /// <param name="value"></param>
    private void AlienScore(int value)
    {
        alienScore += value;

        if (setAlienSlider == null)
        {
            setAlienSlider = SetAlienSlider();
            StartCoroutine(setAlienSlider);
        }
    }

    /// <summary>
    /// Adjusts the Astronauts Slider
    /// </summary>
    /// <param name="slider"></param>
    /// <returns></returns>
    private IEnumerator SetAstronautSlider()
    {
        while (astronautScore > 0)
        {
            SetSlider(ref astronautScore, ref alienScore, ref astronautSlider, ref alienSlider, ref setAstronautSlider, ref setAlienSlider);

            yield return new WaitForSeconds(.01f);
        }

        if (astronautScore > 0)
        {
            setAstronautSlider = SetAstronautSlider();
            StartCoroutine(setAstronautSlider);
        }
        else
        {
            setAstronautSlider = null;
        }
        Debug.Log("SetAstronautSlider");
    }

    /// <summary>
    /// Adjusts the Alien Slider
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAlienSlider()
    {
        while (alienScore > 0)
        {
            SetSlider(ref alienScore, ref astronautScore, ref alienSlider, ref astronautSlider, ref setAlienSlider, ref setAstronautSlider);

            yield return new WaitForSeconds(.01f);
        }

        if (alienScore > 0)
        {
            setAlienSlider = SetAlienSlider();
            StartCoroutine(setAlienSlider);
        }
        else
        {
            setAlienSlider = null;
        }
    }

    /// <summary>
    /// Sets the Slider to their new values
    /// </summary>
    /// <param name="score"></param>
    /// <param name="enemyScore"></param>
    /// <param name="slider"></param>
    /// <param name="enemySlider"></param>
    /// <param name="coroutine"></param>
    /// <param name="enemyCoroutine"></param>
    private void SetSlider(ref int score, ref int enemyScore, ref Slider slider, ref Slider enemySlider, ref IEnumerator coroutine, ref IEnumerator enemyCoroutine)
    {
        int step = GameController.gameController.GameConfig.SliderSpeed;

        // Checks if the score would be less then zero with the next step
        if (score - step < 0)
        {
            score -= step - (Mathf.Abs(score - step));
            slider.value += step - (Mathf.Abs(score - step));
        }
        // Checks if the slider would overlap with the next step
        else if (slider.value + step + enemySlider.value > slider.maxValue)
        {
            int tmpEnemyScore = enemyScore;

            if (score - tmpEnemyScore > 0)
            {
                score -= tmpEnemyScore;
                enemyScore -= tmpEnemyScore;

                // Checks if the score would be less then zero with the next step
                if (score - step < 0)
                {
                    score -= step - (Mathf.Abs(score - step));
                    slider.value += step - (Mathf.Abs(score - step));
                    enemySlider.value -= step - (Mathf.Abs(score - step));
                }
                else
                {
                    score -= step;
                    slider.value += step;
                    enemySlider.value -= step;
                }
            }
        }
        else
        {
            score -= step;
            slider.value += step;
        }
        // If any Team reached the max ScorePoints
        if (slider.value >= slider.maxValue)
        {
            if (!GameController.gameController.GameConfig.EndlessGame)
            {
                EventController.EndMatch();
            }

            if (enemyCoroutine != null)
            {
                StopCoroutine(enemyCoroutine);
                enemyCoroutine = null;
            }

            slider.value = slider.maxValue;

            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private void AstronautSlider50Prc(int value)
    {
        slider50Prc.value--;
        //StartCoroutine(AstronautSlider50PrcCoroutine());

        if (slider50Prc.value <= 0)
        {
            if (!GameController.gameController.GameConfig.EndlessGame)
            {
                UI.ui.AstronautWon = true;
                EventController.EndMatch();
            }
        }
    }

    //private IEnumerator AstronautSlider50PrcCoroutine()
    //{
    //    int step = GameController.gameController.GameConfig.SliderSpeed;

    //    for (int i = 0; i < step; i++)
    //    {
    //        slider50Prc.value++;

    //        yield return new WaitForSeconds(.01f);
    //    }

    //}

    private void AlienSlider50Prc(int value)
    {
        slider50Prc.value++;
        //StartCoroutine(AlienSlider50PrcCoroutine());

        if (slider50Prc.value >= slider50Prc.maxValue)
        {
            if (!GameController.gameController.GameConfig.EndlessGame)
            {
                UI.ui.AlienWon = true;
                EventController.EndMatch();
            }
        }
    }

    //private IEnumerator AlienSlider50PrcCoroutine()
    //{
    //    int step = GameController.gameController.GameConfig.SliderSpeed;

    //    for (int i = 0; i < step; i++)
    //    {
    //        slider50Prc.value--;

    //        yield return new WaitForSeconds(.01f);
    //    }

    //    yield return null;
    //}
}