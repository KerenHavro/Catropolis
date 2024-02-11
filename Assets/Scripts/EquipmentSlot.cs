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

    [SerializeField]
    private TMP_Text quantityText;



    //=====ITEM SLOT=====
    [SerializeField]
    private Image itemImage;



    [SerializeField]
    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    public void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

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
        if (thisItemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            if (usable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                    EmptySlot();
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;

        }
    }

    private void EmptySlot()
    {
        itemName = "";
        quantityText.text = 0.ToString();
        quantityText.enabled = false;
        itemImage.sprite = null;
        itemSprite = emptySprite;
        this.itemDescription = "";

    }

    public void OnRightClick()
    {
        if (quantityText.text != 0.ToString())
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
            quantityText.text = this.quantity.ToString();
            if (this.quantity <= 0)
                EmptySlot();

        }
        else
            return;
    }

}
