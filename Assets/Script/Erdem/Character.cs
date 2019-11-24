using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class Character : MonoBehaviour
{
    [SerializeField] IntVariable maxHealth;
    int currentHealth;
    TimeLine myTimeLine;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void AssignTimeLine(TimeLine timeLine)
    {
        myTimeLine = timeLine;
    }

    public TimeLine GetTimeLine()
    {
        return myTimeLine;
    }

    public void TakeDamage(int amount = 1)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Debug.Log("I am dead");
        }
    }

}
