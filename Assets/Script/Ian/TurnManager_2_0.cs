using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager_2_0 : MonoBehaviour
{
    public playerCharacter currentPlayer;
    public GridManager gridManager;
    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void ConfirmMove()
    {
        currentPlayer.GetComponent<MovementAciton>().SetMovementList();
    }

    public void PathingPhase()
    {
        gridManager.ShowAvailableMovement((int) currentPlayer.gameObject.transform.position.x, (int) currentPlayer.gameObject.transform.position.y);
    }

    //public void
    // Update is called once per frame
    void Update()
    {
        
    }
}
