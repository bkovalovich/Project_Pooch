using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreenScript : MonoBehaviour
{

    public static bool isPaused = false;
    public GameObject pauseUI;

    public void pausePressed() {
        isPaused = true;
    }

    public void Resume() {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause() {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPaused) {
            Pause();
        } else {
            Resume();
        }
    }
}
