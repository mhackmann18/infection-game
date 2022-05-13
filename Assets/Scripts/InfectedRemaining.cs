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
    }
}