using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
   
    public GameObject mainmenu, pickhero, config, skills, leaderboards;

    public GameObject attacks, appearance;

    public Button btnAttack, btnAppearance;

    public GameObject controlSens, controlVolume;

    public static float heroSpeed = 4, masterVolume = 0.2f;

    public AudioSource backgroundMusic;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("playerVolume") == 0)
        {

            controlVolume.GetComponent<Slider>().value = 0.2f;
            PlayerPrefs.SetFloat("playerVolume", 0.2f);
            backgroundMusic.volume = 0.2f;

        }

        if (PlayerPrefs.GetFloat("playerSensitivity") == 0)
        {

            controlSens.GetComponent<Slider>().value = 4f;
            PlayerPrefs.SetFloat("playerSensitivity", 4f);

        }
    }

    public void pressedPlay()
    {

        mainmenu.SetActive(false);
        pickhero.SetActive(true);


    }

    public void pressedAppearance()
    {

        attacks.SetActive(false);
        appearance.SetActive(true);

        btnAppearance.GetComponent<RectTransform>().localScale = new Vector3(3.5f, 3.5f, 3.5f);
        btnAttack.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 2.5f, 2.5f);

    }

    public void pressedAttacks()
    {

        appearance.SetActive(false);
        attacks.SetActive(true);

        btnAppearance.GetComponent<RectTransform>().localScale = new Vector3(2.5f, 2.5f, 2.5f);
        btnAttack.GetComponent<RectTransform>().localScale = new Vector3(3.5f, 3.5f, 3.5f);

    }

    public void pressedConfig()
    {

        controlSens.GetComponent<Slider>().value = PlayerPrefs.GetFloat("playerSensitivity");
        controlVolume.GetComponent<Slider>().value = PlayerPrefs.GetFloat("playerVolume");

        mainmenu.SetActive(false);
        config.SetActive(true);

    }

    public void pressedSkills()
    {



    }

    public void pressedLeaderboard()
    {

        

    }


    public void home()
    {

        pickhero.SetActive(false);
        config.SetActive(false);
//        skills.SetActive(false);
//        leaderboards.SetActive(false);
        mainmenu.SetActive(true);

        heroSpeed = controlSens.GetComponent<Slider>().value;
        masterVolume = controlVolume.GetComponent<Slider>().value;

        PlayerPrefs.SetFloat("playerSensitivity", heroSpeed);
        PlayerPrefs.SetFloat("playerVolume", masterVolume);

    }

    public void changedVolume()
    {

        backgroundMusic.volume = controlVolume.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("playerVolume", masterVolume);

    }

}
