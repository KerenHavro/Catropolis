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
    private EquipmentSOLibrary equipmentSOLibrary;

    //OTHER VARIABLES//
    private bool slotInUse;
    [SerializeField]
    public GameObject selectedShader;

    [SerializeField]
    public bool thisItemSelected;

    [SerializeField]
    private Sprite emptySprite;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentSOLibrary>();

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
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
            for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
            {
                if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName&& slotInUse)
                    equipmentSOLibrary.equipmentSO[i].PreviewEquipment();
            }
        }
    }

    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription, ItemType itemType)
    {
        if (slotInUse)
            UnEquipGear();

        //update Image
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        this.itemType = itemType;


        //update Data
        this.itemName = itemName;
        this.itemDescription = itemDescription;

        //update the display Image
        playerDisplayImage.sprite = itemSprite;
        // update player stats
        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {

            {
                if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                    equipmentSOLibrary.equipmentSO[i].EquipItem();
                slotInUse = true;
            }
        }

    }
    public void UnEquipGear()
    {
        if (slotInUse)
        {
            slotInUse = false;
            inventoryManager.DeselectAllSlots();

            inventoryManager.AddItem(itemName, 1, itemSprite, itemDescription, itemType);

            this.itemSprite = emptySprite;
            slotImage.sprite = this.emptySprite;
            slotName.enabled = true;

            playerDisplayImage.sprite = emptySprite;
            for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
            {
                if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                    equipmentSOLibrary.equipmentSO[i].UnEquipItem();
            }
            GameObject.Find("StatManager").GetComponent<PlayerStats>().TurnOffPreviewStats();

        }
        else
        {
            GameObject.Find("StatManager").GetComponent<PlayerStats>().TurnOffPreviewStats();
        }
    }
}
