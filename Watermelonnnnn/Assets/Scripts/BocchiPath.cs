using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BocchiPath : ExplosionComponent
{
    // must think about this later
    [SerializeField]
    private Vector3 m_LeftCorner = new Vector3(-3.23f, -4.43f, 0);
    [SerializeField]
    private Vector3 m_RightCorner = new Vector3(3.85f, -4.43f, 0);
    [SerializeField]
    private float m_MoveSpeed = 2;
    [SerializeField]
    private float m_MinDistanceToCorner = 0.05f;

    private Vector3 m_CornerToMoveTowards;
    private bool m_ReachedCorner = false;

    protected override void Awake()
    {
        base.Awake();
        m_CornerToMoveTowards = SelectCorner();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (m_ReachedCorner)
        {
            base.Update();
        } else if (m_CanPerformSpecial)
        {
            //Debug.Log(Vector3.MoveTowards(m_RB.position, m_CornerToMoveTowards, m_MoveSpeed * Time.deltaTime));
            m_RB.MovePosition(Vector3.MoveTowards(m_RB.position, m_CornerToMoveTowards, m_MoveSpeed * Time.deltaTime));
            if (Vector3.Distance(m_RB.position, m_CornerToMoveTowards) <= m_MinDistanceToCorner)
                m_ReachedCorner = true;
        }
    }

    private Vector3 SelectCorner()
    {
        if (Vector3.Distance(m_RB.position, m_LeftCorner) < Vector3.Distance(m_RB.position, m_RightCorner))
            return m_LeftCorner;
        else
            return m_RightCorner;
    }

}
