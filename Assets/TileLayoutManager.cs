using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TileLayoutManager : MonoBehaviour {

    public Questions_Controller questions_Controller;

    public List<string[]> list = new List<string[]>();

    public RectTransform content;
    public RectTransform childLeft;
    public RectTransform childRight;
    public GameObject fillerPrefab;

    public List<GameObject> allChildren = new List<GameObject>();

    public int tileHeight;
    public int padding;

    string path = "";

	void Start()
    {
        path = "save.dat";
        #if UNITY_ANDROID
        path = Application.persistentDataPath + "/save.dat";
        #endif

        File.Delete(path);
        if (!File.Exists(path)) { InitializeList(); }
        DisplayTiles();
    }

    public void DisplayTiles()
    {
        // Kill all children
        foreach(GameObject child in allChildren)
        {
            Destroy(child);
        }
        allChildren = new List<GameObject>();
        LoadList();
        PlaceTiles();
    }

    void PlaceTiles()
    {
        SetContentHeight();
        InstantiateTiles();
    }

    void InstantiateTiles()
    {
        int i = 0;
        foreach(string[] s in list)
        {
            if(i % 2 == 0)
            {
                allChildren.Add(Instantiate(fillerPrefab, new Vector3(0,0,0), new Quaternion(0,0,0,0), childLeft) as GameObject);
                allChildren[i].GetComponent<RectTransform>().sizeDelta = new Vector2(541.5f, allChildren[i].GetComponent<RectTransform>().sizeDelta.y);
            }
            else
            {
                allChildren.Add(Instantiate(fillerPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), childRight) as GameObject);
            }
            int temp = i;
            allChildren[i].transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { TaskWithParameters(temp); });
            i++;
        }
        i = 0;
        foreach(GameObject child in allChildren)
        {
            foreach(Transform childInner in child.transform)
            {
                if(childInner.name == "Background")
                {
                    if (list[i][list[i].Length - 1] != " ")
                    {
                        Image image = childInner.GetComponent<Image>();
                        if (list[i][list[i].Length - 1][0] == 'O')
                        {
                            image.sprite = Resources.Load<Sprite>("Images/" + list[i][0]);
                        }
                        else
                        {
                            // Sprite sprite = Resources.Load<Sprite>(list[i][list[i].Length - 1]);
                            print(list[i][list[i].Length - 1] + "HERE");
                            Sprite sprite = IMG2Sprite.instance.LoadNewSprite(list[i][list[i].Length - 1]);
                            image.sprite = sprite;
                        }
                    }
                }
                if(childInner.name == "Text")
                {
                    childInner.GetComponent<Text>().text = list[i][0];
                }
            }
            i++;
        }
    }

    public void TaskWithParameters(int i)
    {
        string[] newOne = new string[list[i].Length - 1];
        
        foreach(string s in list[i])
        {
            print("TEST TEST: " + s);
        }

        for (int j = 0; j < list[i].Length - 1; j++)
        {
            newOne[j] = list[i][j];
        }
        foreach(string s in newOne)
        {
            print(s + " GBSHFB");
        }
        questions_Controller.StartListAlternative(newOne);
    }

    void SetContentHeight()
    {
        int contentHeight = int.Parse(((float.Parse(list.Count + "") / 2) * (tileHeight + padding)) + "");
        while (contentHeight % (tileHeight + padding) != 0)
        {
            contentHeight++;
        }
        content.sizeDelta = new Vector2(0, contentHeight - padding);
    }

    // All premade lists can be found here
    void InitializeList()
    {
        List<string[]> firstList = new List<string[]>();
        firstList.Add( new string[] { "Sodas", "Coca Cola", "Pepsi", "Mountain Dew", "Sprite", "Fanta", "O" } );
        firstList.Add( new string[] { "Car brands", "Mercedes", "Ferrari", "Lamborghini", "Maserati", "Aston Martin", "Alfa Romeo", "Lexus", "O" } );
        foreach (string[] s in firstList)
        {
            list.Add(s);
        }
        SaveList();
    }

    public void SaveList()
    {
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, list);
        fs.Close();
    }

    public void LoadList()
    {
        using (Stream stream = File.Open(path, FileMode.Open))
        {
            var bformatter = new BinaryFormatter();

            list = (List<string[]>)bformatter.Deserialize(stream);
        }
    }

}
