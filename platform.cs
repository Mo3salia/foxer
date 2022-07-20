using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField] private AudioSource platformSound;
    [SerializeField] private GameObject PlatformObject;
    [SerializeField] private GameObject platfromText;
    public Animator CRANK;
    private bool platformOpen =false;
    private bool onSpot= false;
    private float time,waitingtime=2f;
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && onSpot)
        {
            if (time < Time.time)
            {
                time = Time.time + waitingtime;
                if (!platformOpen)
                {
                    CRANK.SetBool("open", true);
                    platformSound.Play();
                    PlatformObject.SetActive(false);
                    platformOpen = true;
                }
                else
                {
                    CRANK.SetBool("open", false);
                    platformSound.Play();
                    PlatformObject.SetActive(true);
                    platformOpen = false;

                }
            }
        }
    }
   
    private void OnTriggerEnter2D(Collider2D other)

    {
        if(other.tag == "Player")
            onSpot = true;
        platfromText.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            onSpot = false;
        platfromText.SetActive(false);
    }
    
    
}
