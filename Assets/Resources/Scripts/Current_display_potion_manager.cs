using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Text.RegularExpressions;
public class Potion_sprite_manager{
    public Sprite[] Mana_potion_list;
    public Sprite[] Life_potion_list;
    public Sprite[] Stamina_potion_list;
    public void load_all_material(){
        Mana_potion_list = Resources.LoadAll<Sprite>("Spirits/potion/Mana");
        Life_potion_list = Resources.LoadAll<Sprite>("Spirits/potion/Life");
        Stamina_potion_list = Resources.LoadAll<Sprite>("Spirits/potion/Stamina");
        
        // fucked up code
        // Debug.Log("Mana");
        // resort_material_list_by_index(Mana_potion_list);
        // Debug.Log("Life");
        // resort_material_list_by_index(Life_potion_list);
        // Debug.Log("Stamina");
        // resort_material_list_by_index(Stamina_potion_list);

        // for test
        // foreach(Object ob in Mana_potion_list){
        //     Debug.Log(ob.name);
        //     Debug.Log(ob.GetType());
        // }
    }

    // fucked up code
    // public Sprite[] resort_material_list_by_index(Sprite[] input_sprites_data){
    //     Sprite[] output_sprites_data = input_sprites_data;

    //     for(int i=0;i<input_sprites_data.Length-1;i++){
    //         Match real_index = Regex.Match(output_sprites_data[i].name,@"\d+$");

    //         Sprite sprite_tem = output_sprites_data[i];
    //         output_sprites_data[i] = output_sprites_data[int.Parse(real_index.ToString())-1];
    //         output_sprites_data[int.Parse(real_index.ToString())] = sprite_tem;
    //     }

    //     foreach(Sprite i in output_sprites_data){
    //         Debug.Log(i.name);
    //     }
        
    //     return output_sprites_data;
    // }

    public Sprite find_sprite_by_index(Sprite[] input_datas,int index){
        Sprite output = input_datas[0];
        foreach(Sprite input_data in input_datas){
            Match object_match = Regex.Match(input_data.name,@"\d+$");

            if(int.Parse(object_match.ToString()) == index){
                output = input_data;
                return output;
            }
        }
        return null;
    }

}

public class potion_generator_manager{
    // the flag about now display potion
    public static bool has_potion_now = false;

    // generate going display potion by name and index
    // potion sprite finded by index
    public void generate_current_potion_by_name(string Potion_name,int Potion_index,Sprite[] Potion_sprite_data){
        // create potion
        GameObject go = new GameObject("now_display_potion");
        
        // add Sprite component
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        Debug.Log("Potion_index:"+Potion_index);

        // apply and find sprite
        Potion_sprite_manager potion_Sprite_Manager = new Potion_sprite_manager();
        renderer.sprite = potion_Sprite_Manager.find_sprite_by_index(Potion_sprite_data,Potion_index);
        // renderer.sprite = Potion_sprite_data[Potion_index];

        // potion position
        Camera camera = Camera.main;
        float potion_x_potision;
        float potion_y_potision;
        potion_x_potision = -camera.orthographicSize * camera.aspect + renderer.size.x/2;
        potion_y_potision = -camera.orthographicSize + renderer.size.y/1.6f;

        go.transform.position = new Vector3(potion_x_potision,potion_y_potision,0);
    }

    public void generate_sell_potion_button(){
        // load button spirits
        Sprite sell_button = Resources.Load<Sprite>("Spirits/UI/sell_button");

        // Debug.Log(sell_button.name);

        // create button
        GameObject go = new GameObject("sell_button");

        // apply sprites
        SpriteRenderer spriterenderer = go.AddComponent<SpriteRenderer>();
        spriterenderer.sprite = sell_button;

        //circulate position by camera
        Camera camera = Camera.main;
        float potion_x_potision;
        float potion_y_potision;
        potion_x_potision = -camera.orthographicSize * camera.aspect + spriterenderer.size.x/10;
        potion_y_potision = -camera.orthographicSize * 0.45f;

        // button position
        go.transform.position = new Vector3(potion_x_potision,potion_y_potision,0);
        
        // button scale
        go.transform.localScale = new Vector3(0.2f,0.2f,1);

        // add collider for script
        go.AddComponent<BoxCollider2D>();

        // add script
        go.AddComponent<sell_button_manager>();
    }
    
    
    // identify what will going display potion type
    public void generate_current_potion_manager(int index,string material_name){
        Potion_sprite_manager load_Potion_Sprite;
        load_Potion_Sprite = new Potion_sprite_manager();
        load_Potion_Sprite.load_all_material(); // load potion sprite

        // compare material name to control
        switch(material_name){
            case "Mana":
                if(!has_potion_now){
                    has_potion_now = true;
                    generate_current_potion_by_name(material_name,index,load_Potion_Sprite.Mana_potion_list);
                    generate_sell_potion_button();
                }
                break;
            case "Life":
                if(!has_potion_now){
                    has_potion_now = true;
                    generate_current_potion_by_name(material_name,index,load_Potion_Sprite.Life_potion_list);
                    generate_sell_potion_button();
                }
                break;
            case "Stamina":
                if(!has_potion_now){
                    has_potion_now = true;
                    generate_current_potion_by_name(material_name,index,load_Potion_Sprite.Stamina_potion_list);
                    generate_sell_potion_button();
                }
                break;

        }

    }
}


public class Current_display_potion_manager : MonoBehaviour
{
    // public load_Potion_sprite load_Potion_Sprite;

    // public bool has_potion_now = false;

    // void Awake(){
    //     load_Potion_sprite load_Potion_Sprite = new load_Potion_sprite();
    //     load_Potion_Sprite.load();

    // }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
