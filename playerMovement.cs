using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    [Header("Player")]
    public playerControl controller;
    public Animator animator;
    [SerializeField] private float HorizontalMove =0f;
    [SerializeField] private float runspeed = 40f;
    [SerializeField] private bool jump =false;
    [SerializeField] private bool crouch = false;

    [Header("Enemies")]
    public Animator FAR_Animator;
    public AudioSource FAR_death;
    public Animator Frog_Animator;

    [Header("REWARDS")]
    [SerializeField] private int NumberOFGemsToWin;


    [Header("UI")]
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private AudioSource GameOverSound;
    [SerializeField] private AudioSource MainMusic;
    [SerializeField] private AudioSource WinningSound;
   

    private void Update()
    {
        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;
    }
    
    public void oncrouching (bool crouch)
    {
        animator.SetBool("crouch", crouch);
    }
    private void FixedUpdate()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
        if (Input.GetButton("Jump"))
            jump = true;

       
        animator.SetFloat("speed", Mathf.Abs(HorizontalMove));
        animator.SetBool("jump", jump);

        animator.SetBool("ground", controller.m_Grounded);
        controller.Move(HorizontalMove *Time.fixedDeltaTime, crouch, jump); //time.fixeddeltatime to be the same in all platform
        jump = false;
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "FAR_death" || other.tag == "frog_death") // for enmey kill 
        {  
            other.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
            other.gameObject.transform.parent.GetChild(0).gameObject.SetActive(false);
            FAR_death.Play();
            if(other.tag == "FAR_death")
            {
            FAR_Animator.SetBool("Death", true);
            }
            else if (other.tag == "frog_death")
            {
                frog_move.die = true;
               // Frog_Animator.SetBool("death", true);
            }
            StartCoroutine(DO(other.gameObject));
            

        }
        else if (other.tag == "FAR_Attack" || other.tag == "frog_attack") // for enmey attack
        {
            other.gameObject.transform.parent.GetChild(0).gameObject.SetActive(false);
            animator.SetBool("hurting", true);
           
            StartCoroutine(death());

        }
        else if(other.tag == "DeathLine" || other.tag == "wood" || other.tag == "eagle_attack") //for death line
        {
            animator.SetBool("hurting", true);
            
            StartCoroutine(death());

        }
        else if (other.tag == "box" ) //box
        {
            other.gameObject.SetActive(false);

            
        }
        else if (other.tag == "door" && NumberOFGemsToWin == GEMS_collect.GEMScount) // winning
        {
            other.gameObject.SetActive(false);
            MainMusic.Pause();
            WinningSound.Play();
            this.GetComponent<Renderer>().enabled = false;
            StartCoroutine(Levelcomplete());

        }



    }

    // delays
    IEnumerator DO(GameObject enmey)
    {
        yield return new WaitForSeconds(0.35f);
        enmey.transform.parent.gameObject.SetActive(false);
        frog_move.die = false;

    }
    IEnumerator death ()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("hurting", false);
        this.gameObject.SetActive(false);
        GameOverScreen.SetActive(true);
        MainMusic.Pause();
        GameOverSound.Play();
    }
    IEnumerator Levelcomplete()
    {
        yield return new WaitForSeconds(3);
        
        globalLevel.Levelnumber += 1;
        if (globalLevel.Levelnumber > 4)
        {
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("levelLoadNum", 2);
            GEMS_collect.GEMScount = 0;
        }
        else
        {
            SceneManager.LoadScene(globalLevel.Levelnumber);
            PlayerPrefs.SetInt("levelLoadNum", globalLevel.Levelnumber);
            GEMS_collect.GEMScount = 0;

        }
    }

}
