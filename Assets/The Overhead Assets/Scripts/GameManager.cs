using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private GameObject player;
    private CharacterMoveController playerController;
    private Transform deadMenu;


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
        deadMenu = transform.FindChild("DeadMenu");
    }

    public void Setup(GameObject player)
    {
        this.player = player;
        playerController = player.GetComponent<CharacterMoveController>();
    }

    void Update()
    {
        if (playerController != null) {
            if (!playerController.stats.isAlive) {
                Time.timeScale = 0;
                deadMenu.gameObject.SetActive(true);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
