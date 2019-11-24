using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine
{
    int nextActionEnd = 0;
    int nextActionRecovery = 0;
    Action nextAction;

    Character myOwner;

    public TimeLine(Character owner)
    {
        myOwner = owner;
    }

    public Character GetOwner()
    {
        return myOwner;
    }

    //public void SetOwner(Character character)
    //{
    //    myOwner = character;
    //}

    public void AddAction(Action action)
    {
        nextActionEnd += action.GetTurnCost();
        nextActionRecovery = nextActionEnd + action.GetRecoveryTurns();
        nextAction = action;
    }

    public bool CheckForActionDone(int currentTurn)
    {
        return currentTurn >= nextActionEnd;
    }

    public bool CheckIfNewActionNeeded(int currentTurn)
    {
        return currentTurn >= nextActionRecovery;
    }

    public void CompleteAction()
    {
        if(nextAction != null && nextAction.eventCallback != null)
        {
            nextAction.eventCallback.Raise(myOwner.gameObject);
        }
    }
}
