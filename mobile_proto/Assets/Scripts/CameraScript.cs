using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows the camera to follow the player no matter how it is transformed by getting its position and setting cameras position to it
public class CameraScript : MonoBehaviour {
    private Camera mainCamera;
    private GameObject player;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        player = GameObject.Find("P_Player");
    }

    void LateUpdate()
    {
        try {
            Vector3 playerInfo = player.transform.position;
            mainCamera.transform.position = new Vector3(playerInfo.x, playerInfo.y, playerInfo.x/* - cameraDistOffset*/);
        } catch (MissingReferenceException) {
        }
    }
}
