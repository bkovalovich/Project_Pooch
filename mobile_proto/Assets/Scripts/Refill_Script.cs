using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Refill_Script : MonoBehaviour
{
    [SerializeField] public Image meter; //meter image
    public GameObject player; //Main player object

    //Start()
    //Gets player
    void Start(){
        player = GameObject.Find("Player");
    }

    //FixedUpdate()
    //Fills the button depending on how much recharge time is left
    void FixedUpdate() {
        meter.fillAmount = player.GetComponent<SecondaryWeapon>().IsCoolingDown ? player.GetComponent<SecondaryWeapon>().RechargeRatio : 0;
    }
}
