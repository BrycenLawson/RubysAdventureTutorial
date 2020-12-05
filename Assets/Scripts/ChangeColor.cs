using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeColor : MonoBehaviour 
{
    private Renderer rend;

    private Renderer render;

    [SerializeField]
    private Color colorToTurnTo = Color.white;

    [SerializeField]
    private Color colorTurn = Color.white;

    private void Start()    
    {
        rend = GetComponent<Renderer>();
        render = GetComponent<Renderer>();
    }
    
    void Update()
    {   
        if (RubyController.currentAbility >= 1)
        {
            rend.material.color = colorToTurnTo;
        }
        if (RubyController.currentAbility < 1)
        {
            rend.material.color = colorTurn;
        }
    }
}