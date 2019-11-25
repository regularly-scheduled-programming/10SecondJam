using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerCharacter : MonoBehaviour,IShootable
{
    public float health;
    public float maxHealth = 10;
    public bool invunerable;
    public enum actionState{none,Move,Shoot,Dodge,Wait,recovery,cover};
    public actionState currentAction;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shot(float damage)
    {

    }

    public bool isInvunverable()
    {
        return (invunerable);
    }
}
