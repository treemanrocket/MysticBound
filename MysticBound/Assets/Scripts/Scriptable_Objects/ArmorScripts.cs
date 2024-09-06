using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


[CreateAssetMenu(menuName = "Gear System/Armors", order = 12)]
public class ArmorScripts : ScriptableObject
{
    public enum ArmorType
    {
        HEAD,
       CHEST,
        LEGS,
        FEET
    }

    [Header("Armor Type")]
    public ArmorType armorType;

    [Header("Stats")]
    public int armorValue;

    [Header("UI")]
    public Sprite icon;
}
