using UnityEngine;

public class TurretDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private TurretController _controller;
    [SerializeField] private Laser _laser;
   
    private Player _player;
    
    public bool Interact(Player player)
    {
        if (_player != null)
            return false;
        
        player.SetInTurret(_controller, this);
        _player = player;
        
        _laser.gameObject.SetActive(true);
        
        return true;
    }

    public void Leave()
    {
        _player = null;
        _laser.gameObject.SetActive(false);
    }
}
