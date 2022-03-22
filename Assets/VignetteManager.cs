using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteManager : MonoBehaviour
{
    float m_scale = 1.0f;
    float m_interval = 0.5f; // usually chevron distance

    private readonly float SCALE_MAX = 7.0f;
    private readonly float SCALE_MIN = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RespondToKeyInput();

        float scale = m_scale;
    }

    void RespondToKeyInput()
    {
        float interval = m_interval*Time.deltaTime;

        if (Input.GetKey("c") && m_scale - interval > SCALE_MIN)
        {
            //Debug.Log("slower");
            transform.localScale = Vector3.one * m_scale;
            Debug.Log(transform.localScale);

            m_scale -=  interval;
        }
        

        if (Input.GetKey("x") && m_scale + interval < SCALE_MAX)
        {
            //Debug.Log("faster");
            transform.localScale = Vector3.one * m_scale;
            Debug.Log(transform.localScale);
            m_scale += interval;
        }
        
    }
}
