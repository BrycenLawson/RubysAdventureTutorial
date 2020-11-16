using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmptyController : MonoBehaviour
{
    public Text winText;
    public Text countText;
    public static int count;
    
    void Start()
    {
        count = 0;
        SetCountText ();
        winText.text = "";

    }

    void Update()
    {
        count = EnemyController.count + HardEnemyController.count; 
        SetCountText();
    }

    void SetCountText ()
    {
        countText.text = "Robots Fixed: " + count.ToString ();
        if (count >= 4)
        {
            winText.text = "Talk to Jambi to visit stage two!";

             if (SceneManager.GetActiveScene().name == "SecondScene")
            {
                winText.text = "WINNER! Game created by Brycen Lawson. Press R to restart.";
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                if (SceneManager.GetActiveScene().name == "MainScene")
                {
                    SceneManager.LoadScene("MainScene");
                    count = EnemyController.count * 0;
                }

                if (SceneManager.GetActiveScene().name == "SecondScene")
                {
                    SceneManager.LoadScene("SecondScene");
                    count = EnemyController.count * 0;
                }
            }
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }
    } 
}