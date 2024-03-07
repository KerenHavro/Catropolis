
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Canvas canvas;
    public Image image;
    private GameObject emptyImage;
    [SerializeField]
    private Sprite empty;
    public ItemSlot itemSlot;

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
    [SerializeField]


    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;


    private CanvasGroup canvasGroup;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Transform parentBeforeDrag;
    [HideInInspector] public Transform dparentAfterDrag;
    [HideInInspector] public Transform dparentBeforeDrag;


    private void Awake()
    {
        parentBeforeDrag = transform.parent;
        dparentBeforeDrag = parentBeforeDrag.transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
    }




    public void OnBeginDrag(PointerEventData eventData)
    {
        if (image.sprite != empty)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            canvasGroup.alpha = .6f;
            image.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (image.sprite != empty)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (image.sprite != empty)
        {
            canvasGroup.alpha = 1f;
            transform.SetParent(parentAfterDrag);
            dparentAfterDrag = parentAfterDrag.transform.parent;
            image.raycastTarget = true;
            if (parentBeforeDrag != parentAfterDrag)
            {
                SlotUpdate();

                emptyImage = new GameObject("empty");
                emptyImage.transform.SetParent(parentBeforeDrag);
                emptyImage.AddComponent<Image>().sprite = empty;
                // Set empty sprite
                emptyImage.AddComponent<CanvasGroup>();
                DragAndDrop dragAndDrop = emptyImage.AddComponent<DragAndDrop>();
                dragAndDrop.canvas = GameObject.Find("InventoryCanvas").GetComponent<Canvas>();
                emptyImage.transform.SetAsLastSibling();

                parentBeforeDrag = parentAfterDrag;

                ItemSlot itemSlotBeforeDrag = dparentBeforeDrag.GetComponent<ItemSlot>();
                ItemSlot itemSlotAfterDrag = dparentAfterDrag.GetComponent<ItemSlot>();

                itemSlotBeforeDrag.UpdateUI();
                itemSlotAfterDrag.UpdateUI();

            }
        }

    }
    public void SlotUpdate()
    {
        // Get references to the ItemSlot components of both game objects
        ItemSlot itemSlotBeforeDrag = dparentBeforeDrag.GetComponent<ItemSlot>();
        ItemSlot itemSlotAfterDrag = dparentAfterDrag.GetComponent<ItemSlot>();




        // Swap item name
        string tempItemName = itemSlotBeforeDrag.itemName;
        itemSlotBeforeDrag.itemName = itemSlotAfterDrag.itemName;
        itemSlotAfterDrag.itemName = tempItemName;

        // Swap quantity
        int tempQuantity = itemSlotBeforeDrag.quantity;
        itemSlotBeforeDrag.quantity = itemSlotAfterDrag.quantity;
        itemSlotAfterDrag.quantity = tempQuantity;

        //quantity

        Transform quantityTextAfterDrag = dparentAfterDrag.Find("QuantityText");

        TMP_Text quantityText = quantityTextAfterDrag.GetComponent<TMP_Text>();

        quantityText.text = itemSlotAfterDrag.quantity.ToString();
        quantityText.enabled = true;


        Transform quantityTextBeforeDrag = dparentBeforeDrag.transform.Find("QuantityText");

        TMP_Text quantityTextSec = quantityTextBeforeDrag.GetComponent<TMP_Text>();

        quantityTextSec.text = itemSlotBeforeDrag.quantity.ToString();
        quantityTextSec.enabled = false;


        // Swap item sprite
        Sprite tempItemSprite = itemSlotBeforeDrag.itemSprite;
         itemSlotBeforeDrag.itemSprite = itemSlotAfterDrag.itemSprite;
         itemSlotAfterDrag.itemSprite = tempItemSprite;

        // Assuming the sprite is represented by a SpriteRenderer component




        // Swap isFull
        bool tempIsFull = itemSlotBeforeDrag.isFull;
        itemSlotBeforeDrag.isFull = itemSlotAfterDrag.isFull;
        itemSlotAfterDrag.isFull = tempIsFull;


        // Swap item description
        string tempItemDescription = itemSlotBeforeDrag.itemDescription;
        itemSlotBeforeDrag.itemDescription = itemSlotAfterDrag.itemDescription;
        itemSlotAfterDrag.itemDescription = tempItemDescription;



        // Swap item type
        ItemType tempItemType = itemSlotBeforeDrag.itemType;
        itemSlotBeforeDrag.itemType = itemSlotAfterDrag.itemType;
        itemSlotAfterDrag.itemType = tempItemType;



 


    }


}