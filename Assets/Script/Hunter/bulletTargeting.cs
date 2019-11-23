using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTargeting : MonoBehaviour
{
    //Vector2 ShootingLocation;
    public GameObject PlaceHolder;
    [SerializeField]
    GameObject bulletSpawn;
    [SerializeField]
    float spawnOffset=.7f;

    [Header("DIFFERENT ACTION TYPES")]
    [SerializeField]
    frameData frames;
 



    // Start is called before the first frame update
    void Start()
    {
        SpawnBullet(PlaceHolder.transform.position);
        
    }

    // Update is called once per frame
   

    //Spawns a bullet with the correct trasform
    public void SpawnBullet(Vector3 targetLocation)
    {
        Vector3 direcetion = (targetLocation-transform.position).normalized;
        float rotZ = Mathf.Atan2(direcetion.y, direcetion.x) * Mathf.Rad2Deg;

        GameObject Bullet= Instantiate(bulletSpawn, transform.position+direcetion*spawnOffset,bulletSpawn.transform.rotation=Quaternion.Euler(0f,0f,rotZ)) ;
        Bullet.GetComponent<Bullet>().direction = direcetion;
    }


    //Returns Shootings Frame data
    public frameData GetFrames()
    {
        return (frames);
    }
 
    
}
