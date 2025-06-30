using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemActive : MonoBehaviour
{
    public float Durasi = 30f;
    private bool gameOver = false;

    private int Score = 0;
    public TextMeshProUGUI scoreText;

    // Player health variables
    public float maxHealth = 100f;
    public float currHealth;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        // Initialize player health
        currHealth = maxHealth;
        UpdateHealthUI();
    }

    private void OnTriggerEnter(Collider ColliderGuwe)
    {
        if (ColliderGuwe.CompareTag("BLU"))
        {
            // Handle Blue item (e.g., increase score)
            Destroy(ColliderGuwe.gameObject);
            Score += 10;
        }
        else if (ColliderGuwe.CompareTag("RED"))
        {
            // Handle Red item (e.g., decrease player health)
            Destroy(ColliderGuwe.gameObject);
            Score -= 20;

            // Decrease player health when colliding with a RED item
            currHealth -= 20f; // You can adjust this damage value as needed

            // Check if health is below 0
            if (currHealth <= 0)
            {
                currHealth = 0;
                GameOver();
            }

            UpdateHealthUI();
        }
        else if (ColliderGuwe.CompareTag("GRN"))
        {
            // Handle Green item (e.g., game over)
            Time.timeScale = 0f; // Freezes game
            Destroy(gameObject);
            gameOver = true;
        }
        else if (ColliderGuwe.CompareTag("Enemy"))
        {
            // Handle Enemy (e.g., game over or taking damage)
            Time.timeScale = 0f;
            // You can add more logic for enemy interactions if needed
        }
    }

    private void Update()
    {
        // Update score UI
        scoreText.text = "Score: " + Score.ToString();

        // Cooldown system for duration
        if (Durasi > 0)
        {
            Durasi -= Time.deltaTime;
        }

        if (Durasi <= 0)
        {
            Durasi = 0;
            Time.timeScale = 0f; // Freezes game
        }
    }

    // Function to update health UI text
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currHealth.ToString();
        }
    }

    // Handle game over logic (optional)
    private void GameOver()
    {
        // Handle the game over logic (e.g., show game over UI, stop game)
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
        // Show game over UI or transition to a new scene here if needed.
    }
}
