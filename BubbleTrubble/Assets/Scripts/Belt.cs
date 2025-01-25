using UnityEngine;

public enum BeltType
{
    Straight,
    LeftTurn,
    RightTurn,
}

public class Belt : MonoBehaviour
{
    public BeltType beltType { get; set; } = BeltType.Straight;
}
