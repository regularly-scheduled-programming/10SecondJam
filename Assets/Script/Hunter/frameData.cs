using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class frameVars
{
    public string Name;
    public float startup;
    public float action;
    public float cooldown;
}

[System.Serializable]
public class frameData
{
   public frameVars fastAction;
   public frameVars mediumAction;
   public frameVars slowAction;
}

