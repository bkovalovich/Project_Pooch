using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortalScript : MonoBehaviour
{

    public float activationTime;

    IEnumerator StartNewLevel() {
        yield return new WaitForSeconds(activationTime);
        EnemyScript.destroyedEnemies = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine(StartNewLevel());
        }
    }
}
