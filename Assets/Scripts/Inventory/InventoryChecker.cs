using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryChecker : MonoBehaviour
{

    //public InventoryManager inventoryManager; // Reference to the InventoryManager script
    public ItemSlot[] itemSlots;

    public int woodCount; // To keep track of previous wood count
    public int removedWood=0;
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

    public void RemoveWoodCount()
    {

            // Loop through inventory slots to count wood
            for (int i = 0; i < itemSlots.Length; i++)
            {
            if ((itemSlots[i].itemName == "Wood") && removedWood != 5)
            {
                int thisQuantity= itemSlots[i].quantity;
                for (int j = 0; j < thisQuantity; j++)
                {
                    itemSlots[i].GetComponent<ItemSlot>().SubThisItem();
                    removedWood++;
                    Debug.Log("woodremoved");

                }
            }
            }
        }
}
