using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour, ITimelineAction
{
    Vector3 originalScale;
    [Header("DIFFERENT ACTION TYPES")]
    [SerializeField]
    frameData frames;

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
                return frames.slowAction ;
                break;
        }

        return frames.mediumAction; //does not get called but needed to compile
    }


    public void OnActive()
    {
        this.transform.localScale = originalScale * 2;

    }

    public void OnCooldown()
    {
        this.transform.localScale = originalScale;

    }

    public void OnWarmUp()
    {
        originalScale = this.transform.localScale;

        this.transform.localScale = originalScale / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}