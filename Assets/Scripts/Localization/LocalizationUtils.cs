using UnityEngine;
using UnityEngine.UI;

public class LocalizationUtils
{
    public static void UpdateLanguage()
    {
        int iLanguage = PlayerPrefs.GetInt("Language");
        Localization[] LocalizatedStuff = GameObject.FindObjectsOfType<Localization>();
        for (int i = 0; i < LocalizatedStuff.Length; ++i)
        {
            Text textToLocalize = LocalizatedStuff[i].gameObject.GetComponent<Text>();
            if (textToLocalize)
            {
                textToLocalize.text = (iLanguage == 0 ? LocalizatedStuff[i].sEnglishText : LocalizatedStuff[i].sSpanishText);
            }
        }
    }

    public static void UpdateImages()
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

    public static void UpdateHandMaterial(MaterialLocalization materialLocalization)
    {
        int iLanguage = PlayerPrefs.GetInt("Language");
        Renderer renderer = materialLocalization.gameObject.GetComponent<Renderer>();
        if(renderer)
        {
            renderer.material = (iLanguage == 0 ? materialLocalization.MaterialEnglish : materialLocalization.MaterialSpanish);
        }
    }
}
