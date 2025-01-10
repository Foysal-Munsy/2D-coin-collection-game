// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class Collector : MonoBehaviour
// {
//     bool isCollected = false;
//     void HideCoin()
//     {
//         var sprite = GetComponent<SpriteRenderer>();
//         if (sprite != null)
//         {
//             sprite.enabled = false;
//         }
//         var collider = GetComponent<CircleCollider2D>();
//         if (collider != null)
//         {
//             collider.enabled = false;
//         }
//     }
//     void CollectCoin()
//     {
//         isCollected = true;
//         HideCoin();
//         GameManager gm = GameManager.GetInstance();
//         gm.CollectCoins();
//         print("coins collected = " + gm.GetCollectedCoins());
//     }
//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.tag == "Player")
//         {
//             CollectCoin();
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private bool isCollected = false; // Private variable to track collection status

    void HideCoin()
    {
        // Disable the sprite renderer to hide the coin visually
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.enabled = false;
        }

        // Disable the collider to prevent further interactions
        var collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    void CollectCoin()
    {
        if (!isCollected) // Ensure the coin is not collected multiple times
        {
            isCollected = true; // Mark the coin as collected
            HideCoin(); // Hide the coin
            GameManager gm = GameManager.GetInstance();
            if (gm != null) // Ensure GameManager instance exists
            {
                gm.CollectCoins(); // Update collected coins in the GameManager
                Debug.Log("Coins collected = " + gm.GetCollectedCoins());
            }
            else
            {
                Debug.LogError("GameManager instance is null. Make sure it's properly initialized.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Trigger collection if the colliding object is tagged as "Player"
        if (collision.CompareTag("Player") && !isCollected)
        {
            CollectCoin();
        }
    }
}
