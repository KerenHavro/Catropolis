using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    //SLOT APPEARANCE//
    [SerializeField]
    private Image slotImage;
    [SerializeField]
    private TMP_Text slotName;
    [SerializeField]
    private Image playerDisplayImage;

    //SLOT DATA//
    [SerializeField]
    private ItemType itemType = new ItemType();
    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    private InventoryManager inventoryManager;

    //OTHER VARIABLES//
    private bool slotInUse;
    [SerializeField]
    private GameObject selectedShader;

    [SerializeField]
    private bool thisItemSelected;

    [SerializeField]
    private Sprite emptySprite;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button== PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }

    }

     void OnRightClick()
    {
        UnEquipGear();
    }

     void OnLeftClick()
    {
        if (thisItemSelected && slotInUse)
            UnEquipGear();
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription)
    {
        if (slotInUse)
            UnEquipGear();

        //update Image
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;


        //update Data
        this.itemName = itemName;
        this.itemDescription = itemDescription;

        //update the display Image
        playerDisplayImage.sprite = itemSprite;

        slotInUse = true;
    }
    public void UnEquipGear()
    {
        inventoryManager.DeselectAllSlots();

        inventoryManager.AddItem(itemName, 1, itemSprite, itemDescription, itemType);

        this.itemSprite = emptySprite;
        slotImage.sprite = this.emptySprite;
        slotName.enabled = true;

        playerDisplayImage.sprite = emptySprite;
    }

}
