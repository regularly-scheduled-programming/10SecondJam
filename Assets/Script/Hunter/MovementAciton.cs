using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementAciton : MonoBehaviour,ITimelineAction

   
{
    [SerializeField]
    public List<GameObject> Movementpoints = new List<GameObject>();
    int currentPoint=-1;
    public bool move=false;

    public float lerplength=1;
    float currentLerpTime;
    float timeStarted;

    Vector2 endposition;
    Vector2 currentPos;

    //ActionVariables
    public TimelineBehavior myTimeLine;

    Vector3 originalScale;
    [Header("DIFFERENT ACTION TYPES")]
    [SerializeField]
    frameData frames;

    public UnityEvent OnWarmUpEvent;
    public UnityEvent OnActiveEvent;
    public UnityEvent OnCooldownEvent;

    // Start is called before the first frame update
    void Start()
    {
        //NextMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            currentLerpTime += Time.time-timeStarted;
            //currentLerpTime += Time.deltaTime;
            float perc = currentLerpTime / lerplength;
            transform.position = Vector3.Lerp(currentPos, new Vector2(Movementpoints[currentPoint].GetComponent<Tile>(). XCoord,Movementpoints[currentPoint].GetComponent<Tile>().YCoord), perc);

            if (perc >= 1)
            {
                //move = false;
                currentLerpTime = 0;
                NextMovement();
            }
        }
    }
    



    public void NextMovement()
    {
        currentPoint++;
        currentPos = transform.position;
        if (currentPoint > Movementpoints.Count-1)
        {
            move = false;
            currentPoint = -1;
            clearpaths();
        }

        else
        {
            timeStarted = Time.time;
            move = true;
            //WILL NEED TO WRITE CODE TO INCREASE LERP LENGTH BASED ON MOVEMENT DISTANCE

            //lerplength=Movementpoints.Count
            //Tell something the movements over
            
        }
    }

    public void SetMovementList(){
       Movementpoints=FindObjectOfType<GridManager>().movementPath;
       frames.fastAction.action = (int)0.5f*(Movementpoints.Count + 1);
       
       //NextMovement();

    }
   
   public void clearpaths(){
        Movementpoints.Clear();
        FindObjectOfType<GridManager>().movementPath.Clear();
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
