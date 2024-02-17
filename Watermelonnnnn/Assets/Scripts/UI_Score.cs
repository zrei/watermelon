using UnityEngine;
using TMPro;

public class UI_Score : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Text;

    private void Awake()
    {
        GlobalEvents.UpdateScoreEvent += UpdateScore;
        m_Text.SetText("Score: " + 0);
    }

    private void OnDestroy()
    {
        GlobalEvents.UpdateScoreEvent -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        m_Text.SetText("Score: " + score);
    }
}