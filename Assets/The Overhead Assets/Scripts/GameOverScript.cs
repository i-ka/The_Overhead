using UnityEngine;

public class GameOverScript : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            GameManager.instance.RestartLevel();
        } else {
            Destroy(other);
        }
    }
}
