using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _mainmenuAnim=_mainMenuCanvas.GetComponent<Animator>();
        Time.timeScale = 0;
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
    void PauseGame()
    {
        Time.timeScale = 0; // Oyunu durdur
        _mainMenuCanvas.SetActive(true);
        _mainmenuAnim.updateMode = AnimatorUpdateMode.UnscaledTime; // Animasyonları Unscaled Time ile oynat
        _mainmenuAnim.speed = 1; // Animasyon normal hızda ileri oynasın
        _mainmenuAnim.Play(AnimatorTag.MainMenuAnim_AnimTag); // Ana menü animasyonunu başlat
    }

    public void ResumeGame()
    {
        // UnscaledTime modunda animasyonu ters oynatıyoruz
        Time.timeScale = 1; // Oyunu durdur
        _mainmenuAnim.Play(AnimatorTag.MainMenuAnimReturn_AnimTag); // Ana menü animasyonunu başlat
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
