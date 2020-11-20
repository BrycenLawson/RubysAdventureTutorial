using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThirdEmptyController : MonoBehaviour
{
    public Text countText;
    public static int projectile;
    
    void Start()
    {
        projectile = RubyController.currentCogs;
    }

    void Update()
    {
        projectile = RubyController.currentCogs;
        SetCountText();
    }

    void SetCountText ()
    {
        countText.text = "Cog Bullets: " + projectile.ToString ();
    } 
}