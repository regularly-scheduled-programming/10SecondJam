using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAciton : MonoBehaviour

   
{
    [SerializeField]
    public List<Vector2> Movementpoints = new List<Vector2>();
    int currentPoint=-1;
    bool move=false;

    public float lerplength=1;
    float currentLerpTime;

    Vector2 endposition;
    Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        NextMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            currentLerpTime += Time.deltaTime;
            float perc = currentLerpTime / lerplength;
            transform.position = Vector3.Lerp(currentPos, Movementpoints[currentPoint], perc);

            if (perc >= 1)
            {
                move = false;
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
        }

        else
        {
            move = true;
            //Tell something the movements over
            
        }
    }

    
   

}
