using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button m_StartButton;

    private void Awake()
    {
        m_StartButton.onClick.AddListener(StartGame);
    }

    private void OnDestroy()
    {
        m_StartButton.onClick.RemoveAllListeners();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}