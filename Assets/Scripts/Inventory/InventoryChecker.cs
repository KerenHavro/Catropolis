using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryChecker : MonoBehaviour
{

    //public InventoryManager inventoryManager; // Reference to the InventoryManager script
    public ItemSlot[] itemSlots;

    public int ItemCount; // To keep track of previous wood count
    public int removedItem=0;
    public int CalculateItemCount(string itemToCount)
    {
        ItemCount = 0;
        // Loop through inventory slots to count wood
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].itemName == itemToCount)
            {
                ItemCount = itemSlots[i].quantity;
                Debug.Log(itemToCount);

            }
        }


        return ItemCount;


    }

    public void RemoveItemCount(string itemToRemove, int howMuchToRemove)
    {

            // Loop through inventory slots to count wood
            for (int i = 0; i < itemSlots.Length; i++)
            {
            if ((itemSlots[i].itemName == itemToRemove) && removedItem != howMuchToRemove)
            {
                int thisQuantity= itemSlots[i].quantity;
                for (int j = 0; j < thisQuantity; j++)
                {
                    itemSlots[i].GetComponent<ItemSlot>().SubThisItem();
                    removedItem++;
                    Debug.Log(itemToRemove);

                }
            }
            }
        }
}
