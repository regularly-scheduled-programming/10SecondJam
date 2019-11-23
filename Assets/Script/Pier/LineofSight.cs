using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight : MonoBehaviour
{
    public float x = 5;
    public float y = 5;

    public Vector3 topLeft;
    public Vector3 topRight;
    public Vector3 bottomLeft;
    public Vector3 bottomRight;

    public Obstacle[] obstacles;
    // Start is called before the first frame update
    void Start()
    {
        obstacles = FindObjectsOfType<Obstacle>();

        topRight = new Vector3(x / 2, y / 2, 1);
        topLeft = new Vector3(-x / 2, y / 2, 1);
        bottomRight = new Vector3(x / 2, -y / 2, 1);
        bottomLeft = new Vector3(-x / 2, -y / 2, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(x, y, 1));

        Gizmos.DrawLine(this.transform.position, topRight);
        Gizmos.DrawLine(this.transform.position, topLeft);
        Gizmos.DrawLine(this.transform.position, bottomRight);
        Gizmos.DrawLine(this.transform.position, bottomLeft);
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
