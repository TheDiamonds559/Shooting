using UnityEngine;

public abstract class PlayerComponent : MonoBehaviour
{
    protected PlayerManager _playerManager;
    public virtual void InitialiseComponent(PlayerManager playerManager)
    {
        _playerManager = playerManager;
        AddEvents();
    }

    protected abstract void AddEvents();
    protected abstract void RemoveEvents();

    public void UninitialiseComponent()
    {
        RemoveEvents();
        Destroy(this);
    }
}
