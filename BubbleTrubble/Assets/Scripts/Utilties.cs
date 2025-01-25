public static class Utilties
{
    public static float NormalizeAngle(float angle)
    {
        return (angle + 180) % 360 - 180;
    }
}
