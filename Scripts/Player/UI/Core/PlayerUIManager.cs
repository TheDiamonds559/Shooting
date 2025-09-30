using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [field: SerializeField] public PlayerManager PlayerManager { get; private set; }

    private List<PlayerUIComponent> _playerUIComponents = new List<PlayerUIComponent>();

    private void Start()
    {
        _playerUIComponents = GetComponentsInChildren<PlayerUIComponent>().ToList();
        foreach(PlayerUIComponent component in _playerUIComponents)
        {
            TryInitialiseComponent(component);
        }
    }

    private void TryInitialiseComponent(PlayerUIComponent component)
    {
        if (component == null) return;
        component.InitialiseComponent(this);
    }

    private void UninitialiseComponents()
    {
        foreach (PlayerUIComponent component in _playerUIComponents)
        {
            component.UninitialiseComponent();
        }
    }

    private void OnDestroy()
    {
        UninitialiseComponents();
    }
}
