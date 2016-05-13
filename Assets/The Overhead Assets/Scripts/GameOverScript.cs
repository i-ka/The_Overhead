using UnityEngine;

public class GameOverScript : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.RestartLevel();
    }
}
