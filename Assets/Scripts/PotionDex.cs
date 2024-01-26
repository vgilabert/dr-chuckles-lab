using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/**
 * @class PotionDex
 * @brief A class that stores the potions that the player has unlocked
 */
[CreateAssetMenu(fileName = "PotionDex", menuName = "PotionDex", order = 1)]
public class PotionDex : ScriptableObject
{
    /**
     * @struct PotionData
     * @brief A struct that stores the potion and whether or not it is unlocked
     */
    private struct PotionData
    {
        public PotionObject potion;
        public void Unlock() { unlocked = true; }
        public void Lock() { unlocked = false; }

        public bool unlocked;
    }

    private List<PotionData> potions;

    public PotionDex()
    {
        PotionObject[] tmp_potions = GameManager.Instance.potions.ToArray();
        int count = tmp_potions.Count();

        for(int i = 0; i < count; i++)
        {
            RegisterPotion(tmp_potions[i]);
        }
    }

    private void RegisterPotion(PotionObject potionObj)
    {
        PotionData data = new()
        {
            potion = potionObj,
            unlocked = false
        };
        potions.Add(data);
    }

    /**
     * @brief Checks if the potion is unlocked
     * @param potion The potion to check
     * @return Whether or not the potion is unlocked
     */
    public bool Unlocked(PotionObject potion)
    {
        for(int i = 0; i < potions.Count; i++)
        {
            if (potions[i].potion.potionName == potion.potionName)
            {
                return potions[i].unlocked;
            }
        }
        return false;
    }

    /**
     * @brief Unlocks the potion
     * @param potion The potion to unlock
     */
    public void UnlockPotion(PotionObject potion)
    {
        for(int i = 0; i < potions.Count; i++)
        {
            if (potions[i].potion.potionName == potion.potionName)
            {
                potions[i].Unlock();
                break;
            }
        }
    }
}   