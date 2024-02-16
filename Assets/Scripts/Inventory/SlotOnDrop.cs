using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotOnDrop : MonoBehaviour, IDropHandler
{
    private ItemSlot itemSlot;

    public void Start()
    {
        itemSlot = GameObject.Find("ItemSlot").GetComponent<ItemSlot>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragAndDrop dragAndDrop = dropped.GetComponent<DragAndDrop>();
            dragAndDrop.parentAfterDrag = transform;
            
        }
        if (transform.childCount != 0)
        {
            // If there is a child, check if its image is "empty"
            Transform child = transform.GetChild(0);
            Image childImage = child.GetComponent<Image>();
            if (childImage.sprite.name == "empty")
            {
                // Destroy the child
                Destroy(child.gameObject);

                GameObject dropped = eventData.pointerDrag;
                DragAndDrop dragAndDrop = dropped.GetComponent<DragAndDrop>();
                dragAndDrop.parentAfterDrag = transform;
                
            }
        }
    }

}
