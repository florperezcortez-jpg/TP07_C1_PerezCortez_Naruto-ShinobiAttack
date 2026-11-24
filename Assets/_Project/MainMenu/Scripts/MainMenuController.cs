using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Warning: El valor por defecto (y el serializado en MainMenu.unity) es "GamePlay", pero la escena se llama Gameplay.unity.
    // Que coincida exacto con el nombre del archivo, o mejor guardá el buildIndex o un const compartido para no depender de un string.
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
        // Warning: CloseOptions() reproduce PlayUIClick(), así que al cargar el menú suena un click sin que nadie haya tocado un botón.
        // Separá la lógica de mostrar paneles del sonido: llamá al sonido solo desde los handlers de los botones.
        CloseOptions();
    }
    private void Play()
    {
        // Suggestion: El click se pide después de LoadScene, cuando la escena ya se está descargando (lo mismo en Quit con Application.Quit).
        // Reproducí el sonido antes de cambiar de escena o de salir.
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
