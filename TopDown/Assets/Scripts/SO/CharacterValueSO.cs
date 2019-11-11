using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore.StateMachine;
using Character;

[CreateAssetMenu(fileName = "CharacterValue", menuName = "CustomSO/Character/New Value", order = 100)]
public class CharacterValueSO : ScriptableObject
{
    public const float ConstSpeedCharacter = 10f;

    [Header("Animation Value")]
    public RuntimeAnimatorController animatorController;
    public Avatar animationAvatar;
    private Dictionary<Type, string> animatorKey;

    [Header("Status Value")]
    public int characterHealth;
    public int characterEnergy;
    public float characterReload;
    public float characterSpeed;

    [Header("Speed Moditificator Value")]
    public float normalModificator;
    public float runModificator;
    public float extraRunModifictaor;


    public bool isCheat;//test

    public (RuntimeAnimatorController, Avatar) GetAnimationValue()
    {
        return (animatorController, animationAvatar);
    }

    public Dictionary<SpeedStatus, float> GetChracterSpeedByStatus()
    {
        Dictionary<SpeedStatus, float> dictionary = new Dictionary<SpeedStatus, float>
        {
            { SpeedStatus.NormalSpeed, characterSpeed },
            { SpeedStatus.RunSpeed, characterSpeed * 2f },
            { SpeedStatus.ExtraRunSpeed, characterSpeed * 3f },
        };
        return dictionary;
    }

    public Dictionary<SpeedStatus, float> GetSpeedModificator()
    {
        Dictionary<SpeedStatus, float> dictionary = new Dictionary<SpeedStatus, float>
        {
            { SpeedStatus.NormalSpeed, normalModificator },
            { SpeedStatus.RunSpeed, runModificator },
            { SpeedStatus.ExtraRunSpeed, extraRunModifictaor },
        };
        return dictionary;
    }

    public Dictionary<Type, string> GetAnimationKeys()
    {
        CreateDefaultAnimationKey();
        return animatorKey;
    }

    private void CreateDefaultAnimationKey()
    {
        animatorKey = new Dictionary<Type, string>
        {
            { typeof(Idle), "IdleValue" },
            { typeof(Move), "MoveValue" },
            { typeof(Attack), "AttackValue" },
            { typeof(Dead), "DeadValue" },
        };
    }
}