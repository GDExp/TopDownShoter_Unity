using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterValueSO),true)]
public class ExtendedCharacterValueEditor : Editor
{
    private const string _healtText = "Макс. Здоровье:";
    private const string _energyText = "Макс. Энергия:";
    private const string _stateTypeText = "Состояния персонажа:";
    

    public override void OnInspectorGUI()
    {
        var value = this.target as CharacterValueSO;

        value.characterHealth = EditorGUILayout.IntSlider(_healtText, value.characterHealth, 0, 1000);
        value.characterEnergy = EditorGUILayout.IntSlider(_energyText, value.characterEnergy, 0, 1000);

        value.characterStateType = (CharacterStateType)EditorGUILayout.EnumFlagsField(_energyText, value.characterStateType);

        //переделать!
        base.OnInspectorGUI();
    }
}
