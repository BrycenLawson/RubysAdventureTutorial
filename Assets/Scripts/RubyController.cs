using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public AudioClip soulSound;

    public AudioClip throwSound;

    public AudioClip throw2Sound;

    public AudioClip hurtSound;

    public GameObject hurtParticlePrefab;

    public float speed = 3.0f;
    
    public int maxHealth = 5;
    
    public GameObject projectilePrefab;

    public GameObject projectile2Prefab;
    
    public int health { get { return currentHealth; }}
    public static int currentHealth;

    public int cogs { get { return currentCogs; }}
    public static int currentCogs;

    public int ability { get { return currentAbility; }}
    public static int currentAbility;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentCogs = 4;
        currentAbility = 0;

        audioSource= GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if (currentCogs > 1)
            {
                if (currentAbility > 0)
                {
                    Launch2();
                    ChangeCogs(-2);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (currentCogs > 0)
            {
                Launch();
                ChangeCogs(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (character != null)
                    {
                        character.DisplayDialog();
                        {
                            if (SceneManager.GetActiveScene().name == "SecondScene")
                            {
                                PlaySound(soulSound);
                            }
                            else
                            {
                                if (EnemyController.count + HardEnemyController.count >= 4)
                                {
                                    SceneManager.LoadScene("SecondScene");
                                }
                            }  
                        }
                    }  
                }
            }
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            GameObject hurtParticleObject = Instantiate(hurtParticlePrefab, transform.position, Quaternion.identity);
            hurtParticleObject.GetComponent<ParticleSystem>().Play();
            PlaySound(hurtSound);

            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void ChangeAbility(int amount)
    {
        speed = 5.0f;
        currentAbility = Mathf.Clamp(currentAbility + amount, 0, int.MaxValue);
        Debug.Log(currentAbility + "/" + int.MaxValue);
    }

    public void ChangeCogs(int amount)
    {
        currentCogs = Mathf.Clamp(currentCogs + amount, 0, int.MaxValue);
        Debug.Log(currentCogs + "/" + int.MaxValue);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwSound);
    }

    void Launch2()
    {
        GameObject projectileObject = Instantiate(projectile2Prefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile2 projectile = projectileObject.GetComponent<Projectile2>();
        projectile.Launch(lookDirection, 200);

        animator.SetTrigger("Launch");

        PlaySound(throw2Sound);
    }
}