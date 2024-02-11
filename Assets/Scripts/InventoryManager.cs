using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject HotBarMenu;
    public GameObject EquipmentMenu;
   
    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;

    [SerializeField]
    Transform newParentTransform;
    [SerializeField]
    Transform oldParentTransform;

    public ItemSO[] itemSOs;
    // Start is called before the first frame update
    void Start()
    {
        HotBarMenu.SetActive(true);
        InventoryMenu.SetActive(false);
        EquipmentMenu.SetActive(false);
        for (int i = 0; i < 7; i++)
        {
            itemSlot[i].transform.SetParent(newParentTransform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("InventoryMenu"))
        {
            Inventory();
        }
        if (Input.GetButtonDown("EquipmentMenu"))
        {
            Equipment();
        }

    }

    private void Equipment()
    {

        if (EquipmentMenu.activeSelf)
        {
            HotBarMenu.SetActive(true);
            for (int i = 0; i < 7; i++)
            {
                itemSlot[i].transform.SetParent(newParentTransform);
            }
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);

            Time.timeScale = 1f;
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                itemSlot[i].transform.SetParent(oldParentTransform);
            }
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(true);

            HotBarMenu.SetActive(false);
        }
    }


    void Inventory()
    {
        if (InventoryMenu.activeSelf)
        {
            HotBarMenu.SetActive(true);
           for (int i = 0; i < 7; i++)
            {
                itemSlot[i].transform.SetParent(newParentTransform);
            }
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);

            Time.timeScale = 1f;
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                itemSlot[i].transform.SetParent(oldParentTransform);
            }
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);

            HotBarMenu.SetActive(false);
        }
    }
    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }

        }
        return false;
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        if(itemType== ItemType.consumable || itemType == ItemType.collectable ||itemType == ItemType.crafting)
        {

            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    if (leftOverItems > 0)
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);

                    return leftOverItems;
                }
            }
            return quantity;
        }
        else
        {

            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (equipmentSlot[i].isFull == false && equipmentSlot[i].itemName == itemName || equipmentSlot[i].quantity == 0)
                {
                    int leftOverItems = equipmentSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    if (leftOverItems > 0)
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);

                    return leftOverItems;
                }
            }
            return quantity;
        }

    }

 

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;

        }
    }

}

public enum ItemType
{
    consumable,
    crafting,
    collectable,
    head,
    cloak,
    body,
    mainHand,
    offHand,
    relic,
    feet,
    none,
};
