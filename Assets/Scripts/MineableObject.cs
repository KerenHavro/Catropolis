using UnityEngine;

public class MineableObject : MonoBehaviour
{
    public MineableItem mineableItem;

    private int currentHealth;

    void Start()
    {
        currentHealth = mineableItem.maxHealth;
    }

    // Method to handle mining interaction
    public void Mine()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            DropItem();
        }
        // Optionally, you can play mining animation or sound here
    }

    // Method to drop collectible item
    void DropItem()
    {
        Instantiate(mineableItem.collectibleItemPrefab, transform.position, Quaternion.identity);
        // Optionally, you can add particle effects or other effects here
        Destroy(gameObject); // Destroy the mineable object after it's mined
    }
}