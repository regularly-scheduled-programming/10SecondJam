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
    public ActionWidget[] MyActions;
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

        //Time.timeScale = 1;

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
            currentXPosition -= scrollSpeed * Time.deltaTime;
            rectTransform.anchoredPosition3D = new Vector3(currentXPosition, rectTransform.anchoredPosition3D.y);

        }
        else
        {
            currentXPosition = minXPostion;
            //Time.timeScale = 0;
        }
    }

    public void AddAction(Action action)
    {
        GameObject o = Instantiate(ActionWidgetPrefab, this.transform.GetChild(0));    // Hardcoded. Sorry.
        o.GetComponent<ActionWidget>().WarmUpFrames = action.GetTurnCost();
        o.GetComponent<ActionWidget>().ActiveFrames = 0;    // incompatibility between behaviours. Handle later?
        o.GetComponent<ActionWidget>().CooldownFrames = action.GetRecoveryTurns();

        UpdateActionList();
    }
}
