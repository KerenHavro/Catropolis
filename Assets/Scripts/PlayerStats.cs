using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int attack, defense, agility, intelligance;
    [SerializeField]
    private TMP_Text attackText, defenseText, agilityText, intelliganceText;
    void Start()
    {
        UpdateEquippmentStats();  
    }

    public void UpdateEquippmentStats()
    {
        attackText.text = attack.ToString();
        defenseText.text = defense.ToString();
        agilityText.text = agility.ToString();
        intelliganceText.text = intelligance.ToString();
    }
}
