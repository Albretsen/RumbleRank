using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles all the main UI elements and navigation. Page specific UI elements are handled by that pages controller

public class UI_Controller : MonoBehaviour {

    public TileLayoutManager tileLayoutManager;

    Animator anim;

    string currPage = "Start";

    public void NewPage(string s)
    {
        switch (s)
        {
            case "Questions":
                anim.SetInteger("Param", 1);
                break;
            case "Finished":
                anim.SetInteger("Param", 2);
                break;
            case "Custom":
                anim.SetInteger("Param", 2);
                break;
            case "Newpage":
                anim.SetInteger("Param", 3);
                break;
        }
        currPage = s;
    }

    public void Return()
    {
        switch (currPage)
        {
            case "Questions":
                anim.SetInteger("Param", 0);
                break;
            case "Finished":
                anim.SetInteger("Param", 0);
                break;
            case "Custom":
                anim.SetInteger("Param", 0);
                break;
            case "Newpage":
                anim.SetInteger("Param", 0);
                break;
        }
        tileLayoutManager.DisplayTiles(); 
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Return(); }
    }

}
