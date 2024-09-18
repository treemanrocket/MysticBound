using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    void PickupArmor(GameObject armorGameObject, Sprite armorIcon, ArmorScripts armorBehavior);
    void PickupWeapon(GameObject weaponGameObject, Sprite weaponIcon, WeaponScript armorBehavior);
    //void PickupItem(); no scriptable object for the items yet
}
