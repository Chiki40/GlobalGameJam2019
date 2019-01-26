using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{
    [System.Serializable]
    public struct SubtitleEntry
    {
        public string sText;
        public float fTimeout;
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
        if (m_TextSubtitles)
        {
            m_TextSubtitles.text = subtitles[0u].sText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bPlaying)
        {
            m_fCurrentTimeout = Mathf.Min(m_fCurrentTimeout + Time.deltaTime, subtitles[m_uCurrentSubtitleNum].fTimeout);
            if (m_fCurrentTimeout >= subtitles[m_uCurrentSubtitleNum].fTimeout)
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
                    if (m_TextSubtitles)
                    {
                        m_TextSubtitles.text = subtitles[m_uCurrentSubtitleNum].sText;
                    }
                }
            }
        }
    }
}
