using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton: MonoBehaviour
{
    public bool WasClicked;
    public bool IsAvailable;
    public Transform SubMenu;
    public GameObject[] Cards;
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.childCount != 0)
            {
            SubMenu = this.transform.GetChild(0).transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void MakeActiveButton()
    {
        if (!WasClicked)
        {
            Debug.Log("I have been clicked");
            WasClicked = true;
            OpenSubMenu();
            foreach (var Card in Cards)
            {
                Card.GetComponent<Button>().interactable = false;
            } 
        }
        else {
            Debug.Log("I have been unclicked");
            WasClicked = false;
            CloseSubMenu();
            foreach (var Card in Cards)
            {
                if (Card.GetComponent<BaseButton>().IsAvailable)
                {
                    Card.GetComponent<Button>().interactable = true;
                    Debug.Log (Card.name + " " + IsAvailable + " has become interactable");
                }
                    
            }
        }
    }
    public void OpenSubMenu() {
        if (SubMenu)
        {
            SubMenu.gameObject.SetActive(true);
        }
    }
    public void CloseSubMenu()
    {
        if (SubMenu)
        {
            SubMenu.gameObject.SetActive(false);
        }  
    }
}
