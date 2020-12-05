using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealScript : MonoBehaviour
{
    public AudioClip collectedClip;

    public GameObject pickupParticlePrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeAbility(1);
            GameObject pickupParticleObject = Instantiate(pickupParticlePrefab, transform.position, Quaternion.identity);
            pickupParticleObject.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
        }  
    } 
}