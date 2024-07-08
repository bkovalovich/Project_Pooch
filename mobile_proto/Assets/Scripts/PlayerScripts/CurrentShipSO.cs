using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentShip")]
public class CurrentShipSO : ScriptableObject {
    [SerializeField] PlayerInfoSO selectedShip;
    public PlayerInfoSO SelectedShip {
        get { return selectedShip; }
        set { selectedShip = value; }
    }

}
