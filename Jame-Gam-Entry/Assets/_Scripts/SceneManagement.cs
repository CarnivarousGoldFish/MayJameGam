using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    public static bool isPaused;
    [SerializeField] GameObject PauseCanvas;
    [SerializeField] GameObject GameCanvas;

    public void StartGame(){
        isPaused = false;
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        SceneManager.LoadScene("SFXTest");
    }
    
    public void QuitGame(){
        Application.Quit();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            TogglePause();
        }
    }
    public void TogglePause(){
        Debug.Log("pausing...");
        Time.timeScale = PauseCanvas.activeSelf ? 1 : 0;
        isPaused = PauseCanvas.activeSelf ? false : true;
        GameCanvas.SetActive(PauseCanvas.activeSelf);
        PauseCanvas.SetActive(!PauseCanvas.activeSelf);
    }
}
