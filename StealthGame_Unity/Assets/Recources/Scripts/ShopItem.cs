using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public int itemPrice;
    public bool itemIsOwned;
    public Color itemColor;
    public int itemID;
}
