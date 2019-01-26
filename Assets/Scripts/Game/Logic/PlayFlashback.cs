using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFlashback : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onFlashbackStart;

    public void playFlashback()
    {
        onFlashbackStart.Invoke();
    }

}
