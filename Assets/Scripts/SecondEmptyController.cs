using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SecondEmptyController : MonoBehaviour
{
    public Text winText;
    public Text countText;
    public static int hp;
    
    void Start()
    {
        hp = 5;
        winText.text = "";
    }

    void Update()
    {
        hp = RubyController.currentHealth;
        SetCountText();
    }

    void SetCountText ()
    {
        countText.text = hp.ToString ();
        if (hp == 0)
        {
            Destroy(GameObject.FindWithTag("Player"));
            winText.text = "You lost! Press R to restart.";
            
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