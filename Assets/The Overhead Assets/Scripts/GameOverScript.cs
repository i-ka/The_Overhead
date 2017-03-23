﻿using UnityEngine;

public class GameOverScript : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            other.GetComponent<CharacterMoveController>().m_stats.isAlive = false;
        } else {
            Destroy(other.gameObject);
        }
    }
}
