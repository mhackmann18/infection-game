using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfectedRemaining : MonoBehaviour
{
    public Text text;

    void Start()
    {   
        text.text = "Enemies Remaining: " + GlobalVars.enemiesRemaining.ToString();
    }

    void Update()
    {
        text.text = "Enemies Remaining: " + GlobalVars.enemiesRemaining.ToString();

        // Player is taken to victory screen when all zombies are defeated
        if(GlobalVars.enemiesRemaining == 0){
            GlobalVars.enemiesRemaining = 0;
            SceneManager.LoadScene("WinScreen");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}