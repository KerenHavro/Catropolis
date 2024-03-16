using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryChecker : MonoBehaviour
{
    //public InventoryManager inventoryManager; // Reference to the InventoryManager script
    public ItemSlot[] itemSlots;

    public int woodCount; // To keep track of previous wood count

    private void Update()
    {
        CalculateWoodCount();
        FiveWood();
    }
    public int CalculateWoodCount()
    {
        woodCount = 0;
        // Loop through inventory slots to count wood
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].itemName == "Wood")
            {
                woodCount = itemSlots[i].quantity;
                Debug.Log("wood!");

            }
        }
        
        
        return woodCount;
        

    }
  private void FiveWood()
    {
        if(woodCount>=5)
      GameEventsManager.instance.miscEvents.WoodCollected(5);
        if (woodCount < 5)
            Debug.Log("less");
            GameEventsManager.instance.miscEvents.WoodCollected(0);
    }
  
}
