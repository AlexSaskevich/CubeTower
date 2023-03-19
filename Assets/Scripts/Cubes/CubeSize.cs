public static class CubeSize
{
    public const float XS = 0.2f;
    public const float S = 0.4f;
    public const float M = 0.6f;
    public const float L = 1.0f;
    public const float XL = 1.4f;

    public static float Get(AvailableCubeSizeLevel availableCubeSizeLevel)
    {
        return availableCubeSizeLevel switch
        {
            AvailableCubeSizeLevel.XS => XS,
            AvailableCubeSizeLevel.S => S,
            AvailableCubeSizeLevel.M => M,
            AvailableCubeSizeLevel.L => L,
            AvailableCubeSizeLevel.XL => XL,
            _ => XL
        };
    }
}