using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
            Debug.Log("You win");
        }
    }
}