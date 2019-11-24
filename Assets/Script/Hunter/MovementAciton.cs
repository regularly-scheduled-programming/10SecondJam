using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAciton : MonoBehaviour

   
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
            //Tell something the movements over
            
        }
    }

    public void SetMovementList(){
       Movementpoints=FindObjectOfType<GridManager>().movementPath;
       NextMovement();

    }
   
   public void clearpaths(){
        Movementpoints.Clear();
        FindObjectOfType<GridManager>().movementPath.Clear();
   } 

}
