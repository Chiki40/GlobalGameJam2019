﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;

public class GameManager : MonoBehaviour {

    [System.Serializable]
    public struct ClueInfo
    {
        public UnityEvent eClueEvent;
        public float fTimeToGiveClue;
    }

    public ClueInfo[]CluesInfo;

    public UnityEvent eFinishEvent = null;
    public UnityEvent eStartEvent = null;
    public float m_fStartDelay = 0.5f;
    private bool m_bCluesDisabled = false;
    private bool m_bLevelCompleted = false;
    private float m_fCurrentTimeBetweenClues;
    private uint m_uCurrentClue;

	// Use this for initialization
	void Start()
    {
        UtilSound.instance.StopAllSounds();
        m_bCluesDisabled = false;
        m_bLevelCompleted = false;
        m_fCurrentTimeBetweenClues = 0.0f;
        m_uCurrentClue = 0u;
        MaterialLocalization materialLocalization = GameObject.FindGameObjectWithTag("Player").transform.Find("FirstPersonCharacter").Find("Hand").Find("Photo").GetComponent<MaterialLocalization>();
        if (materialLocalization)
        {
            LocalizationUtils.UpdateHandMaterial(materialLocalization);
        }
        StartCoroutine(DelayedStart());
        UtilSound.instance.PlaySound("birds", 0.4f, true);
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(m_fStartDelay);
        eStartEvent.Invoke();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ReturnToMainMenu();
        }
        else if (Input.GetButtonDown("Restart"))
        {
            Restart();
        }

        //Input.GetKeyDown(KeyCode.LeftAlt)
        if (Input.GetButtonDown("Alt") && !m_bLevelCompleted) // Only manual clues are accepted if clues are disabled but level is not completed (which means every clue was received)
        {
            if(m_uCurrentClue> 0)
            {
                ShowClue(m_uCurrentClue - 1u);
            }
        }

        if (!m_bCluesDisabled)
        {
            if (CluesInfo.Length > 0)
            {
                m_fCurrentTimeBetweenClues = Mathf.Min(m_fCurrentTimeBetweenClues + Time.deltaTime, CluesInfo[m_uCurrentClue].fTimeToGiveClue);
                if ( m_fCurrentTimeBetweenClues >= CluesInfo[m_uCurrentClue].fTimeToGiveClue)
                {
                    ShowClue(m_uCurrentClue);
                    m_fCurrentTimeBetweenClues = 0.0f;
                    ++m_uCurrentClue;
                    if (m_uCurrentClue >= CluesInfo.Length)
                    {
                        m_bCluesDisabled = true; // All clues given, stop clues
                    }
                }
            }
        }
	}

    private void ShowClue(uint uClue)
    {
        CluesInfo[uClue].eClueEvent.Invoke();
    }

    public void ObjectEventCompleted()
    {
        m_bLevelCompleted = true;
        m_bCluesDisabled = true; // Level completed, stop automatic clues
        eFinishEvent.Invoke();
    }

    public void Restart()
    {
        //UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        //UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene("MainMenu");
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
