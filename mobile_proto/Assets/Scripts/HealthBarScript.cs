using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{ 
    [SerializeField] public Image meter; //meter image
//    [SerializeField] public GameObject enemyObject;
    private EnemyScript enemyScript;

    void Awake() {
        enemyScript = GetComponentInParent<EnemyScript>();
    }

    void LateUpdate() {
        transform.position = new Vector3(enemyScript.EnemyPosition.x, enemyScript.EnemyPosition.y + 1.3f);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.parent.rotation.z * 0.1f);
        meter.fillAmount = enemyScript.HealthRatio;
    }
}
