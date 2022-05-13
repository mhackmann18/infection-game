using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonMenuFunctions : MonoBehaviour
{
    public void BeginGame(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GlobalVars.enemiesRemaining = 0;
        SceneManager.LoadScene("MainScene");
    }

    public void ExitProgram(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }
}
