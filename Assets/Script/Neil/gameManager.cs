using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gameManager: MonoBehaviour {

	private static int rows = 16;
	private static int coloumns = 16;
	private GameObject[] maxTiles = new GameObject[8];

	private GameObject[,] grid = new GameObject[rows, coloumns];
	public GameObject tilePrefab;
	public bool ScanMode = true;
	public bool ExtractMode;
	public int scans;
	public int extracts;
	public float score;
/*public GameObject modeText;
	public GameObject scanText;
	public GameObject extractText;
	public GameObject scoreText;
	public GameObject endPanel;
	public GameObject finalScoreText;*/



	private GameObject[] scannedTiles = new GameObject[9];



	// Use this for initialization
	void Start () {
//		endPanel.SetActive (false);
		CreateGamBoard ();
		//GetRandomTiles ();
	}

	void CreateGamBoard()
	{
		for (int row = 0; row < rows; row++) 
		{
			for (int col = 0; col < coloumns; col++)
			{
				GameObject newTile = Instantiate (this.tilePrefab, Vector2.zero, Quaternion.identity, this.transform) as GameObject;
				grid [row, col] = newTile;
				newTile.GetComponent<ResourceButton> ().xCoord = row;
				newTile.GetComponent<ResourceButton>().yCoord =  col;
				//Debug.Log (grid [row, col]);
			}
		}
		SetNeighbors ();
	}


	void SetNeighbors()
	{
		for (int row = 0; row < rows; row++) 
		{
			for (int col = 0; col < coloumns; col++)
			{
				int tempx = 0;
				int tempy = 0;
				//corner cases
				if (row < 1 && col < 1)
				{
					tempx = row;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					tempx = row + 1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [1] = grid [tempx, tempy];
					tempx = row + 1;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
				}
				if (row < 1 && col > 14)
				{
					tempx = row;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					tempx = row + 1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [1] = grid [tempx, tempy];
					tempx = row + 1;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
				}
				if (row > 14 && col < 1)
				{
					tempx = row;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					tempx = row - 1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [1] = grid [tempx, tempy];
					tempx = row - 1;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
				}
				if (row > 14 && col > 14)
				{
					tempx = row;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					tempx = row - 1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [1] = grid [tempx, tempy];
					tempx = row - 1;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
				}

				//topside
				if (row == 0 && col > 0 && col < 15) 
				{
					tempx = row;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					tempx = row ;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [1] = grid [tempx, tempy];
					tempx = row +1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
					tempx = row +1;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [3] = grid [tempx, tempy];
					tempx = row + 1;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [4] = grid [tempx, tempy];
				}

				// bottomside
				if (row == 15 && col > 0 && col < 15)
				{
					tempx = row;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					tempx = row ;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [1] = grid [tempx, tempy];
					tempx = row -1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
					tempx = row -1;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [3] = grid [tempx, tempy];
					tempx = row - 1;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [4] = grid [tempx, tempy];
				}

				// LeftSide
				if (col == 0 && row > 0 && row < 15)
				{
					tempx = row + 1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					tempx = row - 1 ;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [1] = grid [tempx, tempy];
					tempx = row;
					tempy = col +1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
					tempx = row -1;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [3] = grid [tempx, tempy];
					tempx = row + 1;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [4] = grid [tempx, tempy];
				}

				//RightSide
				if (col == 15 && row > 0 && row < 15)
				{
					tempx = row + 1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					tempx = row - 1 ;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [1] = grid [tempx, tempy];
					tempx = row;
					tempy = col -1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
					tempx = row -1;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [3] = grid [tempx, tempy];
					tempx = row + 1;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [4] = grid [tempx, tempy];
				}
				//full scan
				if (row > 0 && row < 15 && col > 0 && col < 15) 
				{
					tempx = row - 1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [0] = grid [tempx, tempy];
					//middle tile
					tempx = row - 1;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton>().neighbors [1] = grid [tempx, tempy];
					//Right Tile
					tempx = row - 1;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [2] = grid [tempx, tempy];
					//middle
					//Left Tile
					tempx = row;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [3] = grid [tempx, tempy];
					//Right Tile
					tempx = row;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [4] = grid [tempx, tempy];
					tempx = row + 1;
					tempy = col - 1;
					grid [row, col].GetComponent<ResourceButton>().neighbors [5] = grid [tempx, tempy];
					//Middle Tile
					tempx = row + 1;
					tempy = col;
					grid [row, col].GetComponent<ResourceButton>().neighbors [6] = grid [tempx, tempy];
					//Right Tile
					tempx = row + 1;
					tempy = col + 1;
					grid [row, col].GetComponent<ResourceButton> ().neighbors [7] = grid [tempx, tempy];
				}
			}
		}
	}


	void GetRandomTiles()
	{
		for(int i =0; i<maxTiles.Length; i++)
		{
			int x = Random.Range (2, 13);
			int y = Random.Range (2, 13);
			maxTiles[i] = grid [x, y];
			maxTiles [i].GetComponent<ResourceButton> ().MaxResource = true;
			CreateInnerBoard (x,y);
		}
	}

	void CreateInnerBoard(int x, int y)
	{
		int tempx;
		int tempy;

		// top row
		if (x > 1 && x < 14)
		{
			//top row
			//left tile
			tempx = x - 1;
			tempy = y;
			grid [tempx, tempy].GetComponent<ResourceButton> ().HalfMaxResource = true;
			//middle tile
			tempx = x - 1;
			tempy = y - 1;
			grid [tempx, tempy].GetComponent<ResourceButton> ().HalfMaxResource = true;
			//Right Tile
			tempx = x - 1;
			tempy = y + 1;
			grid [tempx, tempy].GetComponent<ResourceButton> ().HalfMaxResource = true;

			//middle row
			if (y > 1 && y < 14) 
			{
				//Left Tile
				tempx = x;
				tempy = y - 1;
				grid [tempx, tempy].GetComponent<ResourceButton> ().HalfMaxResource = true;
				//Right Tile
				tempx = x;
				tempy = y + 1;
				grid [tempx, tempy].GetComponent<ResourceButton> ().HalfMaxResource = true;
			}

			//bottom row
			//Left tile
			tempx = x + 1;
			tempy = y - 1;
			grid [tempx, tempy].GetComponent<ResourceButton> ().HalfMaxResource = true;
			//Middle Tile
			tempx = x + 1;
			tempy = y;
			grid [tempx, tempy].GetComponent<ResourceButton> ().HalfMaxResource = true;
			//Right Tile
			tempx = x + 1;
			tempy = y + 1;
			grid [tempx, tempy].GetComponent<ResourceButton> ().HalfMaxResource = true;
		}
		CreateOuterBoard (x, y);
	}

	void CreateOuterBoard(int x, int y)
	{
		int tempx;
		int tempy;
		if (x > 1 && x < 14) {
			//top row
			//left tile
			tempx = x - 2;
			tempy = y - 2;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			tempx = x - 2;
			tempy = y;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			//middle tile
			tempx = x - 2;
			tempy = y - 1;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			//Right Tile
			tempx = x - 2;
			tempy = y + 1;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			tempx = x - 2;
			tempy = y + 2;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;

			if (y > 1 && y < 14) 
			{
				//Left Tile
				tempx = x - 1;
				tempy = y - 2;
				grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
				//Right Tile
				tempx = x - 1;
				tempy = y + 2;
				grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;

				//Left Tile
				tempx = x;
				tempy = y - 2;
				grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
				//Right Tile
				tempx = x;
				tempy = y + 2;
				grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;

				//Left Tile
				tempx = x + 1;
				tempy = y - 2;
				grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
				//Right Tile
				tempx = x + 1;
				tempy = y + 2;
				grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			}
			//bottom row
			//left tile
			tempx = x + 2;
			tempy = y - 2;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			tempx = x + 2;
			tempy = y;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			//middle tile
			tempx = x + 2;
			tempy = y - 1;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			//Right Tile
			tempx = x + 2;
			tempy = y + 1;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
			tempx = x + 2;
			tempy = y + 2;
			grid [tempx, tempy].GetComponent<ResourceButton> ().QuarterMaxResource = true;
		}
	}

/*	public void Scan(GameObject tile)
	{
		
		if (ScanMode)
		{
			UseScan ();
			tile.GetComponent<ResourceButton> ().isScanned = true;
		}
		else if(ExtractMode) 
		{
			//Add score
			if (tile.GetComponent<ResourceButton> ().isScanned == true) {
				score += tile.GetComponent<ResourceButton> ().tileValue;
				//change tile level
				tile.GetComponent<ResourceButton> ().lowerValue ();
				UseExtract ();
			}
		}
	}
	

	void UseScan()
	{
		if (scans > 0)
		{
			scans -= 1;
			Debug.Log ("scans" + scans);
		}
	}

	void UseExtract()
	{
		if (extracts > 0)
		{
			extracts -= 1;
			Debug.Log ("extracts" + extracts);
		}
	}

	public void SetMode()
	{
		if (ScanMode || scans == 0) 
		{
			ScanMode = false;
			ExtractMode = true;
		} 
		else
		{
			ExtractMode = false;
			ScanMode = true;
		}

	}

	*/


	// Update is called once per frame
	void Update () {
		//UpdateUI ();
		Debug.Log (scans);
		if (scans == 0) {
			ScanMode = false;
		}
		if (extracts == 0)
		{
			ExtractMode = false;
		}
		if (scans == 0 && extracts == 0)
		{
		//	Invoke( "Restart", 1.0f );
		}

	}
/*/	void UpdateUI()
	{
		if (ScanMode) {
			modeText.GetComponent<Text>().text = "SCAN MODE";
		} else if (ExtractMode) {
			modeText.GetComponent<Text>().text = "EXTRACT MODE";
		}
		scanText.GetComponent<Text>().text = "SCANS : " + scans;
		extractText.GetComponent<Text>().text = "EXTRACTS : " + extracts;
		scoreText.GetComponent<Text>().text = "SCORE : " + score;
	}

	void Restart()
	{
		endPanel.SetActive (true);
		finalScoreText.GetComponent<Text>().text = "Final Score : " +   score;

	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene("GameScene");
		endPanel.SetActive (false);
	}*/
}
