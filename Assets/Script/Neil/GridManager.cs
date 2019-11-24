using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject player1;
    public Tile TileScript;
    public int player1Xcoord;
    public int player1Ycoord;

    public bool ShowWalkable = false;

    public GameObject[] Grid;

    public GameObject[,] grid;

    private static int rows = 8;
	private static int coloumns = 16;
    
    // Start is called before the first frame update
    void Start()
    {
        player1 =  GameObject.FindGameObjectWithTag("Player");
        Invoke ("SetNeighbors",1f);
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerPositions();
        UpdateGrid();
        
    }

    void SetNeighbors()
	{
         
		for (int i = 0; i < Grid.Length; i++) 
		{
           var tile = Grid[i].GetComponent<Tile>();
	            int tempX = 0;
			    int tempY = 0;
			if(Grid[i].GetComponent<Tile>().XCoord >0 && Grid[i].GetComponent<Tile>().XCoord < 15)
            {
               
                if(Grid[i].GetComponent<Tile>().YCoord >0 && Grid[i].GetComponent<Tile>().YCoord < 7)
                {
                   
                   
                        tempX = Grid[i].GetComponent<Tile>().XCoord -1;
                        tempY = Grid[i].GetComponent<Tile>().YCoord;                       
                        Grid[i].GetComponent<Tile>().neighbors[0] =FindByCoord(tempX,tempY);
                        tempX = Grid[i].GetComponent<Tile>().XCoord +1;
                        tempY = Grid[i].GetComponent<Tile>().YCoord;                       
                        Grid[i].GetComponent<Tile>().neighbors[1] =FindByCoord(tempX,tempY);
                        tempX = Grid[i].GetComponent<Tile>().XCoord ;
                        tempY = Grid[i].GetComponent<Tile>().YCoord -1;                       
                        Grid[i].GetComponent<Tile>().neighbors[2] =FindByCoord(tempX,tempY);
                        tempX = Grid[i].GetComponent<Tile>().XCoord;
                        tempY = Grid[i].GetComponent<Tile>().YCoord +1;                       
                        Grid[i].GetComponent<Tile>().neighbors[3] =FindByCoord(tempX,tempY);
                
		        }
                }
    }
    }

    GameObject FindByCoord(int x, int y)
    {
        for (int i = 0; i < Grid.Length; i++) 
		{
            if(Grid[i].GetComponent<Tile>().XCoord == x 
            && Grid[i].GetComponent<Tile>().YCoord == y)
            {
                return Grid[i];
            }
          
        }
          return null;
    }
	
    

    
    
    void UpdatePlayerPositions()
    {
        player1Xcoord = (int)player1.transform.position.x;
        player1Ycoord = (int)player1.transform.position.y;
    }

    void UpdateGrid()
    {
        UpdatePlayerPositionOnGrid(player1Xcoord, player1Ycoord);
    }

    void UpdatePlayerPositionOnGrid(int x, int y)
    {
        for(int i = 0; i < Grid.Length; i++)
        {
            if(Grid[i].GetComponent<Tile>().XCoord == x 
            && Grid[i].GetComponent<Tile>().YCoord == y)
            {
                Grid[i].GetComponent<Tile>().TileType =  Tile.TileTypes.PlayerOccupied;
                Grid[i].GetComponent<SpriteRenderer>().color = Color.red;
            }
                else{
                    if( Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.PlayerOccupied)
                    {
                        Grid[i].GetComponent<Tile>().TileType =  Tile.TileTypes.Walkable;
                        
                    }
            }
        }
    }

    public void ShowAvailableMovement()
    {
        if(!ShowWalkable)
        {
            ShowWalkable = true;
            for(int i = 0; i < Grid.Length; i++)
            {
                //middle check
            //    if(Grid[i].GetComponent<Tile>().XCoord >0 && Grid[i].GetComponent<Tile>().XCoord < 15 &&
            //     Grid[i].GetComponent<Tile>().YCoord >0 && Grid[i].GetComponent<Tile>().YCoord < 7)
            //     {
                    if(Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                    {
                        Grid[i].GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else
                    {
                        if(Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Cover)
                        {
                            Grid[i].GetComponent<SpriteRenderer>().color = Color.red;
                        }
                    }
             //    }
            }
        }
        else
        {
            ShowWalkable = false;
            for(int i = 0; i < Grid.Length; i++)
            {
                if(Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                {
                    Grid[i].GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f);
                }
                else
                {
                    if(Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Cover)
                    {
                        Grid[i].GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f);
                    }
                }
            }
        }
    }

    public void SelectMovementPath(int x, int y)
    {
         for(int i = 0; i < Grid.Length; i++)
            {
                if(Grid[i].GetComponent<Tile>().XCoord == x 
                    && Grid[i].GetComponent<Tile>().YCoord == y && 
                    Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                {                                 
                    Grid[i].GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
    }
    public void MovePlayer()
    {

    }
}
