using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    [System.Serializable]
    public struct SubtitleEntry
    {
        public string sText;
        public string sSoundName;
    }

    private uint m_uCurrentSubtitleNum = 0u;
    private float m_fCurrentTimeout = 0.0f;
    private bool m_bPlaying = false;
    private Text m_TextSubtitles = null;
    public SubtitleEntry[] subtitles;

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
        m_uCurrentSubtitleNum = 0u;
        m_fCurrentTimeout = 0.0f;
        m_bPlaying = true;
        if (m_TextSubtitles) // First subtitle
        {
            m_TextSubtitles.text = subtitles[0u].sText;
            UtilSound.instance.PlaySound(subtitles[0u].sSoundName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bPlaying)
        {
            float fTimeOut = UtilSound.instance.GetClipLength(subtitles[m_uCurrentSubtitleNum].sSoundName);
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
                }
                else
                {
                    if (m_TextSubtitles) // New subtitle
                    {
                        m_TextSubtitles.text = subtitles[m_uCurrentSubtitleNum].sText;
                        UtilSound.instance.PlaySound(subtitles[m_uCurrentSubtitleNum].sSoundName);
                    }
                }
            }
        }
    }
}
