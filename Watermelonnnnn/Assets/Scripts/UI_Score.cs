using UnityEngine;
using TMPro;

public class UI_Score : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Score;
    [SerializeField]
    private TMP_Text m_Penalty;

    private void Awake()
    {
        GlobalEvents.UpdateScoreEvent += UpdateScore;
        GlobalEvents.UpdatePenaltyEvent += UpdatePenalty;
        m_Score.SetText("Score: " + 0);
        m_Penalty.SetText("Penalty: " + 0);
    }

    private void OnDestroy()
    {
        GlobalEvents.UpdateScoreEvent -= UpdateScore;
        GlobalEvents.UpdatePenaltyEvent -= UpdatePenalty;
    }

    private void UpdateScore(int score)
    {
        m_Score.SetText("Score: " + score);
    }

    private void UpdatePenalty(int penalty)
    {
        m_Penalty.SetText("Penalty: " + penalty);
    }
}