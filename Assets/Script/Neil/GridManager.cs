using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static bool isInitiated = false;
    private enum GridStates{
        Default,
        ShowMove,
        ShowShoot,
        ShowDodge
    }

    private GridStates GridState;

    private Tile TileScript;
    private int player1Xcoord;
    private int player1Ycoord;
    private bool ShowWalkable = false;
    private bool ShowShootable = false;

    private bool ShowDodgeable = false;

    public GameObject[] Grid;

    public GameObject[] tempneighbors;

    public List<GameObject> movementPath;

    public Vector2 toShoot;
    
    public GameObject TM;
    public playerCharacter ActivePlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        TM = GameObject.FindGameObjectWithTag("TurnManager");
        ActivePlayer = TM.GetComponent<TurnManager_2_0>().currentPlayer;
      
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerPositions();
        UpdateGrid();
        
        if(isInitiated == false)
        {
            SetNeighbors();
            SetDodgeNeighbors();
        }
    }

    void SetNeighbors()
	{
         
		for (int i = 0; i < Grid.Length; i++) 
		{
           var tile = Grid[i].GetComponent<Tile>();
	            int tempX = 0;
			    int tempY = 0;
			if(tile.XCoord >0 && tile.XCoord < 15)
            {
               
               // MIDDLE BOARD //
                if(tile.YCoord >0 && tile.YCoord < 7)
                {
                        //initialiaze array for 8 neighbors
                        tile.neighbors = new GameObject[8];
                   
                        tempX = tile.XCoord -1;
                        tempY = tile.YCoord;                       
                        tile.neighbors[0] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord +1;
                        tempY = tile.YCoord;                       
                        tile.neighbors[1] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord ;
                        tempY = tile.YCoord -1;                       
                        tile.neighbors[2] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord;
                        tempY = tile.YCoord +1;                       
                        tile.neighbors[3] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord -1;
                        tempY = tile.YCoord +1;                       
                        tile.neighbors[4] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord -1;
                        tempY = tile.YCoord -1;                       
                        tile.neighbors[5] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord +1;
                        tempY = tile.YCoord -1;                       
                        tile.neighbors[6] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord +1;
                        tempY = tile.YCoord +1;                       
                        tile.neighbors[7] =FindByCoord(tempX,tempY);
                
		        } 
            }

            
                    // CORNERS//

                        //BOTTOM LEFT CORNER//
                        if(tile.XCoord == 0 && tile.YCoord == 0)
                        {
                            // initialize array for 3 neighbors
                            tile.neighbors = new GameObject[3];

                            tempX = tile.XCoord + 1;
                            tempY = tile.YCoord;                       
                            tile.neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord + 1;                       
                            tile.neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord + 1;
                            tempY = tile.YCoord + 1;                       
                            tile.neighbors[2] =FindByCoord(tempX,tempY);
                        }
                          //BOTTOM RIGHT CORNER//
                        if(tile.XCoord == 15 && tile.YCoord == 0)
                        {
                            // initialize array for 3 neighbors
                            tile.neighbors = new GameObject[3];

                            tempX = tile.XCoord - 1;
                            tempY = tile.YCoord;                       
                            tile.neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord +1;                       
                            tile.neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -1;
                            tempY = tile.YCoord +1;                       
                            tile.neighbors[2] =FindByCoord(tempX,tempY);
                        }
                        
                          //TOP LEFT CORNER//
                        if(tile.XCoord == 0 && tile.YCoord == 7)
                        {
                            // initialize array for 3 neighbors
                            tile.neighbors = new GameObject[3];
                            
                            tempX = tile.XCoord + 1;
                            tempY = tile.YCoord;                       
                            tile.neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +1;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[2] =FindByCoord(tempX,tempY);
                        }
                        
                          //BOTTOM RIGHT CORNER//
                        if(tile.XCoord == 15 && tile.YCoord == 7)
                        {
                            // initialize array for 3 neighbors
                            tile.neighbors = new GameObject[3];
                            
                            tempX = tile.XCoord - 1;
                            tempY = tile.YCoord;                       
                            tile.neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -1;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[2] =FindByCoord(tempX,tempY);
                        }

                        //OUTER BOARD SIDES//

                        //LEFT SIDE//
                         
                        if(tile.XCoord == 0 && tile.YCoord > 0 && tile.YCoord <7)
                        {
                            // initialize array for 5 neighbors
                            tile.neighbors = new GameObject[5];
                            
                            tempX = tile.XCoord;
                            tempY = tile.YCoord + 1;                      
                            tile.neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +1;
                            tempY = tile.YCoord ;                       
                            tile.neighbors[2] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +1;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[3] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +1;
                            tempY = tile.YCoord +1;                       
                            tile.neighbors[4] =FindByCoord(tempX,tempY);
                        }

                        //TOP SIDE//
                        if(tile.XCoord > 0 && tile.XCoord < 15 && tile.YCoord == 7)
                        {
                            // initialize array for 5 neighbors
                            tile.neighbors = new GameObject[5];
                            
                            tempX = tile.XCoord + 1;
                            tempY = tile.YCoord;                      
                            tile.neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord - 1;
                            tempY = tile.YCoord ;                       
                            tile.neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord ;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[2] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +1;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[3] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -1;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[4] =FindByCoord(tempX,tempY);
                        }


                        //RIGHT SIDE//
                         if(tile.XCoord == 15 && tile.YCoord > 0 && tile.YCoord < 7)
                        {
                            // initialize array for 5 neighbors
                            tile.neighbors = new GameObject[5];
                            
                            tempX = tile.XCoord ;
                            tempY = tile.YCoord + 1;                      
                            tile.neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord ;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -1;
                            tempY = tile.YCoord ;                       
                            tile.neighbors[2] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -1;
                            tempY = tile.YCoord +1;                       
                            tile.neighbors[3] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -1;
                            tempY = tile.YCoord -1;                       
                            tile.neighbors[4] =FindByCoord(tempX,tempY);
                        }

                        //BOTTOM SIDE//
                         if(tile.XCoord > 0 && tile.XCoord < 15 && tile.YCoord == 0)
                        {
                            // initialize array for 5 neighbors
                            tile.neighbors = new GameObject[5];
                            
                            tempX = tile.XCoord + 1;
                            tempY = tile.YCoord;                      
                            tile.neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord - 1;
                            tempY = tile.YCoord ;                       
                            tile.neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord ;
                            tempY = tile.YCoord +1;                       
                            tile.neighbors[2] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +1;
                            tempY = tile.YCoord +1;                       
                            tile.neighbors[3] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -1;
                            tempY = tile.YCoord +1;                       
                            tile.neighbors[4] =FindByCoord(tempX,tempY);
                        }
        
        }
        isInitiated = true;

    }


    void SetDodgeNeighbors()
    {
        for (int i = 0; i < Grid.Length; i++) 
		{
           var tile = Grid[i].GetComponent<Tile>();
	            int tempX = 0;
			    int tempY = 0;
			if(tile.XCoord >1 && tile.XCoord < 14)
            {
               
               // MIDDLE BOARD //
                if(tile.YCoord >1 && tile.YCoord < 6)
                {
                        //initialiaze array for 8 neighbors
                        tile.dodgeNeighbors = new GameObject[8];
                   
                        tempX = tile.XCoord -2;
                        tempY = tile.YCoord;                       
                        tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord +2;
                        tempY = tile.YCoord;                       
                        tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord ;
                        tempY = tile.YCoord -2;                       
                        tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord;
                        tempY = tile.YCoord +2;                       
                        tile.dodgeNeighbors[3] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord -2;
                        tempY = tile.YCoord +2;                       
                        tile.dodgeNeighbors[4] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord -2;
                        tempY = tile.YCoord -2;                       
                        tile.dodgeNeighbors[5] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord +2;
                        tempY = tile.YCoord -2;                       
                        tile.dodgeNeighbors[6] =FindByCoord(tempX,tempY);
                        tempX = tile.XCoord +2;
                        tempY = tile.YCoord +2;                       
                        tile.dodgeNeighbors[7] =FindByCoord(tempX,tempY);
                
		        } 
            }

            
                   

                        //OUTER BOARD SIDES//

                        //LEFT SIDE//
                         
                        if((tile.XCoord == 0 && tile.YCoord > 0 && tile.YCoord <7)
                        ||(tile.XCoord == 1 && tile.YCoord > 0 && tile.YCoord <7))
                        {
                            // initialize array for 5 neighbors
                            tile.dodgeNeighbors = new GameObject[5];
                            
                            tempX = tile.XCoord;
                            tempY = tile.YCoord + 2;                      
                            tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +2;
                            tempY = tile.YCoord ;                       
                            tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +2;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[3] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +2;
                            tempY = tile.YCoord +2;                       
                            tile.dodgeNeighbors[4] =FindByCoord(tempX,tempY);
                        }

                        //TOP SIDE//
                        if((tile.XCoord > 0 && tile.XCoord < 15 && tile.YCoord == 7)||
                        (tile.XCoord > 0 && tile.XCoord < 15 && tile.YCoord == 6))
                        {
                            // initialize array for 5 neighbors
                            tile.dodgeNeighbors = new GameObject[5];
                            
                            tempX = tile.XCoord + 2;
                            tempY = tile.YCoord;                      
                            tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord - 2;
                            tempY = tile.YCoord ;                       
                            tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord ;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +2;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[3] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -2;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[4] =FindByCoord(tempX,tempY);
                        }


                        //RIGHT SIDE//
                         if((tile.XCoord == 15 && tile.YCoord > 0 && tile.YCoord < 6)
                         ||(tile.XCoord == 14 && tile.YCoord > 0 && tile.YCoord < 6))
                        {
                            // initialize array for 5 neighbors
                            tile.dodgeNeighbors = new GameObject[5];

                            tempX = tile.XCoord ;
                            tempY = tile.YCoord + 2;                      
                            tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord ;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -2;
                            tempY = tile.YCoord ;                       
                            tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -2;
                            tempY = tile.YCoord +2;                       
                            tile.dodgeNeighbors[3] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -2;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[4] =FindByCoord(tempX,tempY);
                             
                        }

                        //BOTTOM SIDE//
                         if((tile.XCoord > 0 && tile.XCoord < 15 && tile.YCoord == 0)
                         ||(tile.XCoord > 0 && tile.XCoord < 15 && tile.YCoord == 1))
                        {
                            // initialize array for 5 neighbors
                            tile.dodgeNeighbors = new GameObject[5];
                            
                            tempX = tile.XCoord + 2;
                            tempY = tile.YCoord;                      
                            tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord - 2;
                            tempY = tile.YCoord ;                       
                            tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord ;
                            tempY = tile.YCoord +2;                       
                            tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +2;
                            tempY = tile.YCoord +2;                       
                            tile.dodgeNeighbors[3] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -2;
                            tempY = tile.YCoord +2;                       
                            tile.dodgeNeighbors[4] =FindByCoord(tempX,tempY);
                        }
                        

                         // CORNERS//

                        //BOTTOM LEFT CORNER//
                        if((tile.XCoord == 0 && tile.YCoord == 0) 
                        ||(tile.XCoord == 1 && tile.YCoord == 1))
                        {
                            // initialize array for 3 neighbors
                            tile.dodgeNeighbors = new GameObject[3];

                            tempX = tile.XCoord + 2;
                            tempY = tile.YCoord;                       
                            tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord + 2;                       
                            tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord + 2;
                            tempY = tile.YCoord + 2;                       
                            tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
                        }
                          //BOTTOM RIGHT CORNER//
                        if((tile.XCoord == 15 && tile.YCoord == 0)
                        ||(tile.XCoord == 14 && tile.YCoord == 1))
                        {
                            // initialize array for 3 neighbors
                            tile.dodgeNeighbors = new GameObject[3];

                            tempX = tile.XCoord - 2;
                            tempY = tile.YCoord;                       
                            tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord +2;                       
                            tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -2;
                            tempY = tile.YCoord +2;                       
                            tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
                        }
                        
                          //TOP LEFT CORNER//
                        if((tile.XCoord == 0 && tile.YCoord == 7)
                        || (tile.XCoord == 1 && tile.YCoord == 6))
                        {
                            // initialize array for 3 neighbors
                            tile.dodgeNeighbors = new GameObject[3];
                            
                            tempX = tile.XCoord + 2;
                            tempY = tile.YCoord;                       
                            tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord +2;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
                        }
                        
                          //TOP RIGHT CORNER//
                        if((tile.XCoord == 15 && tile.YCoord == 7)
                        ||(tile.XCoord == 14 && tile.YCoord == 6))
                        {
                            // initialize array for 3 neighbors
                            tile.dodgeNeighbors = new GameObject[3];
                            
                            tempX = tile.XCoord - 2;
                            tempY = tile.YCoord;                       
                            tile.dodgeNeighbors[0] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[1] =FindByCoord(tempX,tempY);
                            tempX = tile.XCoord -2;
                            tempY = tile.YCoord -2;                       
                            tile.dodgeNeighbors[2] =FindByCoord(tempX,tempY);
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
        player1Xcoord = (int)ActivePlayer.transform.position.x;
        player1Ycoord = (int)ActivePlayer.transform.position.y;

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
            else
            {
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
            //movementPath.Clear();
            GridState = GridStates.Default;
             ShowShootable = false;
             ShowDodgeable = false;
             ShowWalkable = false;
             ResetBoard();
            //ActivePlayer.setState(0);
            break;
            case GridStates.ShowMove:
            if(i == (int)GridState){
                GridState = GridStates.Default;
                //movementPath.Clear();
                ResetBoard();
                // not sure if this is the right state for default, hunter?
                //ActivePlayer.setState(0);
            }
            else{
                 movementPath.Clear();
                ShowAvailableMovement(player1Xcoord, player1Ycoord);
                GridState = GridStates.ShowMove;
                ActivePlayer.setState(1);
            }
            
            break;

            case GridStates.ShowShoot:
                if(i == (int)GridState){
                GridState = GridStates.Default;
                ShowShootable = false;
                ResetBoard();
                // not sure if this is the right state for default, hunter?
                //ActivePlayer.setState(0);
            }
            else{    
                ShowShootableTiles();
                GridState = GridStates.ShowShoot;
                ActivePlayer.setState(2);
            }
            break;

            case GridStates.ShowDodge:
            if(i == (int)GridState){
                GridState = GridStates.Default;
                ShowDodgeable = false;
                ResetBoard();
                //ActivePlayer.setState(0);
            }
            else{    
                ShowAvailableDodge(player1Xcoord, player1Ycoord);
                GridState = GridStates.ShowDodge;
                ActivePlayer.setState(3);
            }
            break;
        }
    }

    public void ResetBoard()
    {
        ShowWalkable = false;
        for(int i = 0; i < Grid.Length; i++)
        {
            Grid[i].GetComponent<SpriteRenderer>().color = Color.grey;
            Grid[i].GetComponent<Tile>().isActive = false;
        }
        for(int i= 0; i < movementPath.Count; i++)
        {
            movementPath[i].GetComponent<SpriteRenderer>().color = Color.blue;
        }
        
    }

    public void ShowAvailableMovement(int x, int y)
    {
        if(!ShowWalkable)
        {
            ShowWalkable = true;
            for(int i = 0; i < Grid.Length; i++)
            {
                 if(Grid[i].GetComponent<Tile>().XCoord == x 
                    && Grid[i].GetComponent<Tile>().YCoord == y)
            {
                  
                  tempneighbors = Grid[i].GetComponent<Tile>().neighbors;
                  for(int j = 0; j < tempneighbors.Length; j++)
                  {     
                      
                        if(tempneighbors[j].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                        {
                            tempneighbors[j].GetComponent<Tile>().GetComponent<SpriteRenderer>().color = Color.green;
                            tempneighbors[j].GetComponent<Tile>().isActive = true;
                        }
                  }
                  
             }
        }
    } 
 }

 public void ShowShootableTiles()
 {
     if(!ShowShootable)
     {
         ShowShootable = true;
        for(int i = 0; i < Grid.Length; i++)
        {
            if( Grid[i].GetComponent<Tile>().TileType != Tile.TileTypes.Cover)
            {
                Grid[i].GetComponent<Tile>().GetComponent<SpriteRenderer>().color = Color.green;
                Grid[i].GetComponent<Tile>().isActive = true;
            }
        }
        
     }
 }

 public void ShowAvailableDodge(int x, int y)
    {
        if(!ShowDodgeable)
        {
            ShowDodgeable = true;
            for(int i = 0; i < Grid.Length; i++)
            {
                 if(Grid[i].GetComponent<Tile>().XCoord == x 
                    && Grid[i].GetComponent<Tile>().YCoord == y)
            {
                  
                  tempneighbors = Grid[i].GetComponent<Tile>().dodgeNeighbors;
                  for(int j = 0; j < tempneighbors.Length; j++)
                  {     
                      
                        if(tempneighbors[j].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                        {
                            tempneighbors[j].GetComponent<Tile>().GetComponent<SpriteRenderer>().color = Color.green;
                            tempneighbors[j].GetComponent<Tile>().isActive = true;
                        }
                  }
                  
             }
        }
    } 
 }

    public void SelectMovementPath(int x, int y)
    {
        var state=ActivePlayer.GetComponent<playerCharacter>().currentAction;
        switch (state){

            case playerCharacter.actionState.Move:
              for(int i = 0; i < Grid.Length; i++)
            {
                if(Grid[i].GetComponent<Tile>().XCoord == x 
                    && Grid[i].GetComponent<Tile>().YCoord == y && 
                    Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable 
                    && GridState == GridStates.ShowMove)
                {                                 
                 
                        if(Grid[i].GetComponent<Tile>().isActive)
                        {
                            movementPath.Add(Grid[i]);
                             ShowWalkable = false;
                            ResetBoard();
                            ShowAvailableMovement(Grid[i].GetComponent<Tile>().XCoord, Grid[i].GetComponent<Tile>().YCoord);
                        }
                    
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
                        if(Grid[i].GetComponent<Tile>().isActive)
                        {
                            ResetBoard();
                            ShowShootable = false;
                            Debug.Log("GETS HERE");
                            toShoot= new Vector2(Grid[i].GetComponent<Tile>().XCoord,Grid[i].GetComponent<Tile>().YCoord);
                            Grid[i].GetComponent<Tile>().GetComponent<SpriteRenderer>().color = Color.blue;
                        }
                    
                    }       
            }    
            break;
        
            case playerCharacter.actionState.Dodge:
                 for(int i = 0; i < Grid.Length; i++)
                {
                    if(Grid[i].GetComponent<Tile>().XCoord == x 
                        && Grid[i].GetComponent<Tile>().YCoord == y && 
                        Grid[i].GetComponent<Tile>().TileType == Tile.TileTypes.Walkable)
                    {                                 
                         if(Grid[i].GetComponent<Tile>().isActive)
                        {
                            movementPath.Add(Grid[i]);
                             ShowDodgeable = false;
                            ResetBoard();
                           // ShowAvailableMovement(Grid[i].GetComponent<Tile>().XCoord, Grid[i].GetComponent<Tile>().YCoord);
                        }
                    }
                }
                break;
        }

        
        
    }
    
}
