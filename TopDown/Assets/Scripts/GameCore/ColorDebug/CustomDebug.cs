
public enum DebugColor
{
    black,
    red,
    blue,
    green,
    orange,
    white,
}

public static class CustomDebug
{
    public static bool isShowLog = true;

    public static void LogMessage(object message, DebugColor color = DebugColor.black)
    {
        if (!isShowLog) return;
        UnityEngine.Debug.Log($"<color={color.ToString()}><size=12><color=black>CUSTOM_DEBUG >>></color></size> Message - {message}.</color>");
    }
}
