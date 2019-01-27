using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Subtitles : MonoBehaviour
{
    [System.Serializable]
    public struct SubtitleEntry
    {
        public string sText;
        public string sTextES;
        public string sSoundName;
        public string sSoundNameES;
    }

    private uint m_uCurrentSubtitleNum = 0u;
    private float m_fCurrentTimeout = 0.0f;
    private bool m_bPlaying = false;
    private Text m_TextSubtitles = null;
    public SubtitleEntry[] subtitles;
    public UnityEvent introEnd;

    // Start is called before the first frame update
    void Start()
    {
        m_uCurrentSubtitleNum = 0u;
        m_fCurrentTimeout = 0.0f;
        m_bPlaying = false;
        m_TextSubtitles = gameObject.GetComponent<Text>();
        if (!m_TextSubtitles)
        {
            Debug.LogError("[Subtitles.Start] ERROR. Text component not found in " + gameObject.name);
            return;
        }
    }

    public void PlaySubtitles()
    {
        if (!m_bPlaying)
        {
            m_uCurrentSubtitleNum = 0u;
            m_fCurrentTimeout = 0.0f;
            m_bPlaying = true;
            if (m_TextSubtitles) // First subtitle
            {
                int iLanguage = PlayerPrefs.GetInt("Language");
                m_TextSubtitles.text = (iLanguage == 0 ? subtitles[0u].sText : subtitles[0u].sTextES);
                UtilSound.instance.PlaySound(iLanguage == 0 ? subtitles[0u].sSoundName : subtitles[0u].sSoundNameES);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bPlaying)
        {
            int iLanguage = PlayerPrefs.GetInt("Language");
            float fTimeOut = UtilSound.instance.GetClipLength(iLanguage == 0 ? subtitles[m_uCurrentSubtitleNum].sSoundName : subtitles[m_uCurrentSubtitleNum].sSoundNameES);
            m_fCurrentTimeout = Mathf.Min(m_fCurrentTimeout + Time.deltaTime, fTimeOut);
            if (m_fCurrentTimeout >= fTimeOut)
            {
                m_fCurrentTimeout = 0.0f;
                ++m_uCurrentSubtitleNum;
                if (m_uCurrentSubtitleNum >= subtitles.Length)
                {
                    m_bPlaying = false;
                    if (m_TextSubtitles)
                    {
                        m_TextSubtitles.text = "";
                    }

                    if (introEnd != null)
                    {
                        introEnd.Invoke();
                    }
                }
                else
                {
                    if (m_TextSubtitles) // New subtitle
                    {
                        m_TextSubtitles.text = (iLanguage == 0 ? subtitles[m_uCurrentSubtitleNum].sText : subtitles[m_uCurrentSubtitleNum].sTextES);
                        UtilSound.instance.PlaySound(iLanguage == 0 ? subtitles[m_uCurrentSubtitleNum].sSoundName : subtitles[m_uCurrentSubtitleNum].sSoundNameES);
                    }
                }
            }
        }
    }
}
