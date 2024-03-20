using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int attack, defense, agility, choppingStrenght, miningStrength;
    [SerializeField]
    private TMP_Text attackText, defenseText, agilityText, choppingStrenghtText, miningStrengthText;

    [SerializeField]
    private TMP_Text attackPreText, defensePreText, agilityPreText, choppingStrenghtPreText, miningStrengthPreText;
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
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            
            attackText.text = attack.ToString();
            defenseText.text = defense.ToString();
            agilityText.text = agility.ToString();
            choppingStrenghtText.text = choppingStrenght.ToString();
            miningStrengthText.text = miningStrength.ToString();
            playerObject.GetComponent<Player>().currentChoppingPower = choppingStrenght;
            playerObject.GetComponent<Player>().currentMiningPower = miningStrength;
            playerObject.GetComponent<Player>().playerDmg = attack;
        }
        else
        {
            attackText.text = 0.ToString();
            defenseText.text = 0.ToString();
            agilityText.text = 0.ToString();
            choppingStrenghtText.text = 0.ToString();
            miningStrengthText.text = 0.ToString();
            
        }
    }

    public void PreviewEquipmentStats(int attack, int defense, int agility, int choppingStrenght,int miningStrength, Sprite itemSprite)
    {
        
        attackPreText.text = attack.ToString();
        defensePreText.text = defense.ToString();
        agilityPreText.text = agility.ToString();
        choppingStrenghtPreText.text = choppingStrenght.ToString();
        miningStrengthPreText.text = miningStrength.ToString();

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
