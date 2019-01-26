using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class DirectoCallback : MonoBehaviour
{
    public PlayableDirector director;
    public UnityEvent onFinished;

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        onFinished.Invoke();
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
