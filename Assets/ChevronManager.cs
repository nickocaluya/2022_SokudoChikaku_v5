using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChevronManager : MonoBehaviour
{
    float m_speed = 0.1f;
    float MAX_DISPLACEMENT = 0.3f; // usually chevron distance
    float distance = 0;

    Vector3 origPos = new Vector3();
    Vector3 origPosSphere = new Vector3();
    Vector3 displacementVec = new Vector3();
    //Quaternion origRot = new Quaternion();


    public GameObject sphereObj;


    private readonly float SPEED_MAX = 1.0f;
    private readonly float SPEED_MIN = 0.1f;

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
        origPos = transform.localPosition;
        origPosSphere = sphereObj.transform.position;
        displacementVec = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //RespondToKeyInput();
        AutoAnimate();

        float speed = m_speed * Time.deltaTime;
        float curPos = transform.position.z + speed;
        distance += speed;

        if (distance > MAX_DISPLACEMENT)
        {
            transform.localPosition = origPos;
            displacementVec.z = MAX_DISPLACEMENT - speed;
            transform.localPosition = transform.localPosition + displacementVec;
            distance = 0;

            //Debug.Log(gameObject.name + " " + origPos + "GREATER");
        }
        else if (distance == MAX_DISPLACEMENT)
        {
            transform.localPosition = origPos;
            distance = 0;
        }
        else
        {
            displacementVec.z = speed;
            transform.localPosition = transform.localPosition + displacementVec;
        }

    }

    void RespondToKeyInput()
    {
        if (Input.GetKey("s") && m_speed - 0.01f > SPEED_MIN)
        {
            //Debug.Log("slower");
            m_speed -= 0.01f;
        }

        if (Input.GetKey("f") && m_speed + 0.01f < SPEED_MAX)
        {
            //Debug.Log("faster");
            m_speed += 0.01f;
        }
    }


    void AutoAnimate()
    {
        m_time += Time.deltaTime;

        if (m_time <= MAX_ANIM_TIME && (m_currentAnim == ANIM_STATE.Slow || m_currentAnim == ANIM_STATE.Fast))
        {
            float lerpTime = m_time / MAX_ANIM_TIME;

            m_speed = (m_currentAnim == ANIM_STATE.Slow) ? SPEED_MIN : SPEED_MAX;
        }
        else if (m_time <= MAX_TRANS_TIME && (m_currentAnim == ANIM_STATE.Up || m_currentAnim == ANIM_STATE.Down))
        {
            float transTime = m_time / MAX_TRANS_TIME;
            float lerpDir = (m_currentAnim == ANIM_STATE.Up) ? transTime : 1.0f - transTime;

            m_speed = Mathf.Lerp(SPEED_MIN, SPEED_MAX, lerpDir);
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
