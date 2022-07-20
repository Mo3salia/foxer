using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEMS_collect : MonoBehaviour
{
    public AudioSource collectFx;
    public static int GEMScount;
    private bool ones = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && ones)
        {
            collectFx.Play();
            GEMScount++;
            ones = false;
            this.gameObject.SetActive(false);

        }
    }
    
}
