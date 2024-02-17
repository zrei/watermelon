using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button m_RestartButton;

    private void Awake()
    {
        m_RestartButton.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
        m_RestartButton.onClick.RemoveAllListeners();
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}