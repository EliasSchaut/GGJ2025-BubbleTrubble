using UnityEngine;

public class Belt : MonoBehaviour
{
    void Update()
    {
        IBelt self = this;
        self.BeldUpdate();
    }

    bool IBelt.IsOccupied { get; set; } = false;
    IBelt IBelt.NextBelt { get; set; }
    GameObject IBelt.Bubble { get; set; }
    float IBelt.MovementSpeed { get; set; } = 1.0f;
    Vector2 IBelt.MovementDirection { get; set; }
    Vector2 IBelt.Position => transform.position;
}