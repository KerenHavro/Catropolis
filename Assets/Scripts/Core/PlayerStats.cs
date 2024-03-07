using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int attack, defense, agility, intelligance;
    [SerializeField]
    private TMP_Text attackText, defenseText, agilityText, intelliganceText;

    [SerializeField]
    private TMP_Text attackPreText, defensePreText, agilityPreText, intelligancePreText;
    [SerializeField]
    private Image previeweImage;

    [SerializeField]
    private GameObject selectedItemStats;

    [SerializeField]
    private GameObject selectedItemImage;
    [SerializeField]
    private Sprite EmptySprite;
    void Start()
    {
        UpdateEquippmentStats();
    }

    public void UpdateEquippmentStats()
    {
        if (this.selectedItemImage != EmptySprite)
        {
            attackText.text = attack.ToString();
            defenseText.text = defense.ToString();
            agilityText.text = agility.ToString();
            intelliganceText.text = intelligance.ToString();
        }
        else
        {
            attackText.text = 0.ToString();
            defenseText.text = 0.ToString();
            agilityText.text = 0.ToString();
            intelliganceText.text = 0.ToString();
        }
    }

    public void PreviewEquipmentStats(int attack, int defense, int agility, int intelligance, Sprite itemSprite)
    {
        
        attackPreText.text = attack.ToString();
        defensePreText.text = defense.ToString();
        agilityPreText.text = agility.ToString();
        intelligancePreText.text = intelligance.ToString();

        previeweImage.sprite = itemSprite;
        selectedItemImage.SetActive(true);
        selectedItemStats.SetActive(true);
    }

    public void TurnOffPreviewStats()
    {
        
        UpdateEquippmentStats();
        selectedItemImage.SetActive(false);
        selectedItemStats.SetActive(false);
    }
}
