using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableObject : MonoBehaviour, IMineable
{
    public MineableItem mineableData;
    
   
    public void Mine()
    {
        mineableData.health--; // Decrement health
        if (mineableData.health <= 0)
        {
            DropItems();
            Destroy(gameObject);
        }


    }
    public void DropItems()
    {

        foreach (var dropItem in mineableData.dropItems)
        {
            GameObject NewItem = new GameObject(dropItem.name);
            Item newItem = NewItem.AddComponent<Item>();
            NewItem.AddComponent<Rigidbody2D>();
            NewItem.GetComponent<Rigidbody2D>().mass = 0;
            NewItem.GetComponent<Rigidbody2D>().gravityScale = 0;
            NewItem.GetComponent<Rigidbody2D>().angularDrag = 0;

            newItem.quantity = 1;
            newItem.itemName = dropItem.name;
            newItem.sprite = dropItem.sprite;
            newItem.itemDescription = "";
        }
    }
}
