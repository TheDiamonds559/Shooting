public interface IPickUp
{
    public bool Pickup(PlayerManager playerManager, Interactable attachedObject);
    public void DropItem();
}
