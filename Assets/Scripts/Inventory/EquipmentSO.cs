using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int attack, defense, agility, choppingStrenght, miningStrength;
    [SerializeField]
    private Sprite itemSprite;
    [SerializeField]
    private Sprite EmptySprite;


    public void PreviewEquipment()
    {
        GameObject.Find("StatManager").GetComponent<PlayerStats>().
            PreviewEquipmentStats(attack, defense, agility, choppingStrenght, miningStrength, itemSprite);
    }

    public void EquipItem()
    {
        //update Stats
        PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerstats.attack += attack;
        playerstats.defense += defense;
        playerstats.agility += agility;
        playerstats.choppingStrenght += choppingStrenght;
        playerstats.miningStrength += miningStrength;

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
            playerstats.choppingStrenght -= choppingStrenght;
            playerstats.miningStrength -= miningStrength;

        }

    }

}
