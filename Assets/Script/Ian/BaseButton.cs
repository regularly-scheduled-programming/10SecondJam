using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton: MonoBehaviour
{
    public bool WasClicked;
    public Transform SubMenu;
    public GameObject[] Cards;
    // Start is called before the first frame update
    void Start()
    {
        
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
                Color CurrentColor = Card.GetComponent<Image>().color;
                Card.GetComponent<Button>().interactable = false;
            } 
        }
        else {
            Debug.Log("I have been unclicked");
            WasClicked = false;
            CloseSubMenu();
            foreach (var Card in Cards)
            {
                Color CurrentColor = Card.GetComponent<Image>().color;
                Card.GetComponent<Button>().interactable = true;
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
