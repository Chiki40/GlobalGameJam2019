using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoor : MonoBehaviour
{
    public float fAngleOpened = 80.0f;
    public float fSpeed = 40.0f;

    private bool m_bOpened = false;

    void Start()
    {
        m_bOpened = false;
    }

    void Update()
    {
        float fAngle = transform.localEulerAngles.y;
        float fNewAngle = fAngle;
        if (m_bOpened)
        {
            fNewAngle = Mathf.Min(fNewAngle + fSpeed * Time.deltaTime, fAngleOpened);
        }
        else
        {
            fNewAngle = Mathf.Max(fNewAngle - fSpeed * Time.deltaTime, 0.0f);
        }
        transform.localEulerAngles = new Vector3(0.0f, fNewAngle, 0.0f);
    }

    public void OpenDoor(bool open)
    {
        m_bOpened = open;
    }
}
