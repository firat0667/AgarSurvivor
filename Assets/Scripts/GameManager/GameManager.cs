using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    private static event Action _onGamePaused;
    [SerializeField] private GameObject _mainMenuCanvas;
    public Button ResumeButton;
    public Button RestartButton;
    private Animator _mainmenuAnim;
    public TextMeshProUGUI HighScoreText;
    public int HighScore;
    public TextMeshProUGUI ScoreText;
    public int Score;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _mainmenuAnim=_mainMenuCanvas.GetComponent<Animator>();
        Time.timeScale = 0;
        GetScore();
    }
    void OnEnable()
    {
       _onGamePaused += PauseGame;
        ResumeButton.onClick.AddListener(ResumeGame);
        RestartButton.onClick.AddListener(RestartGame);
    }

    void OnDisable()
    {
        _onGamePaused -= PauseGame;
        ResumeButton.onClick.RemoveListener(ResumeGame);
    }
    public void Addscore()
    {
        Score++;
        PlayerPrefs.SetInt("Score", Score);
        ScoreText.text = Score.ToString();
    }
    public void DeadScore()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("Score", HighScore);
        }
            

    }
   public void GetScore()
    {
        HighScore = PlayerPrefs.GetInt("Score");
        HighScoreText.text = HighScore.ToString();
    }
    void PauseGame()
    {
        Time.timeScale = 0; 
        _mainMenuCanvas.SetActive(true);
        _mainmenuAnim.updateMode = AnimatorUpdateMode.UnscaledTime; 
        _mainmenuAnim.speed = 1; 
        _mainmenuAnim.Play(AnimatorTag.MainMenuAnim_AnimTag); 
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _mainmenuAnim.Play(AnimatorTag.MainMenuAnimReturn_AnimTag); 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ResumeGameCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f); // UnscaledTime kullanarak animasyonu tamamla
        Time.timeScale = 1; // Oyun devam etsin
      //  _mainMenuCanvas.SetActive(false); // Menü kapanır
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _onGamePaused?.Invoke();
        }
    }
}
