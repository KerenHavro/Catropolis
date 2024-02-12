using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{


    //=====ITEM DATA=====
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;





    //=====ITEM SLOT=====
    [SerializeField]
    private Image itemImage;

    //=====EQUIPED SLOTS=====
    [SerializeField]
    private EquippedSlot headSlot, cloakSlot, bodySlot, legsSlot, mainHandSlot, offHandSlot, relicSlot, feetSlot;

    [SerializeField]
    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;
    public void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentSOLibrary>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        //check if slot is already Full
        if (isFull)
            return quantity;
        //Update ITEM TYPE
        this.itemType = itemType;

        // Update NAME
        this.itemName = itemName;

        // Update IMAGE
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        //Update DESCRIPTION
        this.itemDescription = itemDescription;

        //Update QUANTITY
        this.quantity = 1;
        isFull = true;

        return 0;
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

    public void OnLeftClick()
    {
        if (isFull)
        {
            if (thisItemSelected)
            {

                EquipGear();

            }
            else
            {
                inventoryManager.DeselectAllSlots();
                selectedShader.SetActive(true);
                thisItemSelected = true;
                for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
                {
                    if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                        equipmentSOLibrary.equipmentSO[i].PreviewEquipment();
                }

            }
        }
        else
        {
            GameObject.Find("StatManager").GetComponent<PlayerStats>().TurnOffPreviewStats();
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }
    private void EquipGear()
    {
        if (itemType == ItemType.head)
            headSlot.EquipGear(itemSprite, itemName, itemDescription, itemType);
        if (itemType == ItemType.cloak)
            cloakSlot.EquipGear(itemSprite, itemName, itemDescription, itemType);
        if (itemType == ItemType.body)
            bodySlot.EquipGear(itemSprite, itemName, itemDescription, itemType);
        if (itemType == ItemType.legs)
            legsSlot.EquipGear(itemSprite, itemName, itemDescription, itemType);
        if (itemType == ItemType.mainHand)
            mainHandSlot.EquipGear(itemSprite, itemName, itemDescription, itemType);
        if (itemType == ItemType.offHand)
            offHandSlot.EquipGear(itemSprite, itemName, itemDescription, itemType);
        if (itemType == ItemType.relic)
            relicSlot.EquipGear(itemSprite, itemName, itemDescription, itemType);
        if (itemType == ItemType.feet)
            feetSlot.EquipGear(itemSprite, itemName, itemDescription, itemType);
        EmptySlot();
    }

    private void EmptySlot()
    {
        itemName = "";

        itemImage.sprite = emptySprite;
        itemSprite = emptySprite;
        this.itemDescription = "";
        isFull = false;
        this.quantity = 0;


    }

    public void OnRightClick()
    {

        if (this.quantity != 0)
        {
            //create a ne item
            GameObject itemToDrop = new GameObject(itemName);
            Item newItem = itemToDrop.AddComponent<Item>();
            itemToDrop.AddComponent<Rigidbody2D>();
            itemToDrop.GetComponent<Rigidbody2D>().mass = 0;
            itemToDrop.GetComponent<Rigidbody2D>().gravityScale = 0;
            itemToDrop.GetComponent<Rigidbody2D>().angularDrag = 0;

            newItem.quantity = 1;
            newItem.itemName = itemName;
            newItem.sprite = itemSprite;
            newItem.itemDescription = itemDescription;
            newItem.itemType = itemType;

            //Create and modify the SR
            SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
            sr.sprite = itemSprite;
            sr.sortingOrder = 5;
            //sr.sortingLayerName = "UI";

            //Add a collider
            itemToDrop.AddComponent<BoxCollider2D>();
            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(0, 2f, 0);

            //sub the item
            this.quantity -= 1;
            if (this.quantity <= 0)
                EmptySlot();

        }
    
    }

}
