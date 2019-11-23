using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    int nextActionEnd = 0;
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
        nextAction = action;
    }

    public bool CheckForActionDone(int currentTurn)
    {
        return currentTurn >= nextActionEnd;
    }

    public void CompleteAction()
    {
        nextAction.eventCallback.Raise(myOwner.gameObject);
    }
}
