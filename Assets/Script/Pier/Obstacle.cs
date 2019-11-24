using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public Vector3 topLeft;
    public Vector3 topRight;
    public Vector3 bottomLeft;
    public Vector3 bottomRight;

    BoxCollider2D box;
    public Vector2 size;
    // Start is called before the first frame update
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        GetBoxCorners();
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, size.y, 1)*2);

        //Gizmos.DrawLine(this.transform.position, topRight);
        //Gizmos.DrawLine(this.transform.position, topLeft);
        //Gizmos.DrawLine(this.transform.position, bottomRight);
        //Gizmos.DrawLine(this.transform.position, bottomLeft);

    }
    void GetBoxCorners()
    {

        Transform bcTransform = this.transform;

        // The collider's centre point in the world
        Vector3 worldPosition = bcTransform.TransformPoint(0, 0, 0);

        // The collider's local width and height, accounting for scale, divided by 2
        size = new Vector2(box.size.x * bcTransform.localScale.x * 0.5f, box.size.y * bcTransform.localScale.y * 0.5f);
        size *= 1.1f;
        // STEP 1: FIND LOCAL, UN-ROTATED CORNERS
        // Find the 4 corners of the BoxCollider2D in LOCAL space, if the BoxCollider2D had never been rotated
        Vector3 corner1 = new Vector2(-size.x, -size.y);
        Vector3 corner2 = new Vector2(-size.x, size.y);
        Vector3 corner3 = new Vector2(size.x, -size.y);
        Vector3 corner4 = new Vector2(size.x, size.y);

        // STEP 2: ROTATE CORNERS
        // Rotate those 4 corners around the centre of the collider to match its transform.rotation
        corner1 = RotatePointAroundPivot(corner1, Vector3.zero, bcTransform.eulerAngles);
        corner2 = RotatePointAroundPivot(corner2, Vector3.zero, bcTransform.eulerAngles);
        corner3 = RotatePointAroundPivot(corner3, Vector3.zero, bcTransform.eulerAngles);
        corner4 = RotatePointAroundPivot(corner4, Vector3.zero, bcTransform.eulerAngles);

        // STEP 3: FIND WORLD POSITION OF CORNERS
        // Add the 4 rotated corners above to our centre position in WORLD space - and we're done!
        corner1 = worldPosition + corner1;
        corner2 = worldPosition + corner2;
        corner3 = worldPosition + corner3;
        corner4 = worldPosition + corner4;

        bottomLeft = corner1;
        topLeft = corner2;
        bottomRight = corner3;
        topRight = corner4;
    }

    // Helper method courtesy of @aldonaletto
    // http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
    // Update is called once per frame
    void Update()
    {
        GetBoxCorners();

    }
}
