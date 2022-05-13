using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommonMenuFunctions : MonoBehaviour
{
    public void BeginGame(){
        SceneManager.LoadScene("MainScene");
    }

    public void ExitProgram(){
        SceneManager.LoadScene("Menu");
    }
}
