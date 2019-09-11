using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItemScriptObj : ScriptableObject
{
    public int itemPrice;
    public bool itemIsOwned;
    public Color itemColor;
    public int itemID;
}
