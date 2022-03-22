using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectionManager : MonoBehaviour
{
    float m_speed = 10.0f;

    private ParticleSystem ps;

    private readonly float SPEED_MAX = 70.0f;
    private readonly float SPEED_MIN = 10.0f;

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
        ps = gameObject.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        RespondToKeyInput();
        //AutoAnimate();

        ParticleSystem.MainModule m = ps.main;

        m.startSpeed = m_speed;

    }

    void RespondToKeyInput()
    {
        if (Input.GetKey("v") && m_speed - 0.01f > SPEED_MIN)
        {
            //Debug.Log("slower");
            m_speed -= 0.1f;
        }

        if (Input.GetKey("b") && m_speed + 0.01f < SPEED_MAX)
        {
            //Debug.Log("faster");
            m_speed += 0.1f;
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
