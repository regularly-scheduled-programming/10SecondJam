using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public enum TileTypes{
         Default,
         PlayerOccupied,
         EnemyOccupied,
         Cover,
         Walkable,
         Visability,
         Exotic,
    }

    public TileTypes TileType;
    public int XCoord;
    public int YCoord;

    public GameObject[] neighbors;

    public GridManager gridManager;
   


    // Start is called before the first frame update
    void Start()
    {
        XCoord = (int)this.transform.position.x;
        YCoord = (int)this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            gridManager.SelectMovementPath(XCoord, YCoord);
            Debug.Log("here");
        }
    }
}
