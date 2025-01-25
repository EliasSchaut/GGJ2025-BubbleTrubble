using System;
using UnityEngine;

// enum with defined colors
[Flags]
public enum BubbleColor
{
    White = 0,
    Red = 1 << 0,
    Blue = 1 << 1,
    Yellow = 1 << 2,
    Black = 1 << 3,
    Purple = Red | Blue,
    Orange = Red | Yellow,
    Green = Blue | Yellow,
    Brown = Red | Blue | Yellow
}
