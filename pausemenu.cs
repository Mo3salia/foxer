using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    [SerializeField] private AudioSource click;
    [SerializeField] private GameObject pause_menu;
    public bool gamepaused = false;
    public void RESTART()
    {
        click.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(globalLevel.Levelnumber);
        GEMS_collect.GEMScount = 0;
    }

    public void GoToMainMenu()
    {
        click.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        GEMS_collect.GEMScount = 0;
    }
    public void RESUME()
    {
        gamepaused = false;
        click.Play();
        Time.timeScale = 1;
        pause_menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gamepaused == false)
            {
                Time.timeScale = 0;
                gamepaused = true;
                Cursor.visible = true;
                pause_menu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                gamepaused = false;
                Cursor.visible = false;
                pause_menu.SetActive(false);

            }
        }
    }
}
