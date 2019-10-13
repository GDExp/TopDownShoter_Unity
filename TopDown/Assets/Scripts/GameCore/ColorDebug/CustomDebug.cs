public enum DebugColor
{
    black,
    red,
    blue,
    green,
    orange,
    white,
}

static class CustomDebug
{
    public static void LogMessage(object message, DebugColor color = DebugColor.black)
    {
        UnityEngine.Debug.Log($"<color={color.ToString()}><size=12><color=black>CUSTOM_DEBUG >>></color></size> Message - {message}.</color>");
    }
}
