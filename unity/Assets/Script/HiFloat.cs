//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

public class HiFloat
{
    public static float FloatEqualJudgeValue = 0.1f;//小于认为相等
    public static bool IsEqual(float f1, float f2)
    {
        return System.Math.Abs(f1 - f2) < FloatEqualJudgeValue;
    }
}
