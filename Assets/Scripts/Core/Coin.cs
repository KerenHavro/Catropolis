using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Coin : MonoBehaviour
{
    
    private CircleCollider2D circleCollider;
    private SpriteRenderer visual;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        visual = GetComponent<SpriteRenderer>();
    }

    private void CollectCoin()
    {
        circleCollider.enabled = false;
        visual.gameObject.SetActive(false);
        GameEventsManager.instance.miscEvents.CoinCollected();

    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            CollectCoin();
        }
    }
}
