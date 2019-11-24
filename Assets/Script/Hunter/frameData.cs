using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class frameVars
{
    public string Name;
    public int startup;
    public int action;
    public int cooldown;
    public int damage;
}

[System.Serializable]
public class frameData
{
   public frameVars fastAction;
   public frameVars mediumAction;
   public frameVars slowAction;
}
public enum ActionType
{
    fastAction, mediumAction,slowAction
}
