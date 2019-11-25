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
        Debug.Log(currentPlayer);
    }

    public void UpdateGridState(int gridState)
    {
        gridManager.ChangeGridState(gridState);
    }

    public void ConfirmMove()
    {
        currentPlayer.GetComponent<MovementAciton>().AddFastAction();
    }

    public void PathingPhase()
    {
        gridManager.ShowAvailableMovement((int) currentPlayer.gameObject.transform.position.x, (int) currentPlayer.gameObject.transform.position.y);
    }

    public void TargetingPhase()
    {

    }

    public void ConfirmShoot()
    {
        currentPlayer.GetComponent<bulletTargeting>().getShootLocation();
        currentPlayer.GetComponent<bulletTargeting>().AddFastAction();
    }

    public void DodgingPhase()
    {

    }
    
    public void ConfirmDodge()
    {
        currentPlayer.GetComponent<dodgeAction>().AddFastAction();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
