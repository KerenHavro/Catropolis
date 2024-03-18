using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttribute;


    public bool UseItem()
    {
        if(statToChange== StatToChange.health)
        {
            Player playerHealth = GameObject.Find("player").GetComponent<Player>();
            if (playerHealth.CurrentHealth == playerHealth.MaxHealth)
            {
                return false;
            }
            else {
                playerHealth.Heal(amountToChangeStat);
                return true;
            }
        }
       
        if (statToChange == StatToChange.hunger)
        {
            Player playerHunger = GameObject.Find("player").GetComponent<Player>();
            if (playerHunger.CurrentHunger == playerHunger.MaxHunger)
            {
                return false;
            }
            else
            {
                playerHunger.Feed(amountToChangeStat);
                return true;
            }
        }
        return false;
    }



    public enum StatToChange
    {
        none,
        health,
        hunger
    }
    public enum AttributesToChange
    {
        none,
        defense
    }
}
