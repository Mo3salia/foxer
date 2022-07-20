using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    
    public void RESTART()
    {
        click.Play();
        SceneManager.LoadScene(globalLevel.Levelnumber);
        GEMS_collect.GEMScount = 0;
    }

    public void GoToMainMenu ()
    {
        click.Play();
        SceneManager.LoadScene(1);
        GEMS_collect.GEMScount = 0;
    }

    
        
    
}
