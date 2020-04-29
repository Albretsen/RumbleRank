using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour {

    public GameObject fillHerUpPrefab;
    public Image topImage;
    public Image bottomImage;

    public int height;
    public int width;
    public float speed;

    float contentHeight = 0;

    Color blue = new Color(0, Remap(112, 0, 255, 0, 1), Remap(192, 0, 255, 0, 1));
    Color red = new Color(Remap(192, 0, 255, 0, 1), 0, 0);

    GameObject parent;

    List<GameObject> cards = new List<GameObject>();

    public void Display(string[] s)
    {
        animate = true;
        parent = GameObject.Find("PARENT");
        contentHeight = 0;
        foreach (GameObject i in cards)
        {
            Destroy(i);
        }
        cards.Clear();

        topImage.color = red;
        if((s.Length - 1) % 2 == 0)
        {
            bottomImage.color = red;
        }
        else
        {
            bottomImage.color = blue;
        }

        StartCoroutine(Example(s));
    }

    IEnumerator Example(string[] s)
    {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i < s.Length; i++)
        {
            InstantiateCard(i, s);
        }
        RectTransform rectTransform = GameObject.Find("Content").GetComponent<RectTransform>();
        if(contentHeight < 1920) { contentHeight = 1920; }
        rectTransform.sizeDelta = new Vector2(0, contentHeight);
    }

    void InstantiateCard(int i, string[] s)
    {
        cards.Add(new GameObject("Card number " + i, typeof(RectTransform)));
        cards[i].transform.SetParent(parent.transform, false);

        float scaler = (Remap((s.Length - i) * (s.Length - i), 0, (s.Length - 1) * (s.Length * 2), 1, 3f));

        RectTransform rectTransform = cards[i].GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, height * scaler);

        contentHeight += height * scaler;

        GameObject filler = Instantiate(fillHerUpPrefab) as GameObject;
        filler.transform.SetParent(cards[i].transform, false);
        filler.transform.position = new Vector3(1080, filler.transform.position.y, filler.transform.position.z);
        AnimateObject(filler, true);

        Text txt = filler.transform.GetChild(1).GetComponent<Text>();
        txt.text = s[i];
        txt.color = new Color(255, 255, 255);

        Image image = filler.transform.GetChild(0).GetComponent<Image>();

        if (i%2 == 0)
        { 
            image.color = blue;
        }
        else
        {
            image.color = red;
        }

        filler.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        filler.transform.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

        filler.transform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        filler.transform.GetComponent<RectTransform>().offsetMax = new Vector2(1, 1);
    }

    bool animate = false;
    List<GameObject> gameObjects = new List<GameObject>();
    void AnimateObject(GameObject gameObject, bool newObject)
    {
        if(newObject) { gameObjects.Add(gameObject); }
        foreach(GameObject s in gameObjects)
        {
            s.transform.position = new Vector3(s.transform.position.x - speed,0,0);
            if(s.transform.position.x < 1)
            {
                gameObjects.Remove(s);
            }
        }
        if(gameObjects.Count < 1)
        {
            animate = false;
        }
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void Update()
    {
        GameObject temp = new GameObject();
        if (animate) { AnimateObject(temp,false); }
        Destroy(temp);
    }
}
