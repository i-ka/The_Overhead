using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private GameObject player;
    private CharacterMoveController playerController;
    private Transform deadMenu;
    private Transform mainMenu;

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
        mainMenu.gameObject.SetActive(true);
        deadMenu.gameObject.SetActive(false);
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
            } else {
                Time.timeScale = 1;
            }
        }
    }

    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        deadMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
