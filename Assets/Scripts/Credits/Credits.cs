﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    public ScrollRect scrollRect = null;
    public float fSpeed = 0.5f;
    public float fTimeToReturnToMainMenu = 5.0f;

    private Coroutine closeCoroutine = null;

    void Awake()
    {
        closeCoroutine = null;
    }

    void Start()
    {
        UpdateImages();
    }

    // Update is called once per frame
    void Update()
    {
        if (scrollRect)
        {
            if (scrollRect.verticalNormalizedPosition > 0.01f)
            {
                scrollRect.verticalNormalizedPosition = Mathf.Max(scrollRect.verticalNormalizedPosition - fSpeed * Time.deltaTime, 0.0f);
            }
            else if (closeCoroutine == null)
            {
                closeCoroutine = StartCoroutine("DelayedReturnToMainMenu");
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            ReturnToMainMenu();
        }
    }

    IEnumerator DelayedReturnToMainMenu()
    {
        yield return new WaitForSeconds(fTimeToReturnToMainMenu);
        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        //UtilSound.instance.PlaySound("click", 1.0f, false, true);
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }
        SceneManager.LoadScene("MainMenu");
    }

    void UpdateImages()
    {
        int iLanguage = PlayerPrefs.GetInt("Language");
        ImageLocalization[] LocalizatedStuff = GameObject.FindObjectsOfType<ImageLocalization>();
        for (int i = 0; i < LocalizatedStuff.Length; ++i)
        {
            Image imageToLocalize = LocalizatedStuff[i].gameObject.GetComponent<Image>();
            if (imageToLocalize)
            {
                imageToLocalize.sprite = (iLanguage == 0 ? LocalizatedStuff[i].EnglishSprite : LocalizatedStuff[i].SpanishSprite);
            }
        }
    }
}
