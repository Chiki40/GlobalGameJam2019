using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diary : MonoBehaviour
{
    public string[] lEntries = null;
    private Transform Background = null;

    void Awake()
    {
        Background = transform.Find("Background");
        if (!Background)
        {
            Debug.LogError("[Diary.Start] ERROR. 'Background' object not found as " + gameObject.name + " child");
            return;
        }
        for (int i = 0; i < lEntries.Length; ++i)
        {
            Transform Child = Background.Find("Clue" + i);
            if (!Child)
            {
                Debug.LogError("[Diary.Start] ERROR. 'Clue'" + i + " object not found as " + Background.gameObject.name);
                return;
            }
            Text ChildText = Child.gameObject.GetComponent<Text>();
            if (!ChildText)
            {
                Debug.LogError("[Diary.Start] ERROR. " + Child.name + " does not have Text Component");
                return;
            }
            ChildText.text = "";
            ChildText.enabled = false;
        }
        Background.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowEntries(uint uLastEntry)
    {
        Background.gameObject.SetActive(true);
        for (int i = 0; i <= uLastEntry; ++i)
        {
            Transform Child = Background.Find("Clue" + i);
            Text ChildText = Child.gameObject.GetComponent<Text>();
            ChildText.enabled = true;
            ChildText.text = lEntries[i];
        }
    }
}
