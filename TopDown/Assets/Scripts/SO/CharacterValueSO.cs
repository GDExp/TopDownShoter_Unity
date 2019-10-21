using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore.StateMachine;

[CreateAssetMenu(fileName = "CharacterValue", menuName ="Character/New Value", order = 100)]
public class CharacterValueSO : ScriptableObject
{
    [Header("Status Value")]    
    public int characterHealth;
    public int characterEnergy;
    public float characterReload;
    [Header("Animation Value")]
    public RuntimeAnimatorController animatorController;
    public Avatar animationAvatar;
    private Dictionary<Type, string> animatorKey;
   
    public (RuntimeAnimatorController, Avatar) GetAnimationValue()
    {
        return (animatorController, animationAvatar);
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