using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float floatingHeight = 2f;
    private float floatingSpeed = 3f;

    private void Update() {
        transform.Rotate(0, 0, 1);
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * floatingSpeed) / floatingHeight + 1f, transform.position.z);
    }

    private void OnTriggerEnter(Collider other) {
        Player player = other.gameObject.GetComponent<Player>();

        if (other.gameObject.GetComponent<Player>() != null) {
            Player.balanceTemp++;
            PlayerPrefs.SetInt("balanceTemp", Player.balanceTemp);
            GameManager.instance.balanceTempText.text = Player.balanceTemp.ToString();
            Destroy(gameObject);
        }
    }

}
