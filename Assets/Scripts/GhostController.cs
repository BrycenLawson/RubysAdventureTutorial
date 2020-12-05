using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    public static int count;
    public AudioClip hurtSound;
    public GameObject hurtParticlePrefab;
    public float speed;
    public float startingDistance;
    private Transform target;

    Rigidbody2D rigidbody2D;

    AudioSource audioSource;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SecondScene")
        {
        count = 0;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.3f;
        }

        else
        {
        count = 4; 
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void Update()
    {   
        if (RubyController.currentAbility >= 1)
        {
            if (Vector2.Distance(transform.position, target.position) < startingDistance)
            {
                PlaySound(hurtSound);
                rigidbody2D.simulated = true;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                if (RubyController.currentAbility >= 1)
                {
                    speed = 2.0f;
                }
            }
        }
        if (RubyController.currentAbility < 1)
        {
            rigidbody2D.simulated = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            if (RubyController.currentAbility >= 1)
            {
                player.ChangeHealth(-3);
            }
            if (RubyController.currentAbility < 1)
            {
                player.ChangeHealth(0);
            }
        }
    }

    public void Death()
    {
        Destroy(gameObject, 0.2f);
        GameObject hurtParticleObject = Instantiate(hurtParticlePrefab, transform.position, Quaternion.identity);
        hurtParticleObject.GetComponent<ParticleSystem>().Play();
        count = count + 1; 
    }
}
