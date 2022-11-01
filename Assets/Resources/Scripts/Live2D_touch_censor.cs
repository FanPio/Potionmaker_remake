using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Live2D_touch_censor : MonoBehaviour
{
    public Animator animator;

    UnityEvent Onclick = new UnityEvent();

    void Start(){
        Onclick.AddListener(playanimation);
    }

    void OnMouseDown(){
        Onclick.Invoke();
    }

    public void playanimation(){
        animator.SetInteger("Live2D_animation_id",2);
        StartCoroutine(DelayReset());
    }

    IEnumerator DelayReset(){
        yield return new WaitForEndOfFrame();
        animator.SetInteger("Live2D_animation_id",0);
    }
}
