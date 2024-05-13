using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip CoinPickupSFX;
    [SerializeField] int pointsForCoinsPickup = 1;
    [SerializeField] float volume = 1f;
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToCoins(pointsForCoinsPickup);
            AudioSource.PlayClipAtPoint(CoinPickupSFX, Camera.main.transform.position, volume);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
