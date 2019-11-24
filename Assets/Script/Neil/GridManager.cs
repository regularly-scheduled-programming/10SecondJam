using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public enum GridStates{
        Default,
        ShowMove,
        ShowShoot,
        ShowDodge
    }

    public GridStates GridState;

    public GameObject player1;
    public Tile TileScript;
    public int player1Xcoord;
    public int player1Ycoord;

    public bool ShowWalkable = false;

    public GameObject[] Grid;

    public GameObject[,] grid;

    private static int rows = 8;
	private static int coloumns = 16;

    public GameObject[] tempneighbors;

    public List<GameObject> movementPath;

    public Vector2 toShoot;
    
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
            //Grid[i].GetComponent<SpriteRenderer>().color = Color.grey;
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

    public void ChangeGridState(int i)
    {
        switch((GridStates)i)
        {
            case GridStates.Default:
            break;
            case GridStates.ShowMove:
            if(i == (int)GridState){
                GridState = GridStates.Default;
                ShowWalkable = false;
                ResetBoard();
            }
            else{
                ShowAvailableMovement();
                GridState = GridStates.ShowMove;
            }
            
            break;
            case GridStates.ShowShoot:
            break;
            case GridStates.ShowDodge:
            break;
        }
    }

    public void ResetBoard()
    {
        for(int i = 0; i < Grid.Length; i++)
        {
            Grid[i].GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }

    public void ShowAvailableMovement()
    {
        if(!ShowWalkable)
        {
            ShowWalkable = true;
            for(int i = 0; i < Grid.Length; i++)
            {
                if(Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.PlayerOccupied)
                {
                  
                  tempneighbors = Grid[i].GetComponent<Tile>().neighbors;
                  for(int j = 0; j < tempneighbors.Length; j++)
                  {     
                      
                        if(Grid[i].GetComponent<Tile>().neighbors[j].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                        {
                            Grid[i].GetComponent<Tile>().neighbors[j].GetComponent<SpriteRenderer>().color = Color.green;

                        }    
                  }
                   
                }
            
            }
        }
        /* else
        {
            ShowWalkable = false;
            for(int i = 0; i < Grid.Length; i++)
            {
                 if(Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                {
                    Grid[i].GetComponent<SpriteRenderer>().color = Color.grey;
                }
                else
                {
                    if(Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Cover)
                    {
                        Grid[i].GetComponent<SpriteRenderer>().color = Color.grey;
                    }
                } 
            }
        } */
    }

    public void SelectMovementPath(int x, int y)
    {
        var state=player1.GetComponent<playerCharacter>().currentAction;
        switch (state){

            case playerCharacter.actionState.Move:
              for(int i = 0; i < Grid.Length; i++)
            {
                if(Grid[i].GetComponent<Tile>().XCoord == x 
                    && Grid[i].GetComponent<Tile>().YCoord == y && 
                    Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                {                                 
                    Grid[i].GetComponent<SpriteRenderer>().color = Color.blue;
                    movementPath.Add(Grid[i]);
                }
            }
            break;
            case playerCharacter.actionState.Shoot:
                 for(int i = 0; i < Grid.Length; i++)
            {
                if(Grid[i].GetComponent<Tile>().XCoord == x 
                    && Grid[i].GetComponent<Tile>().YCoord == y && 
                    Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                {                                 

                 toShoot= new Vector2(Grid[i].GetComponent<Tile>().XCoord,Grid[i].GetComponent<Tile>().YCoord);
                }
            }    
            break;
            case playerCharacter.actionState.Dodge:

            break;
        }

        
         for(int i = 0; i < Grid.Length; i++)
            {
                if(Grid[i].GetComponent<Tile>().XCoord == x 
                    && Grid[i].GetComponent<Tile>().YCoord == y && 
                    Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                {                                 
                    ResetBoard();
                    Grid[i].GetComponent<SpriteRenderer>().color = Color.blue;
                    movementPath.Add(Grid[i]);
                }
            }
    }
    public void MovePlayer()
    {

    }
}
