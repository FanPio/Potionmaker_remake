using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TestScript : MonoBehaviour
{
    void Start(){
        StartCoroutine(WaitFuntion(1));
    }

    IEnumerator WaitFuntion(float delay){
        yield return new WaitForSeconds(delay);
        float timer = Time.time + 1.0f;

        Debug.Log("time:"+Time.time);
        Debug.Log("timer:"+timer);
    }
}

