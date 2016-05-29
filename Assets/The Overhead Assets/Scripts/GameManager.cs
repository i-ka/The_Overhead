using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public bool gamePaused;
    private GameObject player;
    private CharacterMoveController playerController;
    private Transform deadMenu;
    private Transform mainMenu;
    private Transform pauseMenu;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        mainMenu = transform.FindChild("MainMenu");
        deadMenu = transform.FindChild("DeadMenu");
        pauseMenu = transform.FindChild("PauseMenu");
        mainMenu.gameObject.SetActive(true);
        deadMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        gamePaused = true;
    }

    public void Setup(GameObject player)
    {
        this.player = player;
        playerController = player.GetComponent<CharacterMoveController>();
        deadMenu.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerController != null) {
            if (!playerController.stats.isAlive) {
                Time.timeScale = 0;
                deadMenu.gameObject.SetActive(true);
                gamePaused = true;
            } else {
                Time.timeScale = 1;
            }
        }
    }

    public void StartGame()
    {
        gamePaused = false;
        mainMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        gamePaused = false;
        deadMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseGame()
    {
        gamePaused = true;
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        gamePaused = false;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void ToMainMenu()
    {
        gamePaused = true;
        SceneManager.LoadScene(0);
        pauseMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
