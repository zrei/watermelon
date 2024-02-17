using Unity.VisualScripting;
using UnityEngine;

// this should also be a singleton???
public class MouseClicker : MonoBehaviour
{
    private bool m_IsHolding;
    private GameObject m_HeldObject;
    private Vector3 m_MousePosition;
    private float m_PrevGravity;

    private void Awake()
    {
        GlobalEvents.OnExplodeEvent += CheckHeld;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnExplodeEvent -= CheckHeld;
    }

    private void Update()
    {
        m_MousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
            OnMouseClicked();

        if (m_IsHolding)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(m_MousePosition);
            m_HeldObject.transform.position = new Vector3(worldPoint.x, worldPoint.y, 0);
        }
    }

    private void OnMouseClicked()
    {
        if (m_IsHolding)
            LetGo();
        else
            TryPickUp();
    }

    private void LetGo()
    {
        // check later
        SpecialComponent special = m_HeldObject.GetComponent<SpecialComponent>();
        if (special != null)
            special.ToggleCanPerformSpecial();
        m_HeldObject.GetComponent<Rigidbody2D>().gravityScale = m_PrevGravity;
        ClearHeld();
    }

    private void TryPickUp()
    {
        Debug.Log("Try pick up");
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(m_MousePosition);
        Collider2D[] objects = Physics2D.OverlapPointAll(worldPoint);
        
        foreach (Collider2D c in objects)
            if (c.gameObject.CompareTag("Fruit"))
            {
                SpecialComponent special = c.gameObject.GetComponent<SpecialComponent>();
                if (special != null)
                    special.ToggleCanPerformSpecial();
                PickUp(c.gameObject);
                break;
            }
    }

    private void PickUp(GameObject gameObject)
    {
        m_IsHolding = true;
        m_HeldObject = gameObject;
        m_PrevGravity = m_HeldObject.GetComponent<Rigidbody2D>().gravityScale;
        m_HeldObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
    }

    private void CheckHeld(GameObject gameObject)
    {
        if (gameObject == m_HeldObject)
        {
            ClearHeld();
        }
    }
    
    private void ClearHeld()
    {
        m_IsHolding = false;
        m_HeldObject = null;
    }
}