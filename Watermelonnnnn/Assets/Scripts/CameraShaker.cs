using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField]
    private float m_TimeToShake;
    [SerializeField]
    private float m_FullShakeForce;

    private Camera m_Camera;
    private float m_ShakeTimer;
    private Vector3 m_InitialPos;

    private void Awake()
    {
        m_Camera = Camera.main;
        m_InitialPos = m_Camera.transform.localPosition;
        GlobalEvents.OnExplodeEvent += StartCameraShake;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnExplodeEvent -= StartCameraShake;
    }

    private void Update()
    {
        if (m_ShakeTimer > 0)
        {
            m_Camera.transform.localPosition = m_InitialPos + Random.insideUnitSphere * Mathf.Lerp(0, m_FullShakeForce, m_ShakeTimer / m_TimeToShake);
            m_ShakeTimer -= Time.deltaTime;
        } else
            m_Camera.transform.localPosition = m_InitialPos;
    }

    private void StartCameraShake(GameObject gameObject)
    {
        m_ShakeTimer = m_TimeToShake;
    }
}