using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemActive : MonoBehaviour
{
    public float Durasi = 30f;

    private int Score = 0;
    public TextMeshProUGUI scoreText;

    // Player health variables
    public float maxHealth = 100f;
    public float currHealth;
    public TextMeshProUGUI healthText;

    public Animator Anim;
    public GameObject GameOverPanel;
     private void IsDeath()
    {

    }

    private void Start()
    {
        // Initialize player health
        currHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider ColliderGuwe)
    {
        if (ColliderGuwe.CompareTag("BLU"))
        {
            Destroy(ColliderGuwe.gameObject);
            Score += 10;
        }
        else if (ColliderGuwe.CompareTag("RED"))
        {
            Destroy(ColliderGuwe.gameObject);
            Score -= 20;
        }
        else if (ColliderGuwe.CompareTag("GRN"))
        {
            // Handle Green item (e.g., game over)
            Time.timeScale = 0f; // Freezes game
            Destroy(gameObject);
        }

        else if (ColliderGuwe.CompareTag("Tongkat"))
        {
            currHealth -= 20f;
        }

        if (currHealth == 0)
        {
            GameOverPanel.SetActive(true);
            
        }
        //else if (ColliderGuwe.CompareTag("Enemy"))
        //{
        //    currHealth -= 10;
        //}
    }

    private void Update()
    {
        // Update score UI
        scoreText.text = "Score: " + Score.ToString();
        healthText.text = "Health: " + currHealth.ToString();        // Cooldown system for duration
        if (currHealth == 0)
        {
            Anim.SetTrigger("IsDeath");
            Debug.Log("mati");

            Time.timeScale = 0f;
        }
    }

    //private void GameOver()
    //{
    //    // Handle the game over logic (e.g., show game over UI, stop game)
    //    Debug.Log("Game Over!");
    //    Time.timeScale = 0f;
    //    // Show game over UI or transition to a new scene here if needed.
    //}
}
