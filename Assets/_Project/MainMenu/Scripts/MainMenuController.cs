using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GamePlay";
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;


    private void OnEnable()
    {
        playButton.onClick.AddListener(Play);
        settingsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(Quit);
        backButton.onClick.AddListener(CloseOptions);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(Play);
        settingsButton.onClick.RemoveListener(OpenOptions);
        quitButton.onClick.RemoveListener(Quit);
        backButton.onClick.RemoveListener(CloseOptions);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        CloseOptions();
    }
    private void Play()
    {
        SceneManager.LoadScene(gameSceneName);
        AudioManager.Instance.PlayUIClick();
    }

    private void OpenOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
        AudioManager.Instance.PlayUIClick();
    }
    private void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
        AudioManager.Instance.PlayUIClick();
    }
    private void Quit()
    {
        Application.Quit();
        AudioManager.Instance.PlayUIClick();
        Debug.Log("Salir del juego");
    }
}
