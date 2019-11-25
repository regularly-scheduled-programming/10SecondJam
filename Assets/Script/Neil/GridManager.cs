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

    public GameObject player2;
    public Tile TileScript;
    public int player1Xcoord;
    public int player1Ycoord;
    public int player2Xcoord;
    public int player2Ycoord;
    public bool ShowWalkable = false;

    public GameObject[] Grid;

    public GameObject[,] grid;

    public GameObject[] tempneighbors;

    public List<GameObject> movementPath;

    public Vector2 toShoot;
    
    // Start is called before the first frame update
    void Start()
    {
        player1 =  GameObject.FindGameObjectWithTag("Player");
        player2 =  GameObject.FindGameObjectWithTag("Player2");
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
                        tempX = Grid[i].GetComponent<Tile>().XCoord -1;
                        tempY = Grid[i].GetComponent<Tile>().YCoord +1;                       
                        Grid[i].GetComponent<Tile>().neighbors[4] =FindByCoord(tempX,tempY);
                        tempX = Grid[i].GetComponent<Tile>().XCoord -1;
                        tempY = Grid[i].GetComponent<Tile>().YCoord -1;                       
                        Grid[i].GetComponent<Tile>().neighbors[5] =FindByCoord(tempX,tempY);
                        tempX = Grid[i].GetComponent<Tile>().XCoord +1;
                        tempY = Grid[i].GetComponent<Tile>().YCoord -1;                       
                        Grid[i].GetComponent<Tile>().neighbors[6] =FindByCoord(tempX,tempY);
                        tempX = Grid[i].GetComponent<Tile>().XCoord +1;
                        tempY = Grid[i].GetComponent<Tile>().YCoord +1;                       
                        Grid[i].GetComponent<Tile>().neighbors[7] =FindByCoord(tempX,tempY);
                
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

        player2Xcoord = (int)player2.transform.position.x;
        player2Ycoord = (int)player2.transform.position.y;


    }

    void UpdateGrid()
    {
        UpdatePlayerPositionOnGrid(player2Xcoord, player2Ycoord);
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
               //ShowWalkable = false;
                ResetBoard();
            }
            else{
                ShowAvailableMovement(player1Xcoord, player1Ycoord);
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
        ShowWalkable = false;
        for(int i = 0; i < Grid.Length; i++)
        {
            Grid[i].GetComponent<SpriteRenderer>().color = Color.grey;
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
                        else{

                        }
                  }
                   
                }
            
            }
        } 
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
                   
                }
            }
    }
    public void MovePlayer()
    {

    }
}
