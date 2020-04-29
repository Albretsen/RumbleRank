using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomController : MonoBehaviour {

    public UI_Controller ui;
    public Questions_Controller questions;
    public GameObject panel;

    public InputField inputField;
    public Text textListDisplay;
    public TileLayoutManager tileLayoutManager;
    public Text inputFieldText;

    string s = "";
    string sDisplay = "";
    int i = 0;

	public void StartCustom()
    {
        panel.SetActive(false);
        ui.NewPage("Custom");
        inputFieldText.text = "List name";
        s = "";
        sDisplay = "";
        i = 0;
    }

    public void AddWord()
    {
        i++;
        if (inputField.text == "") { Debug.Log("Enter a word"); return; }
        if(s == "") { s = inputField.text; sDisplay = inputField.text; }
        else { s += "," + inputField.text; sDisplay += ", " + inputField.text; }
        inputField.text = "";
        inputFieldText.text = "Item " + i;
        textListDisplay.text = sDisplay;
    }

    public void StartGame(int i)
    {
        if(i == 2) { panel.SetActive(true); return; }
        if (s.Split(',').Length > 1)
        {
            if(i == 1)
            {
                NativeGallery.GetImageFromGallery(Callback, "Pick an image", "image/*", -1);
                i = 0;
            }
            else
            {
                i = 0;
                s += ", ";
                print(s);
                tileLayoutManager.list.Add(s.Split(','));
                /*foreach (string[] s in tileLayoutManager.list)
                {
                    foreach(string st in s)
                    {
                        print(st);
                    }
                }*/
                tileLayoutManager.SaveList();
                ui.Return();
            }
            return;
        }
        Debug.Log("Not enough words");
    }

    public void Callback(string result)
    {
        if(result != "") { s += "," + result; }
        else { s += ", "; }
        tileLayoutManager.list.Add(s.Split(','));
        tileLayoutManager.SaveList();
        ui.Return();
    }

}
