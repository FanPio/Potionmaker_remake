using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sell_button_manager : MonoBehaviour
{
    public void destroy_potion_and_button(){
        Destroy(GameObject.Find("now_display_potion"));
        Destroy(GameObject.Find("sell_button"));
    }
    public void reset_now_potion_flag(){
        potion_generator_manager.has_potion_now = false;
    }

    // destroy all material which not go to pot
    public void erase_all_falling_material(){
        // find all gameobject by tag
        UnityEngine.GameObject[] falling_materials = GameObject.FindGameObjectsWithTag("material");

        // check all material which is falling,and destroy
        foreach(UnityEngine.GameObject falling_material in falling_materials){
            bool is_fall = falling_material.GetComponent<material_manager>().is_falling;
            if(is_fall){
                Destroy(falling_material);
            }
        }
    }

    void OnMouseDown(){
        destroy_potion_and_button();
        reset_now_potion_flag();
        erase_all_falling_material();
    }
}
