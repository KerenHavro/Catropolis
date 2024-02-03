using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;
    // Start is called before the first frame update
    void Start()
    {
        InventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory")&& menuActivated)
        {
            InventoryMenu.SetActive(false);
            menuActivated = false;
            Time.timeScale = 1f;
        }
       else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
      for(int i = 0; i< itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull== false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite);
                return;
            }
        }
    }
}
