using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private ShopItemScriptObj shopItemScriptObj;
    [SerializeField] private int isOwned;
    [SerializeField] private Image lockImage;

    private void Start() {
        isOwned = PlayerPrefs.GetInt("isOwned" + shopItemScriptObj.itemID.ToString());
    }

    private void Update() {
        CheckIfOwned();
    }

    public void SetScriptObj(ShopItemScriptObj scriptObj) {
        shopItemScriptObj = scriptObj;
    }

    private void CheckIfOwned() {
        if (shopItemScriptObj.itemIsOwned) {
            isOwned = 1; // Sets to Owned}
            PlayerPrefs.SetInt("isOwned" + shopItemScriptObj.itemID.ToString(), isOwned);
        }

        if (isOwned == 0) {
            //niet
        }

        if (isOwned == 1) {
            lockImage.color = new Color(255, 255, 255, 0);
        }
    }
}
