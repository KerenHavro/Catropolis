using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int attack, defense, agility, intelligence;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private Sprite EmptySprite;


    public void PreviewEquipment()
    {
        GameObject.Find("StatManager").GetComponent<PlayerStats>().
            PreviewEquipmentStats(attack, defense, agility, intelligence, itemSprite);
    }

    public void EquipItem()
    {
        //update Stats
        PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerstats.attack += attack;
        playerstats.defense += defense;
        playerstats.agility += agility;
        playerstats.intelligance += intelligence;

        playerstats.UpdateEquippmentStats();
    }

    public void UnEquipItem()
    {
        if (itemSprite)
        {


            PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
            playerstats.attack -= attack;
            playerstats.defense -= defense;
            playerstats.agility -= agility;
            playerstats.intelligance -= intelligence;

        }

    }

}
