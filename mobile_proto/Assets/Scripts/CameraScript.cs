using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    private Camera mainCamera;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerInfo = player.transform.transform.position;
        mainCamera.transform.position = new Vector3(playerInfo.x, playerInfo.y, playerInfo.x/* - cameraDistOffset*/);
    }
}
