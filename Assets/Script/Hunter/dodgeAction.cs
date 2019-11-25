using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dodgeAction : MonoBehaviour,ITimelineAction
{
    public TimelineBehavior myTimeLine;

    Vector3 originalScale;
    [Header("DIFFERENT ACTION TYPES")]
    [SerializeField]
    frameData frames;

    public UnityEvent OnWarmUpEvent;
    public UnityEvent OnActiveEvent;
    public UnityEvent OnCooldownEvent;

    public playerCharacter PC;

    //Testing IFRAMES
    Color normalColor;

    // Start is called before the first frame update
    void Start()
    {
        PC = GetComponent<playerCharacter>();

        normalColor = GetComponent<SpriteRenderer>().color;

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void setInvunerable(bool dodgeing)
    {
        if (dodgeing)
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            PC.invunerable = true;
            
        }
        else
        {
            GetComponent<SpriteRenderer>().color = normalColor;
            PC.invunerable = false;
        }
    }



    //ACTION CODE

    public frameVars GetFrames(ActionType type)
    {
        
        switch (type)
        {
            case ActionType.fastAction:

                return frames.fastAction;
                break;
            case ActionType.mediumAction:
                return frames.mediumAction;
                break;
            case ActionType.slowAction:
                return frames.slowAction;
                break;
        }

        return frames.mediumAction; //does not get called but needed to compile
    }

    public void OnWarmUp() //same here 
    {
        originalScale = this.transform.localScale;
        OnWarmUpEvent.Invoke();
        this.transform.localScale = originalScale / 2;
    }

    public void OnActive()//add a switch to have the different type of actions
    {
        this.transform.localScale = originalScale * 2;
        OnActiveEvent.Invoke();
    }

    public void OnCooldown() // see above comment
    {
        this.transform.localScale = originalScale;
        OnCooldownEvent.Invoke();
    }



    public void AddFastAction()
    {
        myTimeLine.AddToTimeline(this, ActionType.fastAction);
    }

    public void AddMediumAction()
    {
        myTimeLine.AddToTimeline(this, ActionType.mediumAction);
    }

    public void AddSlowAction()
    {
        myTimeLine.AddToTimeline(this, ActionType.slowAction);
    }
}
