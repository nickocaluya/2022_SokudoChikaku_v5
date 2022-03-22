using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangaLines : MonoBehaviour
{
    float m_scale = 0.02f;
    float m_interval = 0.005f;

    private Vector3 initScale;
    private List<Transform> meshes = new List<Transform>();

    private readonly float SCALE_MAX = 0.03f;
    private readonly float SCALE_MIN = 0.001f;

    private enum ANIM_STATE
    {
        Slow = 0,
        Up = 1,
        Fast = 2,
        Down = 3
    };

    private float m_time = 0.0f;
    private ANIM_STATE m_currentAnim = ANIM_STATE.Slow;

    private readonly float MAX_ANIM_TIME = 0.0f;
    private readonly float MAX_TRANS_TIME = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        initScale = new Vector3(0.06f, 5.0f, 1.0f);

        MeshRenderer[] mrs = gameObject.GetComponentsInChildren<MeshRenderer>();

        //Debug.Log(meshes.Count);

        foreach (MeshRenderer mr in mrs)
        {
            meshes.Add(mr.gameObject.transform);
        }

        //Debug.Log(meshes.Count);
    }

    // Update is called once per frame
    void Update()
    {
        //RespondToKeyInput();
        AutoAnimate();

        //float scale = m_scale;

        Vector3 newScale = new Vector3(m_scale, initScale.y, initScale.z);
        foreach (Transform t in meshes)
        {
            t.localScale = newScale;
        }
    }

    void RespondToKeyInput()
    {
        float interval = m_interval * Time.deltaTime;

        if (Input.GetKey("c") && m_scale - interval > SCALE_MIN)
        {
            //Debug.Log("contract");
            //transform.localScale = Vector3.one * m_scale;
            //Debug.Log(transform.localScale);

            m_scale -= interval;

            Vector3 newScale = new Vector3(m_scale, initScale.y, initScale.z);
            foreach (Transform t in meshes)
            {
                t.localScale = newScale;
                //Debug.Log(transform.localScale);
            }
        }

        if (Input.GetKey("x") && m_scale + interval < SCALE_MAX)
        {
            //Debug.Log("expand");
            //transform.localScale = Vector3.one * m_scale;
            //Debug.Log(transform.localScale);
            m_scale += interval;

            Vector3 newScale = new Vector3(m_scale, initScale.y, initScale.z);
            foreach (Transform t in meshes)
            {
                t.localScale = newScale;
                //Debug.Log(transform.localScale);
            }
        }
    }

    void AutoAnimate()
    {
        m_time += Time.deltaTime;

        if (m_time <= MAX_ANIM_TIME && (m_currentAnim == ANIM_STATE.Slow || m_currentAnim == ANIM_STATE.Fast))
        {
            float lerpTime = m_time / MAX_ANIM_TIME;

            m_scale = (m_currentAnim == ANIM_STATE.Slow) ? SCALE_MIN : SCALE_MAX;
        }
        else if (m_time <= MAX_TRANS_TIME && (m_currentAnim == ANIM_STATE.Up || m_currentAnim == ANIM_STATE.Down))
        {
            float transTime = m_time / MAX_TRANS_TIME;
            float lerpDir = (m_currentAnim == ANIM_STATE.Up) ? transTime : 1.0f - transTime;

            m_scale = Mathf.Lerp(SCALE_MIN, SCALE_MAX, lerpDir);
        }
        else
        {
            switch (m_currentAnim)
            {
                case ANIM_STATE.Slow:
                    {
                        m_currentAnim = ANIM_STATE.Up;
                        break;
                    }
                case ANIM_STATE.Up:
                    {
                        m_currentAnim = ANIM_STATE.Fast;
                        break;
                    }
                case ANIM_STATE.Fast:
                    {
                        m_currentAnim = ANIM_STATE.Down;
                        break;
                    }
                case ANIM_STATE.Down:
                    {
                        m_currentAnim = ANIM_STATE.Slow;
                        break;
                    }
            }

            m_time = 0.0f;
        }
    }

}
