﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController >();

        if (controller != null)
        {
            if (RubyController.currentAbility < 1)
            {
            controller.ChangeHealth(-5);
            Destroy(GameObject.FindWithTag("Player"));
            }
            if (RubyController.currentAbility >= 1)
            {
                
            }
        }
    }
}