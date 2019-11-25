using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineBehavior : MonoBehaviour
{
    [SerializeField] GameObject ActionWidgetPrefab;
    public float scrollSpeed = 1;
    public float currentXPosition = 0;
    public float minXPostion;
    public float nextActionWarmUp;
    public float nextActionActivate;
    public float nextActiobCoolDown;

    public ActionWidget[] MyActions;
    private int currentActionIndex = 0;
    private RectTransform rectTransform;
    private ContentSizeFitter contentSizeFitter;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        contentSizeFitter = GetComponent<ContentSizeFitter>();
        contentSizeFitter.enabled = false;
        Invoke("FixContentSizeFitter", Time.deltaTime);
        UpdateActionList();
    }
    [ContextMenu("UpdateActionList")]
    public void UpdateActionList()
    {
        MyActions = GetComponentsInChildren<ActionWidget>();
        minXPostion = 0;//reset max position
        //loop for all action
        foreach (var action in MyActions)
        {
            minXPostion -= action.WarmUpFrames * action.framemultiplier;
            minXPostion -= action.ActiveFrames * action.framemultiplier;
            minXPostion -= action.CooldownFrames * action.framemultiplier;
        }
        currentActionIndex = MyActions.Length - 1;

       // nextEventXPositon = 
        Time.timeScale = 1;

    }
    void FixContentSizeFitter()
    {
        contentSizeFitter.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {

        if(currentXPosition > minXPostion)
        {
            if(currentXPosition < nextActionWarmUp)
            {
                MyActions[currentActionIndex].myAction.OnWarmUp();
                Debug.Log("OnWarmUp");
                nextActionWarmUp = -Mathf.Infinity;
            }

            if ( currentXPosition < nextActionActivate)
            {
                MyActions[currentActionIndex].myAction.OnActive();
                Debug.Log("OnActive");

                nextActionActivate = -Mathf.Infinity;
            }
            currentXPosition -= scrollSpeed * Time.deltaTime;

            if (currentXPosition < nextActiobCoolDown)
            {
                MyActions[currentActionIndex].myAction.OnCooldown();
                Debug.Log("OnCooldown");

                nextActiobCoolDown = -Mathf.Infinity;
            }

            rectTransform.anchoredPosition3D = new Vector3(currentXPosition, rectTransform.anchoredPosition3D.y);

        }
        else
        {
            //////update current player here//////
            currentXPosition = minXPostion;
            Time.timeScale = 0;
        }
    }
    public void AddToTimeline(ITimelineAction test, ActionType type)
    {


        if (currentXPosition == minXPostion)
        {
            var data = test.GetFrames(type);
            GameObject o = Instantiate(ActionWidgetPrefab, this.transform.GetChild(0));
            var widget = o.GetComponent<ActionWidget>();

            widget.WarmUpFrames = data.startup;
            widget.ActiveFrames = data.action;
            widget.CooldownFrames = data.cooldown;

            widget.myAction = test;

            nextActionWarmUp = minXPostion - widget.WarmUpFrames * widget.framemultiplier;
            nextActionActivate = nextActionWarmUp - widget.ActiveFrames * widget.framemultiplier;
            nextActiobCoolDown = nextActionActivate - widget.CooldownFrames * widget.framemultiplier;

            UpdateActionList();
        }
    }
   
}
