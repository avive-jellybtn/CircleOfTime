using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [HideInInspector] public static UIController instance;

    [SerializeField] private Text _titleText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _tapToStartText;
    [SerializeField] private Text _waveText;
    [SerializeField] private Text _enemiesText;
    [SerializeField] private Text _rememberText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    public void ResetUI()
    {
        _waveText.text = "Wave\n";
        _enemiesText.text = "Enemies Left\n";
        _scoreText.text = "Score\n0";
    }


    public void FadeText(Text t, float to, float time, float delay = 0.0f, bool pingPong = false)
    {
        if (pingPong)
            t.DOFade(to, time).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        else
            t.DOFade(to, time).SetEase(Ease.OutQuad).SetDelay(delay);
    }


    public void ShowMenu()
    {
        FadeText(_titleText, 1, 1);
        FadeText(_tapToStartText, 1, 1);
        FadeText(_rememberText, 1, 1);
    }

    public void UnshowMenu()
    {
        FadeText(_titleText, 0, 1);
        FadeText(_tapToStartText, 0, 1);
        FadeText(_rememberText, 0, 1);
    }

    public void ShowGameUI()
    {
        FadeText(_scoreText, 1, 0.5f, 1);
        FadeText(_waveText, 1, 0.5f, 1);
        FadeText(_enemiesText, 1, 0.5f, 1);
    }

    public void UnshowGameUI()
    {
        FadeText(_scoreText, 0, 0.2f);
        FadeText(_waveText, 0, 0.2f);
        FadeText(_enemiesText, 0, 0.2f);
    }
 
    public void UpdateScore(int score)
    {
        _scoreText.text = "Score \n" + score.ToString("N0");
    }

    public void UpdateNumOfEnemies(int numOfEnemies)
    {
        _enemiesText.text = "Enemies Left \n" + numOfEnemies.ToString("N0");
    }

    public void UpdateWaveNum(int waveNum)
    {
        _waveText.text = "Wave \n" + waveNum.ToString("N0");
    }

}

