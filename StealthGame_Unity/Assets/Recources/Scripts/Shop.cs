using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("List of Items selling")]
    public ShopItemScriptObj[] shopItem;

    [Header("References")]
    public Transform shopContainer;
    public GameObject shopItemPrefab;
    public Text balancePermText;
    private GameObject itemObject;

    void Start(){
        FillShop();
        Player.balancePerm = PlayerPrefs.GetInt("balancePerm");
        balancePermText.text = Player.balancePerm.ToString();
    }

    private void FillShop() {
        for (int i = 0; i < shopItem.Length; i++) {
            ShopItemScriptObj si = shopItem[i];
            itemObject = Instantiate(shopItemPrefab, shopContainer);

            itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(si));
            itemObject.GetComponent<Image>().color = si.itemColor;
            itemObject.transform.GetChild(1).GetComponent<Text>().text = "$ " + si.itemPrice.ToString();
            itemObject.GetComponent<ShopItem>().SetScriptObj(si);
        }
    }

    private void OnButtonClick(ShopItemScriptObj item) {
        if (Player.balancePerm >= item.itemPrice && Player.balancePerm >= 0 && !item.itemIsOwned){
            Player.balancePerm -= item.itemPrice;
            PlayerPrefs.SetInt("balancePerm", Player.balancePerm);
            balancePermText.text = Player.balancePerm.ToString();
            PlayerColorCarrier.instance.playerColor = item.itemColor;
            item.itemIsOwned = true;
        }
        else {
            PlayerColorCarrier.instance.playerColor = item.itemColor;
            print("Already Bought or not enough balance");
        }
    }
}
