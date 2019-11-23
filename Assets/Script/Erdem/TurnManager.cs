using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    float timeSinceStart = 0f;
    int nextCheck = 0;
    [SerializeField] List<Character> characters = new List<Character>();
    List<TimeLine> timelines = new List<TimeLine>();

    Character charToAct;
    List<TimeLine> timeLinesThatNeedActions = new List<TimeLine>();

    enum GameState
    {
        WaitingForSelection,
        Playing
    }

    GameState gameState = GameState.WaitingForSelection;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Character character in characters)
        {
            TimeLine t = new TimeLine(character);
            timelines.Add(t);
            character.AssignTimeLine(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Playing)
        {
            Time.timeScale = 1f;

            timeSinceStart += Time.deltaTime;

            if(timeSinceStart >= nextCheck)
            {
                CheckForCompletedActions();
            }
        }
    }

    void CheckForCompletedActions()
    {
        timeLinesThatNeedActions = new List<TimeLine>();   // Will this be ok with the GarbaceCollector? Tune in after the break to find out!

        foreach (TimeLine timeLine in timelines)
        {
            if (timeLine.CheckForActionDone(nextCheck))
            {
                timeLinesThatNeedActions.Add(timeLine);
            }
        }

        if(timeLinesThatNeedActions.Count > 0)
        {
            Time.timeScale = 0;
            charToAct = timeLinesThatNeedActions[Random.Range(0, timeLinesThatNeedActions.Count)].GetOwner(); // No ionitiative roll, just pick randomly for now
            gameState = GameState.WaitingForSelection;
        }
    }


    public void SetCharacterAction(Action action)
    {
        charToAct.GetTimeLine().AddAction(action);
        timeLinesThatNeedActions.Remove(charToAct.GetTimeLine());

        if(timeLinesThatNeedActions.Count > 0)
        {
            charToAct = timeLinesThatNeedActions[Random.Range(0, timeLinesThatNeedActions.Count)].GetOwner(); // No ionitiative roll, just pick randomly for now
            //Wait for command for the new guy
        }
        else
        {   // Everyone has orders, keep playing
            Time.timeScale = 0;
        }

    }
}
