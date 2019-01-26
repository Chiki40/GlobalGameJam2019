using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float m_fTimeInIntro = 5.0f;

    private float m_fCurrentTimeInIntro = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_fCurrentTimeInIntro = 0.0f;
        UtilSound.instance.PlaySound("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }

        m_fCurrentTimeInIntro = Mathf.Min(m_fCurrentTimeInIntro + Time.deltaTime, m_fTimeInIntro);
        if (m_fCurrentTimeInIntro >= m_fTimeInIntro)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
