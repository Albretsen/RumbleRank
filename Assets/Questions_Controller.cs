using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions_Controller : MonoBehaviour {

    public UI_Controller ui;
    public TileController tiles;

    PreMadeListsObject list;

    bool finished = false;

    // UI Elements
    public Text btnTop;
    public Text btnBottom;

	public void StartList(string s)
    {
        ui.NewPage("Questions");
        finished = false;
        switch (s)
        {
            case "Soda":
                GenerateObject(sodas);
                break;
            default:
                string[] list = s.Split(',');              
                GenerateObject(list);
                break;
        }
    }
    public void StartListAlternative(string[] s)
    {
        ui.NewPage("Questions");
        finished = false;
        GenerateObject(s);
    }

    void GenerateObject(string[] s)
    {
        list = new PreMadeListsObject(s);
        list.battles = new List<string>();
        list.score = new int[list.list.Length];

        // Generate all the battles
        for (int i = 0; i < s.Length; i++)
        {
            for (int j = i+1; j < s.Length; j++)
            {
                if(Random.Range(0f,1f) < 0.5f) { list.battles.Add(s[i] + "|" + s[j]); }
                else { list.battles.Add(s[j] + "|" + s[i]); }
            }
        }
        list.battles = Shuffle(list.battles);

        InitialCycle();
    }

    int iterator = 0;

    void InitialCycle()
    {
        iterator = 0;
        SetButtonText(list.battles[iterator]);
    }

    public void Cycle(int i)
    {
        if (!finished)
        {
            string s = "";
            if (i == 0) { s = btnTop.text; }
            if (i == 1) { s = btnBottom.text; }

            for (int j = 0; j < list.list.Length; j++)
            {
                if (list.list[j] == s)
                {
                    list.score[j]++;
                    break;
                }
            }
            iterator++;
            if (iterator > (list.battles.Count - 1))
            {
                /*Debug.Log("FINISHED NOW!!!!!!!!!");
                 Debug.Log("NEW SCORES NEW SCORES NEW SCORES NEW SCORES NEW SCORES NEW SCORES ");
                 foreach (int score in list.score)
                 {
                     Debug.Log(score);
                 }*/
                ui.NewPage("Finished");
                finished = true;
                Finished();
                return;
            }
            SetButtonText(list.battles[iterator]);
            /*Debug.Log("NEW SCORES NEW SCORES NEW SCORES NEW SCORES NEW SCORES NEW SCORES ");
            foreach (int score in list.score)
            {
                Debug.Log(score);
            }*/
        }
    }

    void Finished()
    {
        string[] s = new string[list.list.Length];
        for (int i = 0; i < list.score.Length; i++)
        {
            int highestIndex = 0;
            int highestScore = 0;
            for (int j = i; j < list.score.Length; j++)
            {
                if (list.score[j] >= highestScore)
                {
                    highestIndex = j;
                    highestScore = list.score[j];
                }
            }
            list.score[highestIndex] = list.score[i];
            list.score[i] = highestScore;
            string temp = list.list[highestIndex];
            list.list[highestIndex] = list.list[i];
            list.list[i] = temp;
        }
        int iterator = 0;
        foreach (string item in list.list)
        {
            s[iterator] = item;     
            iterator++;
        }
        list.list = s;
        tiles.Display(s);
    }

    void SetButtonText(string s)
    {
        string[] sArray = s.Split('|');
        btnTop.text = sArray[0];
        btnBottom.text = sArray[1];
    }

    // PRE MADE LISTS
    string[] sodas = { "Coca Cola", "Pepsi", "Mountain Dew", "Sprite", "Fanta" };
    public List<string> Shuffle(List<string> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
        return ts;
    }

    // TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY 

    public void InstantFinish()
    {
        StartList("Soda");
        StartCoroutine(LateStart(0.25f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        for (int i = 0; i < 10; i++)
        {
            if (Random.Range(0f, 1f) < 0.5f) { Cycle(0); }
            else { Cycle(1); }
        }
    }

    // TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY TEMPORARY 

}

public class PreMadeListsObject
{
    public PreMadeListsObject(string[] list1)
    {
        list = list1;
    }

    public string[] list { get; set; }
    public int[] score;
    public List<string> battles;

}
