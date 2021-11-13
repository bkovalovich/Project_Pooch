using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Quaternion rotation;
    public GameObject enemy;
    [SerializeField] public Image meter; //meter image
    public EnemyScript enemyScript;


    void Awake() {
        rotation = transform.rotation;
        enemyScript = GetComponentInParent<EnemyScript>();
    }

    void LateUpdate() {
        transform.position = new Vector3(enemyScript.EnemyPosition.x, enemyScript.EnemyPosition.y + 1.3f);
        transform.rotation = rotation;
        meter.fillAmount = enemyScript.HealthRatio;
    }
}
