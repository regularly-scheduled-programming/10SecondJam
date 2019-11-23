using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight : MonoBehaviour
{
    public float x = 5;
    public float y = 5;
    public Obstacle[] obstacles;
    // Start is called before the first frame update
    void Start()
    {
        obstacles = FindObjectsOfType<Obstacle>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(x, y, 1));

        Gizmos.DrawLine(this.transform.position, new Vector3(x/2, y/2, 1));
        Gizmos.DrawLine(this.transform.position, new Vector3(-x/2, y/2, 1));
        Gizmos.DrawLine(this.transform.position, new Vector3(x/2, -y/2, 1));
        Gizmos.DrawLine(this.transform.position, new Vector3(-x/2, -y/2, 1));
        foreach (var b in obstacles)
        {
           

            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, b.topLeft);

            Gizmos.DrawLine(this.transform.position, b.topLeft);
            Gizmos.DrawLine(this.transform.position, b.topRight);
            Gizmos.color = Color.blue ;

            Gizmos.DrawLine(this.transform.position, b.bottomLeft);
            Gizmos.DrawLine(this.transform.position, b.bottomRight);

        }

    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
