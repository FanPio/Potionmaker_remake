using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGUI_manager : MonoBehaviour
{      
    private GUIStyle guiTextStyle = new GUIStyle();
    private float hSliderValue = material_generator_manager.interval;

    void OnGUI(){
        guiTextStyle.fontSize = 50;
        // guiTextStyle.normal.textColor = Color.red; // font's color
        GUI.BeginGroup(new Rect(Screen.width/2-500,Screen.height/2-500,1000,1000));
        GUI.Box(new Rect(0,0,100,100),"Material interval",guiTextStyle);
        hSliderValue = GUI.HorizontalSlider(new Rect(0,60,100,30),hSliderValue,0.1f,1f);
        GUI.EndGroup();
        

        if(GUI.changed){
            material_generator_manager.interval = hSliderValue;
            // material_generator_manager.interval = hSliderValue;
        }
    }
}
