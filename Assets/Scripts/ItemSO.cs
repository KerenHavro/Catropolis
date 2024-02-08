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
    //Objects
        public Sprite sprite;
        public int health;
        

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
        return false;
    }



    public enum StatToChange
    {
        none,
        health
    }
    public enum AttributesToChange
    {
        none,
        defense
    }

}
