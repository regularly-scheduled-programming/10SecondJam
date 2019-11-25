using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [HideInInspector]
   public Vector3 direction;
   [SerializeField]
   float speed=20f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        moveBullet();
    }
    public void moveBullet()
    {
        //transform.position=transform.position + direction * speed * Time.deltaTime;

        GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x,direction.y)*speed - GetComponent<Rigidbody2D>().velocity);
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.GetComponent<IShootable>()!=null)
        {
            IShootable shootObject = collision.gameObject.GetComponent<IShootable>();
            if (!shootObject.isInvunverable())
            {
                collision.gameObject.GetComponent<IShootable>().shot(0);
                Destroy(gameObject);
            }
           
            
        }

        else
        {
            Destroy(gameObject);
        }
        
    }
}
