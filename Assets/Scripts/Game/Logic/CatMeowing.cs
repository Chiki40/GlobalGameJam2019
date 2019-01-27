using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMeowing : MonoBehaviour
{
    public float fTimeBetweenMeows = 10.0f;
    public float fSoundMaxDistance = 100.0f;

    private float fCurrentTimeBetweenMeows = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        fCurrentTimeBetweenMeows = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        fCurrentTimeBetweenMeows = Mathf.Min(fCurrentTimeBetweenMeows + Time.deltaTime, fTimeBetweenMeows);
        if (fCurrentTimeBetweenMeows >= fTimeBetweenMeows)
        {
            fCurrentTimeBetweenMeows = 0.0f;
            UtilSound.instance.PlaySound("meow", 1.0f, false, true, false, 0.0f, true, this.gameObject, fSoundMaxDistance);
        }
    }
}
