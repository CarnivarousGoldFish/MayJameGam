using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public GameObject PauseCanvas;
    public void StartGame(){
        SceneManager.LoadScene("Game");
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
        Debug.Log("escape was pressed");
        //Time.timeScale = 0;
        PauseCanvas.SetActive(!PauseCanvas.activeSelf);
    }
}
