using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject HotBarMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    
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
        for (int i = 0; i < 7; i++)
        {
            itemSlot[i].transform.SetParent(newParentTransform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            HotBarMenu.SetActive(true);
            for (int i = 0; i < 7; i++)
            {
               itemSlot[i].transform.SetParent(newParentTransform);
            }
            InventoryMenu.SetActive(false);
            menuActivated = false;
            Time.timeScale = 1f;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            for (int i = 0; i < 7; i++)
            {
               itemSlot[i].transform.SetParent(oldParentTransform);
            }
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
            HotBarMenu.SetActive(false);
        }
    }
    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if(itemSOs[i].itemName== itemName)
            {
              bool usable=  itemSOs[i].UseItem();
                return usable;
            }

        }
        return false;
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);

                return leftOverItems;
            }
        }
        return quantity;
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
