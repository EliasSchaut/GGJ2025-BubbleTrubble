using UnityEngine;

public class TurretDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private TurretController _controller;
   
    private Player _player;
    
    public void Interact(Player player)
    {
        if (_player != null)
            return;
        
        player.SetInTurret(_controller, this);
        _player = player;
    }

    public void Leave()
    {
        _player = null;
    }
}
