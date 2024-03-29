using UnityEngine;

public delegate void IntEvent(int _);
public delegate void GameObjectEvent(GameObject _);

public class GlobalEvents
{
    public static IntEvent UpdateScoreEvent;
    public static IntEvent UpdatePenaltyEvent;
    public static GameObjectEvent OnExplodeEvent;
    public static GameObjectEvent OnFuseEvent;
}