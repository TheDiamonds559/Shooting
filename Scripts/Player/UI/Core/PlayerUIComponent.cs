using UnityEngine;

public abstract class PlayerUIComponent : MonoBehaviour
{
    protected PlayerUIManager _manager;
    public virtual void InitialiseComponent(PlayerUIManager playerUIManager)
    {
        _manager = playerUIManager;
        AddEvents();
    }

    protected abstract void AddEvents();
    protected abstract void RemoveEvents();

    public void UninitialiseComponent()
    {
        RemoveEvents();
    }
}
