using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTargeting : MonoBehaviour
{
    //Vector2 ShootingLocation;
    public GameObject PlaceHolder;
    [SerializeField]
    GameObject bulletSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   


    public void SpawnBullet(Vector3 targetLocation)
    {
        Vector2 rawDirection = targetLocation - transform.position;

        Vector2 direcetion = rawDirection / rawDirection.magnitude;

       // Instantiate()



        //GameObject Bullet= Instantiate()
    }
}
