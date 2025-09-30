using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "World/Item Data")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Texture2D ItemImage { get; private set; }

    [field: SerializeField] public bool IsHoldable { get; private set; }
    [field: SerializeField] public Vector3 HoldingRotation { get; private set; }
}
