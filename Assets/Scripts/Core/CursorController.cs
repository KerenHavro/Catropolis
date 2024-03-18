using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public Sprite cursorSprite; // Sprite for the cursor
    public Sprite clickedCursorSprite; // Sprite for the clicked cursor
    public GameObject cursorObject; // Object to represent cursor

    private SpriteRenderer cursorRenderer;

    private void Start()
    {
        Cursor.visible = false; // Hide default cursor
        cursorRenderer = cursorObject.GetComponent<SpriteRenderer>();
        cursorRenderer.sprite = cursorSprite; // Set default sprite
    }

    private void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get cursor position
        cursorObject.transform.position = cursorPos; // Set object's position to cursor position

        if (Input.GetMouseButtonDown(0)) // If left mouse button is clicked
        {
            cursorRenderer.sprite = clickedCursorSprite; // Change cursor sprite
        }
    }

    private void OnMouseEnter()
    {
        cursorRenderer.sprite = cursorSprite; // Set default cursor sprite
    }

    private void OnMouseExit()
    {
        cursorRenderer.sprite = cursorSprite; // Reset cursor to default
    }
}