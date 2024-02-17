using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this should be a singleton but I'll do that... later
public class ScoreManager : MonoBehaviour
{
    private int m_Score;

    private void Awake()
    {
        ResetScore();
    }

    public void UpdateScore(int changeAmount)
    {
        m_Score += changeAmount;
        GlobalEvents.UpdateScoreEvent?.Invoke(m_Score);
    }

    private void ResetScore()
    {
        m_Score = 0;
        GlobalEvents.UpdateScoreEvent?.Invoke(m_Score);
    }

}
