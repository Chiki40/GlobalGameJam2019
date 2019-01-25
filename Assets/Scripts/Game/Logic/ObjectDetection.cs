using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    private GameObject m_CurrentDetectableObject = null;
    private Camera m_Camera = null;
    private float m_fCurrentTimeLookingAtObject = 0.0f;

    public float fAngleOffset = 8.0f;
    public float fTimeLookingAtObject = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_fCurrentTimeLookingAtObject = 0.0f;
        m_Camera = transform.GetComponentInChildren<Camera>();
        if (!m_Camera)
        {
            Debug.LogError("[ObjectDetection.Start] ERROR. Camera not found in entity " + gameObject.name);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Camera)
        {
            if (m_CurrentDetectableObject)
            {
                Vector3 targetDir = m_CurrentDetectableObject.transform.position - m_Camera.transform.position;
                float angle = Vector3.Angle(targetDir, m_Camera.transform.forward);
                if (angle <= fAngleOffset)
                {
                    m_fCurrentTimeLookingAtObject = Mathf.Min(m_fCurrentTimeLookingAtObject + Time.deltaTime, fTimeLookingAtObject);
                    if (m_fCurrentTimeLookingAtObject >= fTimeLookingAtObject)
                    {
                        Debug.Log("Conseguido!\n");
                        m_CurrentDetectableObject.transform.GetComponentInChildren<Collider>().enabled = false;
                        m_CurrentDetectableObject = null;
                    }
                    else
                    {
                        Debug.Log(m_fCurrentTimeLookingAtObject + " / " + fTimeLookingAtObject);
                    }
                }
                else // Not looking close enough to the object
                {
                    m_fCurrentTimeLookingAtObject = 0.0f;
                }
            }
            else // Not object to look at
            {
                m_fCurrentTimeLookingAtObject = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject triggerParentObject = other.transform.parent.gameObject;
        Debug.Log(triggerParentObject.name);
        if (!m_CurrentDetectableObject && triggerParentObject.tag == "DetectableObject") // Add object if an object didn't exist
        {
            m_CurrentDetectableObject = triggerParentObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject triggerParentObject = other.transform.parent.gameObject;
        Debug.Log(triggerParentObject.name);
        if (m_CurrentDetectableObject == triggerParentObject && triggerParentObject.tag == "DetectableObject") // Remove object is it existed
        {
            m_CurrentDetectableObject = null;
        }
    }
}
