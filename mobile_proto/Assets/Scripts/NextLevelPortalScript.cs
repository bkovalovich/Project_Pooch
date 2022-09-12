using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum PortalState {
    Opening, 
    Idling, 
    Closing
}
public class NextLevelPortalScript : MonoBehaviour
{
    [SerializeField] private float activationTime;
    [SerializeField] private GameObject GetReadyTextPrefab;
    
    [SerializeField] private AudioSource portalAppearSFX;
    [SerializeField] private AudioSource portalEnterSFX;

    PortalState currentPortalState = PortalState.Opening;
    [SerializeField] private CircleCollider2D circleCollider;

    private float shrinkScale = 0.01f;
    private float growScale = 0.009f;
    private float growTimer = 0;
    private float timeToGrow = 2;

    private float rotateSpeed = 0.2f;

    private void Awake() {
        //circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
    }

    //IEnumerator OnSpawn(float timeBeforeActive) {
    //    yield return new WaitForSeconds(timeBeforeActive);
    //    circleCollider.enabled = true;
    //}

    IEnumerator StartNewLevel() {
        Instantiate(GetReadyTextPrefab);
        yield return new WaitForSeconds(activationTime);
        EnemyScript.destroyedEnemies = 0;
        PlayerMovement.startMovingTowardsPortal = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void FixedUpdate() {
        transform.Rotate(Vector3.forward * rotateSpeed);
        switch (currentPortalState) {
            case (PortalState.Opening):
                if (growTimer < timeToGrow) {
                    growTimer += Time.deltaTime;
                    transform.localScale += new Vector3(growScale, growScale, growScale);
                } else {
                    circleCollider.enabled = true;
                    currentPortalState = PortalState.Idling;
                }
                break;
            case (PortalState.Idling):
                break;
            case (PortalState.Closing):
                transform.localScale -= new Vector3(shrinkScale, shrinkScale, shrinkScale);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerMovement.startMovingTowardsPortal = true;
            currentPortalState = PortalState.Closing;
            portalAppearSFX.Stop();
            portalEnterSFX.Play();
            StartCoroutine(StartNewLevel());
        }
    }
}
