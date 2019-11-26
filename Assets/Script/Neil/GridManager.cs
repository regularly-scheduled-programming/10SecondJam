﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static bool isInitiated = false;
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
    public bool ShowShootable = false;

    public bool ShowDodgeable = false;

    public GameObject[] Grid;

    public GameObject[,] grid;

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
       // player1 =  GameObject.FindGameObjectWithTag("Player");
       // player2 =  GameObject.FindGameObjectWithTag("Player2");
      // ActivePlayer = 
        //Invoke ("SetNeighbors",Time.deltaTime);
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerPositions();
        UpdateGrid();
        
        if(isInitiated == false)
        {
            SetNeighbors();
        }
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
               
               // MIDDLE BOARD //
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

            
                    // CORNERS//

                        //BOTTOM LEFT//
                        if(Grid[i].GetComponent<Tile>().XCoord == 0 && Grid[i].GetComponent<Tile>().YCoord == 0)
                        {
                           // Debug.Log("bottom left");
                            tempX = Grid[i].GetComponent<Tile>().XCoord + 1;
                            tempY = Grid[i].GetComponent<Tile>().YCoord;                       
                            Grid[i].GetComponent<Tile>().neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = Grid[i].GetComponent<Tile>().XCoord;
                            tempY = Grid[i].GetComponent<Tile>().YCoord + 1;                       
                            Grid[i].GetComponent<Tile>().neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = Grid[i].GetComponent<Tile>().XCoord + 1;
                            tempY = Grid[i].GetComponent<Tile>().YCoord + 1;                       
                            Grid[i].GetComponent<Tile>().neighbors[2] =FindByCoord(tempX,tempY);
                        }
                          //BOTTOM RIGHT//
                        if(Grid[i].GetComponent<Tile>().XCoord == 15 && Grid[i].GetComponent<Tile>().YCoord == 0)
                        {
                            tempX = Grid[i].GetComponent<Tile>().XCoord - 1;
                            tempY = Grid[i].GetComponent<Tile>().YCoord;                       
                            Grid[i].GetComponent<Tile>().neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = Grid[i].GetComponent<Tile>().XCoord;
                            tempY = Grid[i].GetComponent<Tile>().YCoord +1;                       
                            Grid[i].GetComponent<Tile>().neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = Grid[i].GetComponent<Tile>().XCoord -1;
                            tempY = Grid[i].GetComponent<Tile>().YCoord +1;                       
                            Grid[i].GetComponent<Tile>().neighbors[2] =FindByCoord(tempX,tempY);
                        }
                        
                          //TOP LEFT//
                        if(Grid[i].GetComponent<Tile>().XCoord == 0 && Grid[i].GetComponent<Tile>().YCoord == 7)
                        {
                            tempX = Grid[i].GetComponent<Tile>().XCoord + 1;
                            tempY = Grid[i].GetComponent<Tile>().YCoord;                       
                            Grid[i].GetComponent<Tile>().neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = Grid[i].GetComponent<Tile>().XCoord;
                            tempY = Grid[i].GetComponent<Tile>().YCoord -1;                       
                            Grid[i].GetComponent<Tile>().neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = Grid[i].GetComponent<Tile>().XCoord +1;
                            tempY = Grid[i].GetComponent<Tile>().YCoord -1;                       
                            Grid[i].GetComponent<Tile>().neighbors[2] =FindByCoord(tempX,tempY);
                        }
                        
                          //BOTTOM RIGHT//
                        if(Grid[i].GetComponent<Tile>().XCoord == 15 && Grid[i].GetComponent<Tile>().YCoord == 7)
                        {
                            tempX = Grid[i].GetComponent<Tile>().XCoord - 1;
                            tempY = Grid[i].GetComponent<Tile>().YCoord;                       
                            Grid[i].GetComponent<Tile>().neighbors[0] =FindByCoord(tempX,tempY);
                            tempX = Grid[i].GetComponent<Tile>().XCoord;
                            tempY = Grid[i].GetComponent<Tile>().YCoord -1;                       
                            Grid[i].GetComponent<Tile>().neighbors[1] =FindByCoord(tempX,tempY);
                            tempX = Grid[i].GetComponent<Tile>().XCoord -1;
                            tempY = Grid[i].GetComponent<Tile>().YCoord -1;                       
                            Grid[i].GetComponent<Tile>().neighbors[2] =FindByCoord(tempX,tempY);
                        }
        }
        isInitiated = true;

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

      //  player2Xcoord = (int)player2.transform.position.x;
      //  player2Ycoord = (int)player2.transform.position.y;


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
            break;
            case GridStates.ShowMove:
            if(i == (int)GridState){
                GridState = GridStates.Default;
                movementPath.Clear();
                ResetBoard();
                // not sure if this is the right state for default, hunter?
                ActivePlayer.setState(0);
            }
            else{
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
                ActivePlayer.setState(0);
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
                ActivePlayer.setState(0);
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
    public void MovePlayer()
    {

    }
}
