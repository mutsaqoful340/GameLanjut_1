using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongkarGameOver : MonoBehaviour
{
    public GameObject GameOverPanel;

    private void OnTriggerEnter(Collider TongkatCollider)
    {
        if (TongkatCollider.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            Debug.Log("Tongkat kena");
            GameOverPanel.SetActive(true);
        }
    }
}
