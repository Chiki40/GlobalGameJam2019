﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityStandardAssets.Characters.FirstPerson.MouseLook mouseLook = new UnityStandardAssets.Characters.FirstPerson.MouseLook();
        mouseLook.SetCursorLock(false);
        //UtilSound.instance.StopAllSounds();
        #if (!UNITY_STANDALONE_WIN && !UNITY_STANDALONE_OSX) || UNITY_EDITOR
            transform.Find("Background").Find("ButtonPanel").Find("Exit").GetComponent<Button>().interactable = false;
        #endif
        LocalizationUtils.UpdateLanguage();
        LocalizationUtils.UpdateImages();
        int iLanguage = PlayerPrefs.GetInt("Language");
        if (iLanguage == 0)
        {
            transform.Find("Background").Find("English").GetComponent<Button>().interactable = false;
            transform.Find("Background").Find("Spanish").GetComponent<Button>().interactable = true;
        }
        else
        {
            transform.Find("Background").Find("English").GetComponent<Button>().interactable = true;
            transform.Find("Background").Find("Spanish").GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ExitGame();
        }
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Tienda");
    }


    public void OnClickCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnClickExit()
    {
        ExitGame();
    }

    public void OnClickSpanish()
    {
        PlayerPrefs.SetInt("Language", 1);
        PlayerPrefs.Save();
        LocalizationUtils.UpdateLanguage();
        LocalizationUtils.UpdateImages();
    }

    public void OnClickEnglish()
    {
        PlayerPrefs.SetInt("Language", 0);
        PlayerPrefs.Save();
        LocalizationUtils.UpdateLanguage();
        LocalizationUtils.UpdateImages();
    }

    private void ExitGame()
    {
        #if (UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX) && !UNITY_EDITOR
            //UtilSound.instance.PlaySound("click", 1.0f, false, true);
            Application.Quit();
        #endif
    }
}
