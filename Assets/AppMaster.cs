using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMaster : MonoBehaviour {

    public bool instantFinish;
    public Questions_Controller questions;

    void Start()
    {
        //if (instantFinish) { questions.InstantFinish(); instantFinish = false; Debug.Log("Test"); }
        StartCoroutine(LateStart(0.25f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        if (instantFinish) { questions.InstantFinish(); instantFinish = false; Debug.Log("Test"); }
    }
}
