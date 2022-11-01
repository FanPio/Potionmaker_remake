using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class material_Load{
    public static Object[] Mana_material_list;
    public static Object[] Life_material_list;
    public static Object[] Stamina_material_list;

    public static List<Object> All_material_list;


    public void init_Load_sprite(){
        // load picture
        Mana_material_list = Resources.LoadAll<Sprite>("Spirits/potion_material/blue");
        Life_material_list = Resources.LoadAll<Sprite>("Spirits/potion_material/red");
        Stamina_material_list = Resources.LoadAll<Sprite>("Spirits/potion_material/yellow");

        // struct a list
        All_material_list = new List<Object>();
        
        // add every type material to all material list
        All_material_list.AddRange(Mana_material_list);
        All_material_list.AddRange(Life_material_list);
        All_material_list.AddRange(Stamina_material_list);
        

        // for test
        // foreach(Object ob in All_material_list){
        //     Debug.Log(ob.name);
        //     Debug.Log(ob.GetType());
        // }
        
        // for test
        // foreach(Object ob in blue_material_list){
        //     Debug.Log(ob.name);
        //     Debug.Log(ob.GetType());
        // }
    }
}

public class material_creator{
    Camera main_camera;
    public float move_speed;
    
    // start position
    private float material_bound_start_x,material_bound_start_y;

    // define new material start position 
    public void init_material_start_position(){
        main_camera = Camera.main;
        material_bound_start_x = -main_camera.orthographicSize * (main_camera.aspect + 0.1f);
        material_bound_start_y = main_camera.orthographicSize * 0.85f;
    }

    // function to create material
    // input data list is all material
    public void material_create(Object[] input_material_list){
        // random material
        int random_index = Random.Range(0,input_material_list.Length);

        // create a new gameobject
        GameObject go = new GameObject(input_material_list[random_index].name);
        // set new gameobject's position and scale
        go.transform.position = new Vector2(material_bound_start_x,material_bound_start_y);
        go.transform.localScale = new Vector3(0.7f,0.7f,0);

        // add script to the new gameobject
        go.AddComponent<material_manager>();

        // apply picture to the new gameobject
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = (input_material_list[random_index] as Sprite);

        // add box collider to the new gameobject
        BoxCollider2D boxcollider2d = go.AddComponent<BoxCollider2D>();
        boxcollider2d.isTrigger = true;

        // set new gameobject's rigibody component
        Rigidbody2D rigidbody = go.AddComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        rigidbody.velocity = new Vector2(move_speed,0f);

        // set tag
        go.gameObject.tag = "material";
    }
}

public class material_generator_manager : MonoBehaviour
{
    // interval for every create material
    public static float interval = 1;

    private float delta_time = 0;
    public float move_speed;

    material_Load load_sprite_data;
    material_creator material_Creator;

    void Awake(){
        load_sprite_data = new material_Load();
        load_sprite_data.init_Load_sprite();

        material_Creator = new material_creator();
        material_Creator.move_speed = this.move_speed;
        material_Creator.init_material_start_position();


    }
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {
        if(delta_time >= interval){
            // important: Toarray is in order to change All_material_list
            // from System.Collections.Generic.List to unity object[]
            // material_Creator.material_create();
            material_Creator.material_create(material_Load.All_material_list.ToArray());

            // reset delta_time
            delta_time=0;
        }

        // plus delta_time
        delta_time += Time.deltaTime;
    }
}

