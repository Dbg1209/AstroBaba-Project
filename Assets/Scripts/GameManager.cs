
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIUXManager uiUXManager;
    public static GameManager Instance;
    public GameState state;

    void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        MainMenu
    }

    //Estado de inicio
    void Start()
    {
        state = GameState.Playing;
    }

    void Update()
    {
        // Lógica para pausar el juego cuando es Game Over
        if (state == GameState.GameOver)
        {
            Time.timeScale = 0f;
        }
    }

    //Cambiar a la escena de MainMenu
    public void LoadMainMenu()
    {
        uiUXManager.HideVictory();
        uiUXManager.HideGameOver();
        uiUXManager.ShowMainMenu();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        state = GameState.MainMenu;
    }

    // Función para cambiar a la escena StevenGym
    public void LoadScene()
    {
        uiUXManager.HideMainMenu();
        Time.timeScale = 1f;
        SceneManager.LoadScene("StevenGym");
        state = GameState.Playing;
    }

    //Pausar el juego
    public void PauseGame()
    {
        Time.timeScale = 0f;
        state = GameState.Paused;
    }

    //Reanudar el juego
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        state = GameState.Playing;
    }

    //Reiniciar la escena actual
    public void RestartScene()
    {
        uiUXManager.HideVictory();
        uiUXManager.HideGameOver();
        uiUXManager.HideMainMenu();
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        state = GameState.Playing;
    }

    public void GameOver()
    {   
        uiUXManager.ShowGameOver();
        state = GameState.GameOver;
    }

}
