using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResourceButton : MonoBehaviour {


	public bool isScanned = true;
	public bool MaxResource;
	public bool HalfMaxResource;
	public bool QuarterMaxResource;
	public float tileValue;
	public GameObject gc;


	public int xCoord;
	public int yCoord;
	private float minValue = 312.5f;
	private float quarterValue = 1250.0f;
	private float halfValue = 2500.0f;
	private float maxValue = 5000.0f ;
	public GameObject[] neighbors = new GameObject[8];
	private Button btn;

	// Use this for initialization
	void Start () {
		tileValue = minValue;
		btn = this.GetComponent<Button> ();
//		btn.onClick.AddListener (CheckValue);
		gc = GameObject.FindWithTag("GameController");
	}
	
	// Update is called once per frame
	void Update () {
	//	UpdateColor ();
	}

	void UpdateColor()
	{
		if (MaxResource) 
		{
			tileValue = maxValue;
			if (isScanned) 
			{
				this.GetComponent<Image> ().color = Color.yellow;
			}
		}
		if (HalfMaxResource && !MaxResource)
		{
			tileValue = halfValue;
			if (isScanned)
			{
				this.GetComponent<Image> ().color = Color.red;
			}
		}
		if (QuarterMaxResource && !HalfMaxResource && !MaxResource)
		{
			tileValue = quarterValue;
			if(isScanned)
			{
				this.GetComponent<Image> ().color = Color.blue;
			}
		}

		if (!QuarterMaxResource && !HalfMaxResource && !MaxResource)
		{
			tileValue = minValue;
			if (tileValue == minValue) 
			{
				this.GetComponent<Image> ().color = Color.white;
			}
		}
	}

	 void CheckValue()
	{
		Debug.Log ("TileValue" + tileValue);
		ButtonScan ();
	//	gc.GetComponent<gameManager> ().Scan (this.gameObject);
	}


	public void lowerValue(){
		if (MaxResource) {
			MaxResource = false;
			HalfMaxResource = true;
		} else if (HalfMaxResource) {
			MaxResource = false;
			HalfMaxResource = false;
			QuarterMaxResource = true;
		} else if (QuarterMaxResource) {
			MaxResource = false;
			HalfMaxResource = false;
			QuarterMaxResource = false;
		}

	}


	void ButtonScan()
	{
		foreach(GameObject go in neighbors)
		{
			if (go == null) {
				break;
			}
			if (gc.GetComponent<gameManager> ().ScanMode == true) {
				go.GetComponent<ResourceButton> ().isScanned = true;
			} else if (gc.GetComponent<gameManager> ().ExtractMode == true) {
				//Add score
				if (go.GetComponent<ResourceButton> ().isScanned == true) {
					gc.GetComponent<gameManager> ().score += go.GetComponent<ResourceButton> ().tileValue;
					//change tile level
					go.GetComponent<ResourceButton> ().lowerValue ();
				}
			}

		}
    }



}
