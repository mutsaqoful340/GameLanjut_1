// === [Notes] ===
// Item berupa power up
// Item menghilang
// Item masuk jangkauan
// Interaksi item dengan F

// ??? [Kapan pakai Collider & Trigger?] ???
// Trigger = Di objek yang memang diizinkan overlapping. (Scattered items on the map, etc)
// Collision = Simulation game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
            
public class ItemActive : MonoBehaviour
{
    public float Durasi = 30f;
    private bool gameOver = false;
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider ColliderGuwe)
    {
        if (ColliderGuwe.CompareTag("BLU"))
        {
            Destroy(ColliderGuwe.gameObject);
            Durasi += 5f;
        }

        else if (ColliderGuwe.CompareTag("RED"))
        {
            Destroy(ColliderGuwe.gameObject);
            Durasi -= 2f;
        }

        else if (ColliderGuwe.CompareTag("GRN"))
        {
            Time.timeScale = 0f; //Freezes game
            Destroy(gameObject);
            gameOver = true;
        }
    }

    private void Update()
    {

        // Cooldown system
        if (Durasi > 0)
        {
            Durasi -= Time.deltaTime;
        }

        if (Durasi < 0)
        {
            Durasi = 0;
            Time.timeScale = 0f; //Freezes game
        }
    }
}
