using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RatJump : SpecialComponent
{
    [SerializeField]
    private float m_VerticalForce = 900;
    [SerializeField]
    private float m_HorizontalForce = 450;
    [SerializeField]
    private float m_JumpTimerMin = 0.5f;
    [SerializeField]
    private float m_JumpTimerMax = 1.5f;
    [SerializeField]
    [Range(0, 1)]
    private float m_JumpChance = 0.5f;

    private Rigidbody2D m_RB;
    private float m_JumpTimer;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_CanPerformSpecial)
            return;

        if (m_JumpTimer > 0)
        {
            m_JumpTimer -= Time.deltaTime;
            if (m_JumpTimer <= 0)
            {
                Jump();
                SetTimer();
            }
        }
    }

    // jump...
    private void Jump()
    {
        // Don't jump
        if (UnityEngine.Random.Range(0f, 1f) > m_JumpChance)
            return;

        Debug.Log("JUMP");

        switch(UnityEngine.Random.Range(0, 2))
        {
            case 0:
                m_RB.AddForce(new Vector2(m_HorizontalForce, m_VerticalForce));
                break;
            case 1:
                m_RB.AddForce(new Vector2(-m_HorizontalForce, m_VerticalForce));
                break;
            default:
                break;
        }
    }

    private void SetTimer()
    {
        m_JumpTimer = UnityEngine.Random.Range(m_JumpTimerMin, m_JumpTimerMax);
    }

    public override void ToggleCanPerformSpecial()
    {
        base.ToggleCanPerformSpecial();
        SetTimer();
    }
}
