using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [Header("List of Items selling")]
    public ShopItem[] shopItem;

    [Header("References")]
    public Transform shopContainer;
    public GameObject shopItemPrefab;
    public Text balancePerm;

    void Start(){
        FillShop();
        Player.balancePerm = PlayerPrefs.GetInt("balancePerm");
        balancePerm.text = Player.balancePerm.ToString();
    }
    
    void Update(){
    
        
    }

    private void FillShop() {
        for (int i = 0; i < shopItem.Length; i++) {
            ShopItem si = shopItem[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(si));

            itemObject.GetComponent<Image>().color = si.itemColor;
            itemObject.transform.GetChild(1).GetComponent<Text>().text = si.itemPrice.ToString();

            //itemObject.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }

    private void OnButtonClick(ShopItem item) {
        Debug.Log(item.itemID);
    }
}
