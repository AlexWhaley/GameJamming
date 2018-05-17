using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActionComponent : ScriptableObject
{
    public virtual void ExecuteAction(Character target, Character targeter, float modifier) { }

}