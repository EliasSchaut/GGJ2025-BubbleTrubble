using UnityEngine;

public class TurretDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private TurretController _controller;
   
    private Player _player;
    
    public bool Interact(Player player)
    {
        if (_player != null)
            return false;
        
        player.SetInTurret(_controller, this);
        _player = player;
        
        return true;
    }

    public void Leave()
    {
        _player = null;
    }
}
