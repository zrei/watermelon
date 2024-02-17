using UnityEngine;

public abstract class SpecialComponent : MonoBehaviour
{
    protected bool m_CanPerformSpecial = true;

    public virtual void ToggleCanPerformSpecial()
    {
        m_CanPerformSpecial = !m_CanPerformSpecial;
    }
}