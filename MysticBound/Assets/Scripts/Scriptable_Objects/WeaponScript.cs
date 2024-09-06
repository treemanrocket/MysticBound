using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


[CreateAssetMenu(fileName = "Gear", menuName = "Gear System/Weapons", order = 11)]

public class WeaponScript : ScriptableObject
{
    public enum WeaponType
    {
        LONGSWORD,
        AXE,
        GAUNTLETS,
        NUNCHAKU
    }

    public enum WeaponElementType
    {
        NONE,
        WATER,
        FIRE,
        EARTH,
        LIGHTNING        
    }

    
    [Header("Weapon Type/ Element")]
    [Tooltip("This is the weapon type, just select the drop down menu for what weapon type it wil be")]
    public WeaponType weaponType;

    [Tooltip("This is the weapon elements if they have any")]
    public WeaponElementType weaponElementType;

    [Header("Stats")]
    public int damage;
    public int attackSpeed;


    [Header("UI")]
    public Sprite icon;
}
