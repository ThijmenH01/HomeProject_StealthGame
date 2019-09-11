using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorCarrier : MonoBehaviour
{
    public static PlayerColorCarrier instance;

    public Color playerColor;

    void Start(){
        instance = this;
        DontDestroyOnLoad(this);
    }
}
