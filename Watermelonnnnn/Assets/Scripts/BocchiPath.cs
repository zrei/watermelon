using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BocchiPath : MonoBehaviour
{

    // must think about this later
    [SerializeField]
    private Vector3 m_LeftCorner = new Vector3(0, 0, 0);
    [SerializeField]
    private Vector3 m_RightCorner = new Vector3(0, 0, 0);
    [SerializeField]
    private float m_MoveSpeed;
    [SerializeField]
    private float m_ExplosionTimerMin;
    [SerializeField]
    private float m_ExplosionTimerMax;
    [SerializeField]
    private float m_MinDistanceToCorner;
    [SerializeField]
    private float m_ExplosionRadius;
    [SerializeField]
    private float m_ExplosionForce;
    [SerializeField]
    private LayerMask m_AffectedLayers;

    private Rigidbody2D m_RB;

    private Vector3 m_CornerToMoveTowards;
    private bool m_ReachedCorner = false;
    private float m_ExplosionTimer; 

    private bool m_CanMove = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_CornerToMoveTowards = SelectCorner();
        m_ExplosionTimer = UnityEngine.Random.Range(m_ExplosionTimerMin, m_ExplosionTimerMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ReachedCorner)
        {
            Debug.Log("Doing explosions");
            m_ExplosionTimer -= Time.deltaTime;
            if (m_ExplosionTimer <= 0)
            {
                Collider2D[] m_Objects = Physics2D.OverlapCircleAll(m_RB.position, m_ExplosionRadius, m_AffectedLayers);
                foreach (Collider2D o in m_Objects)
                {
                    Vector2 otherPosition = new Vector2(o.gameObject.transform.localPosition.x, o.gameObject.transform.localPosition.y);
                    Vector2 directionVector = (otherPosition - m_RB.position).normalized;
                    o.gameObject.GetComponent<Rigidbody2D>().AddForce(directionVector * m_ExplosionForce);
                }
                // explode radius... something
                Destroy(gameObject);
            }
            // flash red coroutine or something idk
        } else if (m_CanMove)
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

    public void ToggleCanMove()
    {
        m_CanMove = !m_CanMove;
    }
}
