using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPortalScript : MonoBehaviour
{

    [SerializeField] private float activationTime;
    [SerializeField] private GameObject GetReadyTextPrefab;
    
    [SerializeField] private AudioSource portalAppearSFX;
    [SerializeField] private AudioSource portalEnterSFX;

    IEnumerator StartNewLevel() {
        Instantiate(GetReadyTextPrefab);
        yield return new WaitForSeconds(activationTime);
        EnemyScript.destroyedEnemies = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            portalAppearSFX.Stop();
            portalEnterSFX.Play();
            StartCoroutine(StartNewLevel());
        }
    }
}
