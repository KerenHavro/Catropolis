using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using static UnityEngine.Rendering.DebugUI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
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

    [SerializeField]
    public int maxNumberOfItems;

    //=====ITEM SLOT=====
   [SerializeField]
    public Image itemImage;

    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;



    [SerializeField]
    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    public void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

    }
    public void Update()
    {
        if (quantity <= 0)
        {

        }
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
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            //return the LEFTOVERS
            
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        //Update qyantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

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
           bool usable= inventoryManager.UseItem(itemName);
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
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
    }

    private void EmptySlot()
    {
        itemName = "";
        quantityText.text = "0";
        quantityText.enabled = false;
        itemSprite = emptySprite;
        itemImage.sprite = emptySprite;
        itemDescription = "";
        ItemDescriptionNameText.text = "";
        ItemDescriptionText.text = "";

        // Find the Panel GameObject
        Transform panelTransform = transform.Find("Panel");
        if (panelTransform != null)
        {
            // Find the Image component of the Panel GameObject and set its sprite to the empty sprite
            Image imageChild = panelTransform.GetComponentInChildren<Image>();
            if (imageChild != null)
            {
                imageChild.sprite = emptySprite;
            }
        }
    }


    public void OnRightClick()
    {
        if (quantityText.text!= 0.ToString())
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

    public void UpdateUI()
    {
        // Update quantity text
        this.quantityText.text = quantity.ToString();
        // Update item image
        this.itemImage.sprite = itemSprite;

        // Update item description image
        this.itemDescriptionImage.sprite = itemSprite; // or whatever you need to set here

        // Update item description name text
        this.ItemDescriptionNameText.text = itemName;

        // Update item description text
        this.ItemDescriptionText.text = itemDescription;
        Image itemImageChild = transform.Find("ItemImage").GetComponent<Image>();
        itemImageChild.sprite = itemSprite;

    }
}
