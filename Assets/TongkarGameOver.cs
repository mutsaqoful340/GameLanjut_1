using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongkarGameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider TongkatCollider)
    {
        if (TongkatCollider.CompareTag("Player"))
        {
            Debug.Log("Tongkat kena");
        }
    }
}
