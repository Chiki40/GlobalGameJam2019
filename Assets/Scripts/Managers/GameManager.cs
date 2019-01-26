using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float []fTimeToGiveClues;

    private bool m_bCluesDisabled = false;
    private float m_fCurrentTimeBetweenClues;
    private uint m_uCurrentClue;

	// Use this for initialization
	void Start ()
    {
        UtilSound.instance.StopAllSounds();
        m_bCluesDisabled = false;
        m_fCurrentTimeBetweenClues = 0.0f;
        m_uCurrentClue = 0u;
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

        if (!m_bCluesDisabled)
        {
            m_fCurrentTimeBetweenClues = Mathf.Min(m_fCurrentTimeBetweenClues + Time.deltaTime, fTimeToGiveClues[m_uCurrentClue]);
            if (m_fCurrentTimeBetweenClues >= fTimeToGiveClues[m_uCurrentClue])
            {
                // Show hint m_uCurrentClue
                GameObject.Find("Diary").GetComponent<Diary>().ShowEntries(m_uCurrentClue);
                m_fCurrentTimeBetweenClues = 0.0f;
                ++m_uCurrentClue;
                if (m_uCurrentClue >= fTimeToGiveClues.Length)
                {
                    m_bCluesDisabled = true; // All clues given, stop clues
                }
            }
        }
	}

    public void ObjectEventCompleted()
    {
        Debug.Log("ObjectEventCompleted");
        m_bCluesDisabled = true; // Level completed, stop clues
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

}
