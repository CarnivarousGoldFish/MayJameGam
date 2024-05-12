using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject PauseCanvas;
    public void StartGame(){
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
        SceneManager.LoadScene("GameScene");
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
        PauseCanvas.SetActive(!PauseCanvas.activeSelf);
    }
}
