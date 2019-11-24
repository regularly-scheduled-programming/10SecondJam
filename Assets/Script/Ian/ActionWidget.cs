using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionWidget : MonoBehaviour
{
    public RectTransform[] Segments;
    public int WarmUpFrames = 1;
    public int ActiveFrames = 1;
    public int CooldownFrames = 1;

    public UnityEvent OnWarmUp;
    public UnityEvent OnActive;
    public UnityEvent OnCooldown;

    [HideInInspector]
    public int framemultiplier = 30;
    // Start is called before the first frame update
    void Start()
    {
        SetSegmentWidth();
    }
    public void SetSegmentWidth()
    {
        var rect = Segments[0].rect;
        Segments[0].sizeDelta = new Vector2(WarmUpFrames * framemultiplier, rect.height);
        Segments[1].sizeDelta = new Vector2(ActiveFrames * framemultiplier, rect.height);
        Segments[2].sizeDelta = new Vector2(CooldownFrames * framemultiplier, rect.height);
    }
   
}
