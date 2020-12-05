using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FourthEmptyController : MonoBehaviour
{
    public Text winText;
    public Text countText;
    public static int total;
    
    void Start()
    {
        total = 0;
        SetCountText ();
        winText.text = "";

    }

    void Update()
    {
        total = GhostController.count; 
        SetCountText();
    }

    void SetCountText ()
    {
        countText.text = "Ghosts Defeated: " + total.ToString ();
        if (total >= 4)
        {
            if (EnemyController.count + HardEnemyController.count >= 4)
            {

                if (SceneManager.GetActiveScene().name == "SecondScene")
                {
                    winText.text = "WINNER! Game created by Brycen Lawson. Press R to restart.";
                }

                if(Input.GetKeyDown(KeyCode.R))
                {
                    if (SceneManager.GetActiveScene().name == "SecondScene")
                    {
                        SceneManager.LoadScene("SecondScene");
                    }

                    else
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                }
                if (Input.GetKey("escape"))
                {
                    Application.Quit();
                }
            }
        }
    } 
}