using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class bulletTargeting : MonoBehaviour,ITimelineAction
{
    //Vector2 ShootingLocation;
    //public GameObject PlaceHolder;
    [SerializeField]
    GameObject bulletSpawn;
    [SerializeField]
    float spawnOffset=.7f;

   

    public Vector3 ShootingLocation;
    //Action Variables
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
        //Debug
        //SpawnBullet(PlaceHolder.transform.position);

        
    }

    // Update is called once per frame
   

    //Spawns a bullet with the correct trasform
    public void SpawnBullet()
    {
        Vector3 direcetion = (ShootingLocation-transform.position).normalized;
        float rotZ = Mathf.Atan2(direcetion.y, direcetion.x) * Mathf.Rad2Deg;

        GameObject Bullet= Instantiate(bulletSpawn, transform.position+direcetion*spawnOffset,bulletSpawn.transform.rotation=Quaternion.Euler(0f,0f,rotZ)) ;
        Bullet.GetComponent<Bullet>().direction = direcetion;
    }


    //Returns Shootings Frame data
    public frameData GetFrames()
    {
        return (frames);
    }
 
    
    public void getShootLocation(){
       ShootingLocation=new Vector3(FindObjectOfType<GridManager>().toShoot.x,FindObjectOfType<GridManager>().toShoot.y,0);
       //SpawnBullet(ShootingLocation);
    }

    //Action Code

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
