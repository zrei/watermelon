using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlaneMovement : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed;

    private Rigidbody2D m_RB;

    private int m_Direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_RB.position + new Vector2(m_Direction, 0) * m_MoveSpeed * Time.deltaTime);
        m_RB.MovePosition(m_RB.position + new Vector2(m_Direction, 0) * m_MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Waypoint"))
            m_Direction = -m_Direction; // turn around
    }
}
