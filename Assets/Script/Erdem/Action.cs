using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "New Action", menuName = "New Action")]
public class Action : ScriptableObject
{
    [SerializeField] IntVariable turnCost;
    [SerializeField] IntVariable recoveryTurns;
    [SerializeField] public GameObjectGameEvent eventCallback;

    public int GetTurnCost()
    {
        return turnCost.Value;
    }

    public int GetRecoveryTurns()
    {
        return recoveryTurns.Value;
    }

}
