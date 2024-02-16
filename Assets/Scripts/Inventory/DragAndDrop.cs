using System.Collections;
using System.Collections.Generic;
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
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.alpha = .6f;
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
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

        // Swap item sprite
        Sprite tempItemSprite = itemSlotBeforeDrag.itemSprite;
        itemSlotBeforeDrag.itemSprite = itemSlotAfterDrag.itemSprite;
        itemSlotAfterDrag.itemSprite = tempItemSprite;

        // Swap isFull
        bool tempIsFull = itemSlotBeforeDrag.isFull;
        itemSlotBeforeDrag.isFull = itemSlotAfterDrag.isFull;
        itemSlotAfterDrag.isFull = tempIsFull;

        // Swap item description
        string tempItemDescription = itemSlotBeforeDrag.itemDescription;
        itemSlotBeforeDrag.itemDescription = itemSlotAfterDrag.itemDescription;
        itemSlotAfterDrag.itemDescription = tempItemDescription;

        // Swap empty sprite
        Sprite tempEmptySprite = itemSlotBeforeDrag.emptySprite;
        itemSlotBeforeDrag.emptySprite = itemSlotAfterDrag.emptySprite;
        itemSlotAfterDrag.emptySprite = tempEmptySprite;

        // Swap item type
        ItemType tempItemType = itemSlotBeforeDrag.itemType;
        itemSlotBeforeDrag.itemType = itemSlotAfterDrag.itemType;
        itemSlotAfterDrag.itemType = tempItemType;

        itemSlotBeforeDrag.UpdateUI();
        itemSlotAfterDrag.UpdateUI();

    }
}