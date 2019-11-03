using UnityEditor;

public enum PlayerInputType
{
    KEY_AND_MOUSE,
    ONLY_MOUSE,
}

class PlayerSettingTools
{
    private static string _currentInputType;

    [MenuItem("Setup Settings/Input Type/Key + Mouse")]
    public static void SetKeyAndMouseInputType()
    {
        _currentInputType = PlayerInputType.KEY_AND_MOUSE.ToString();
        SetupPlayerSettings();
    }

    [MenuItem("Setup Settings/Input Type/Mouse")]
    public static void SetOnlyMouseInputType()
    {
        _currentInputType = PlayerInputType.ONLY_MOUSE.ToString();
        SetupPlayerSettings();
    }

    private static void SetupPlayerSettings()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, string.Empty);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, _currentInputType);
#if UNITY_WEBGL
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, string.Empty);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, _currentInputType);
#endif
    }

    #region CustomDebug

    [MenuItem("Setup Settings/Custom Debug/On Show")]
    private static void OnShowCustomDebugLog()
    {
        CustomDebug.isShowLog = true;
    }
    [MenuItem("Setup Settings/Custom Debug/Off Show")]
    private static void OffShowCustomDebugLog()
    {
        CustomDebug.isShowLog = false;
    }

    #endregion
}
