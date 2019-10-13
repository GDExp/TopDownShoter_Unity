using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore.StateMachine;

//TO DO добавить специальные возможности для анимации
public enum CharacterStateType
{
    None = 0,
    Idle = 1,
    Move = 2,
    Attack = 4,
    Hit = 8,
    Dead = 16,
}

[CreateAssetMenu(fileName = "CharacterValue", menuName ="Character/New Value", order = 100)]
public class CharacterValueSO : ScriptableObject
{
    [Header("Status Value")]    
    public int characterHealth;
    public int characterEnergy;
    [Header("Animation Value")]
    public CharacterStateType characterStateType;
    public RuntimeAnimatorController animatorController;
    public Avatar animationAvatar;
    private Dictionary<Type, string> animatorKey;


    public (int,int) GetStatusValue()
    {
        return (characterHealth, characterEnergy);
    }

    public (RuntimeAnimatorController, Avatar) GetAnimationValue()
    {
        return (animatorController, animationAvatar);
    }

    //test callback

    public void SendTypeCount()
    {
        var count = (int)(characterStateType + 1) / 2;
        while(count> 0)
        {
            Debug.Log(Enum.GetName(typeof(CharacterStateType), count));
            count /= 2;
        }
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
            //TO DO доделать!
            //{ typeof(Hit),"HitValue" },
            //{ typeof(Dead),"DeadValue" },
        };
    }
}
