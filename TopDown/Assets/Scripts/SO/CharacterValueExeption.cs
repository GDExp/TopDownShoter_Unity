using UnityEngine;

public interface IExeptionCharacterValue
{
    CharacterValueSO ChoseDefaultValue();
}

class CharacterValueExeption : IExeptionCharacterValue
{
    private const string pathDefaultCharacterValue = "CharacterValue/DefaultValue";


    public CharacterValueSO ChoseDefaultValue()
    {
        var defaultValue = Resources.Load<CharacterValueSO>(pathDefaultCharacterValue);
        CustomDebug.LogMessage("Set Default Value", DebugColor.blue);
        CustomDebug.LogMessage(defaultValue);
        return defaultValue;
    }

}
