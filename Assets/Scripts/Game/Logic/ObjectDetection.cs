﻿using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    private GameObject m_CurrentDetectableObject = null;
    private float m_fDetectionTimeOfCurrentObject = 0.0f;
    private Camera m_Camera = null;
    private GameObject m_GameFlores = null;
    private GameManager m_GameManager = null;
    private float m_fCurrentTimeLookingAtObject = 0.0f;
    private bool m_bObjectDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = transform.GetComponentInChildren<Camera>();
        if (!m_Camera)
        {
            Debug.LogError("[ObjectDetection.Start] ERROR. Camera not found in entity " + gameObject.name);
            return;
        }

        m_GameFlores = GameObject.FindGameObjectWithTag("GameFlores");
        if (!m_GameFlores)
        {
            Debug.LogError("[ObjectDetection.Start] ERROR. GameFlores not found in scene");
            return;
        }

        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (!gameManagerObj)
        {
            Debug.LogError("[ObjectDetection.Start] ERROR. GameManager not found in scene");
            return;
        }
        m_GameManager = gameManagerObj.GetComponent<GameManager>();
        if (!m_GameManager)
        {
            Debug.LogError("[ObjectDetection.Start] ERROR. Object " + gameManagerObj.name + " does not have GameManager component");
            return;
        }

        Reset();
    }

    public void Reset()
    {
        m_fCurrentTimeLookingAtObject = 0.0f;
        m_bObjectDetected = false;
        m_fDetectionTimeOfCurrentObject = 0.0f;
        //GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        if (m_GameFlores)
        {
            m_GameFlores.transform.Find("BarUp").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 0.0f, 1.0f);
            m_GameFlores.transform.Find("BarDown").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 0.0f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bObjectDetected)
        {
            bool bFocusing = false;
            if (m_Camera)
            {
                if (m_CurrentDetectableObject)
                {
                    DetectableObject DetectableComp = m_CurrentDetectableObject.GetComponent<DetectableObject>();
                    if (DetectableComp)
                    {
                        m_fDetectionTimeOfCurrentObject = DetectableComp.fDetectionTimeLookingAtObject;
                        Vector3 targetDir = m_CurrentDetectableObject.transform.position - m_Camera.transform.position;
                        float angle = Vector3.Angle(targetDir, m_Camera.transform.forward);
                        if (angle <= DetectableComp.fDetectionAngleOffset)
                        {
                            bFocusing = true;
                            m_fCurrentTimeLookingAtObject = Mathf.Min(m_fCurrentTimeLookingAtObject + Time.deltaTime, m_fDetectionTimeOfCurrentObject);
                            if (m_fCurrentTimeLookingAtObject >= m_fDetectionTimeOfCurrentObject)
                            {
                                //Debug.Log("Conseguido!\n");
                                m_bObjectDetected = true;
                                m_CurrentDetectableObject.transform.GetComponentInChildren<Collider>().enabled = false;
                                m_CurrentDetectableObject.GetComponentInChildren<PlayFlashback>().playFlashback();
                                m_CurrentDetectableObject = null;
                                GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
                                m_GameManager.ObjectEventCompleted();
                            }
                            else
                            {
                                float progress = m_fCurrentTimeLookingAtObject / m_fDetectionTimeOfCurrentObject;
                                //Debug.Log(progress);
                                if (m_GameFlores)
                                {
                                    m_GameFlores.transform.Find("BarUp").GetComponent<RectTransform>().localScale = new Vector3(1.0f, progress, 1.0f);
                                    m_GameFlores.transform.Find("BarDown").GetComponent<RectTransform>().localScale = new Vector3(1.0f, progress, 1.0f);
                                }
                            }
                        }
                    }
                }
            }
            if (!bFocusing && m_fDetectionTimeOfCurrentObject > 0.0f)
            {
                m_fCurrentTimeLookingAtObject = Mathf.Max(m_fCurrentTimeLookingAtObject - Time.deltaTime, 0.0f);
                float progress = m_fCurrentTimeLookingAtObject / m_fDetectionTimeOfCurrentObject;
                //Debug.Log(progress);
                if (m_GameFlores)
                {
                    m_GameFlores.transform.Find("BarUp").GetComponent<RectTransform>().localScale = new Vector3(1.0f, progress, 1.0f);
                    m_GameFlores.transform.Find("BarDown").GetComponent<RectTransform>().localScale = new Vector3(1.0f, progress, 1.0f);
                }
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
