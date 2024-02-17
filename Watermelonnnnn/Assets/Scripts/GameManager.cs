using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private int maxPenalty = 3;
    public int currentPenalty = 0;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void AddScore(int val)
    {
        score += val;
    }

    public void AddPenalty()
    {
        currentPenalty += 1;
        if(currentPenalty >= maxPenalty)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
