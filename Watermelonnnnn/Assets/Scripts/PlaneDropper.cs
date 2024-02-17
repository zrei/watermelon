using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDropper : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Fruit;
    [SerializeField]
    private Transform m_SpawnPoint;
    [SerializeField]
    private float m_SpawnCountdown = 3.0f;

    private float m_SpawnTimer;
    
    private void Awake()
    {
        ResetTimer();
    }

    // Update is called once per frame
    private void Update()
    {
        m_SpawnTimer += Time.deltaTime;
        if (m_SpawnTimer > m_SpawnCountdown)
        {
            SpawnFruit();
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        m_SpawnTimer = 0.0f;
    }

    private void SpawnFruit()
    {
        //Instantiate(m_Fruit, m_SpawnPoint.localPosition, m_SpawnPoint.localRotation);
    }

}
