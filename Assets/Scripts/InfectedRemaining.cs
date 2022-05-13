using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InfectedRemaining : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {   
        text.text = "Enemies Remaining: 10";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Enemies Remaining: " + GlobalVars.enemiesRemaining.ToString();

        if(GlobalVars.enemiesRemaining == 0){
            Debug.Log("You win");
        }
    }
}