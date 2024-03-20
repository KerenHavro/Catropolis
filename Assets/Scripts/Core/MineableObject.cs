using UnityEngine;

public class MineableObject : MonoBehaviour
{
    public MineableItem mineableItem;

    [SerializeField]
    private int currentHealth;

    void Start()
    {
        currentHealth = mineableItem.maxHealth;
    }

    // Method to handle mining interaction
    public void Mine(int mine, int chop)
    {
        if (this.gameObject.name == "Rock")
        {
            currentHealth= currentHealth- mine ;

            if (currentHealth <= 0)
            {
                DropItems();
            }
        }
        if (this.gameObject.name == "Tree")
        {
            currentHealth = currentHealth - chop;

            if (currentHealth <= 0)
            {
                DropItems();
            }
        }
        // Optionally, you can play mining animation or sound here
    }

    // Method to drop collectible items
    void DropItems()
    {
        foreach (var itemDrop in mineableItem.collectibleItems)
        {
            if (Random.value <= itemDrop.dropChance)
            {
                Instantiate(itemDrop.collectibleItemPrefab, transform.position, Quaternion.identity);
                // Optionally, you can add particle effects or other effects here
            }
        }

        Destroy(gameObject); // Destroy the mineable object after it's mined
    }
}
