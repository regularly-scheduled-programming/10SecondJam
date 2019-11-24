using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight : MonoBehaviour
{
    public float x = 5;
    public float y = 5;
    public Vector3 center;
    public Vector3 topLeft;
    public Vector3 topRight;
    public Vector3 bottomLeft;
    public Vector3 bottomRight;
    public Bounds bounds;
    public Obstacle[] obstacles;
    public List<RayData>  rays;
    public GameObject polygon;
    Vector3 pos;

    [System.Serializable]
    public struct RayData
    {
        public Vector3 origin;
        public Vector3 end;
        public float angle;

        public RayData(Vector3 origin, Vector3 end, float angle)
        {
            this.origin = origin;
            this.end = end;
            this.angle = angle;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        obstacles = FindObjectsOfType<Obstacle>();

        topRight = new Vector3(x / 2, y / 2, 1) + center;
        topLeft = new Vector3(-x / 2, y / 2, 1) + center;
        bottomRight = new Vector3(x / 2, -y / 2, 1) + center;
        bottomLeft = new Vector3(-x / 2, -y / 2, 1) + center;
        rays = new List<RayData>();

    }
    // Update is called once per frame
    void Update()
    {
        rays.Clear();
        CheckCorner(topRight);
        CheckCorner(topLeft);
        CheckCorner(bottomLeft);
        CheckCorner(bottomRight);
        foreach (var b in obstacles)
        {
            // Gizmos.color = Color.red;
            CheckCorner(b.topRight);
            CheckCorner(b.topLeft);
            CheckCorner(b.bottomLeft);
            CheckCorner(b.bottomRight); 
        }

        sortRays();

        foreach (var r in rays)
        {
            Debug.DrawLine(this.transform.position, r.end, Color.black);

        }
        if(pos != this.transform.position)
        {
            pos = this.transform.position;
            drawTriangles();
        }
        
    }
    //public void drawTriangle(RayData ray1, RayData ray2)
    //{
    //    var point1 = ray1.end;
    //    var point2 = ray2.end;
    //    Vector2[] vertices = new Vector2[] { new Vector2(pos.x, pos.y), new Vector2(point1.x, point1.y), new Vector2(point2.x, point2.y) };
    //    ushort[] triangles = new ushort[] { 0, 1, 2 };

    //    Polygons.Add(DrawPolygon2D(vertices, triangles, Color.yellow));
    //}
    public void drawTriangles()
    { 
  
        List<Vector2> vertices = new List<Vector2>();
        List<ushort> triangles = new List<ushort>();
        int index = 0;
        var point1 = rays[0].end;
        var point2 = rays[rays.Count - 1].end;
        vertices.Add(new Vector2(pos.x, pos.y));
        vertices.Add(new Vector2(point1.x, point1.y));
        vertices.Add(new Vector2(point2.x, point2.y));

        triangles.Add(System.Convert.ToUInt16(index));
        triangles.Add(System.Convert.ToUInt16(index + 1));
        triangles.Add(System.Convert.ToUInt16(index + 2));
        index += 3;

        for (int i = 0; i < rays.Count - 1; i++)
        {
            point1 = rays[i].end;
            point2 = rays[i + 1].end;
            vertices.Add(new Vector2(pos.x, pos.y));
            vertices.Add(new Vector2(point1.x, point1.y));
            vertices.Add(new Vector2(point2.x, point2.y));

            triangles.Add(System.Convert.ToUInt16(index));
            triangles.Add(System.Convert.ToUInt16(index + 1));
            triangles.Add(System.Convert.ToUInt16(index + 2));
            index += 3;

            // Vector2[] vertices = new Vector2[] { , ,  };
            //ushort[] triangles = new ushort[] { 0, 1, 2 };


            //drawTriangle(rays[i], rays[i + 1]);

        }
     
        DrawPolygon2D(vertices.ToArray(), triangles.ToArray(), new Color(1f,0.92f,0.016f,0.5f));

    }
    public void sortRays()
    {
        RayData temp;
        for(int j = 0; j < rays.Count -1;j++)
        {
            for (int i = 0; i < rays.Count -1; i++)
            {

                if (rays[i].angle > rays[i + 1].angle)
                {
                    temp = rays[i + 1];
                    rays[i + 1] = rays[i];
                    rays[i] = temp;
                }
            }

        }

    }
    public LayerMask obstacleLayer;
    private void CheckCorner(Vector3 corner)
    {
        Ray2D ray = new Ray2D(transform.position, (corner - transform.position).normalized);
        RaycastHit2D hit =  Physics2D.Raycast(ray.origin,ray.direction,100, obstacleLayer);

        if (hit)
        {
            Debug.DrawLine(hit.point, corner, Color.red);
            Debug.DrawLine(this.transform.position, hit.point, Color.white);
            Vector3 right = ((transform.position + Vector3.right) - transform.position).normalized;
            float angle = Vector3.SignedAngle(right, ray.direction, Vector3.forward);
            rays.Add(new RayData(ray.origin, hit.point, angle));
        }
        else
        {
            Vector3 right = ((transform.position + Vector3.right) - transform.position).normalized;
            float angle = Vector3.SignedAngle(right, ray.direction, Vector3.forward);
            rays.Add(new RayData(ray.origin, ray.GetPoint(20), angle));
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, new Vector3(x, y, 1));

    }



    GameObject DrawPolygon2D(Vector2[] vertices, ushort[] triangles, Color color)
    {
        if(polygon == null)
        {
            polygon = new GameObject(); //create a new game object
        }

        SpriteRenderer sr = polygon.GetComponent<SpriteRenderer>();
        if(sr == null)
        {
            sr = polygon.AddComponent<SpriteRenderer>(); // add a sprite rendere
        }



        Texture2D texture = null; 
        if( sr.sprite != null)
        {
            texture = sr.sprite.texture;

        }
        else
        {
            Debug.Log("create");
            texture = new Texture2D(1025, 1025); // create a texture larger than your maximum polygon size
                                                 // create an array and fill the texture with your color
            List<Color> cols = new List<Color>();
            for (int i = 0; i < (texture.width * texture.height); i++)
            {
                cols.Add(color);

            }
            texture.SetPixels(cols.ToArray());
            texture.Apply();

            sr.sprite = Sprite.Create(texture, new Rect(0, 0, 1024, 1024), Vector2.zero, 1); //create a sprite with the texture we just created and colored in
            if (sr.sprite == null)
            {
                Debug.Log("ba1d");
                return polygon;

            }
        }

  
            
            //

   
        sr.color = color; //you can also add that color to the sprite renderer


        //convert coordinates to local space
        float lx = Mathf.Infinity, ly = Mathf.Infinity;
        foreach (Vector2 vi in vertices)
        {
            if (vi.x < lx)
                lx = vi.x;
            if (vi.y < ly)
                ly = vi.y;
        }
        Vector2[] localv = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            localv[i] = vertices[i] - new Vector2(lx, ly);
        }
        if(sr.sprite == null)
        {
            Debug.Log("bad");
            return polygon;

        }
        sr.sprite.OverrideGeometry(localv, triangles); // set the vertices and triangles

        polygon.transform.position = (Vector2)transform.InverseTransformPoint(transform.position) + new Vector2(lx, ly); // return to world space
       return polygon;
    }
  
}
