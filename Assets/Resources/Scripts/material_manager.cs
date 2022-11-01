using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class material_falling_controller
{
    // the falling material minfy speed
    private float change_scale;
    public void init(){
        change_scale = 0.9975f;
    }

    // minfy material
    public void material_minify(GameObject gameobject){
        gameobject.transform.localScale *= change_scale;
    }
    
    // report material type name and index by RegularExpressions
    public void material_OnDestroy_action(GameObject gameobject){
        Match index = Regex.Match(gameobject.name,@"\d+$");
        Match name =  Regex.Match(gameobject.name,@"(\w+)_");

        // for check index and name
        // Debug.Log("index:"+index.ToString());
        // Debug.Log("name:"+name.Groups[1]);

        potion_generator_manager potion_Generator_Manager = new potion_generator_manager();
        potion_Generator_Manager.generate_current_potion_manager(int.Parse(index.Value),name.Groups[1].ToString());
    }
}

public class material_touch_sensor
{
    float material_bound_limit_x;
    // define the x limit
    public void init(){
        float material_bound_limit_x = (Camera.main.orthographicSize) * (Camera.main.aspect + 0.1f);
    }

    // touch material
    public void touch_incident(GameObject gameobject){
        // to avoid touch multiple times
        gameobject.GetComponent<Collider2D>().enabled = false;
        
        // falling circulate is depend on gravity
        gameobject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameobject.GetComponent<Rigidbody2D>().gravityScale = 5; // speed

        // the distance between now material position and goal pot position 
        float delta_x = material_bound_limit_x-gameobject.transform.position.x;
        // define gameobject velocity by distance
        // the y volocity is the jump effect
        gameobject.GetComponent<Rigidbody2D>().velocity = new Vector2(delta_x/0.75f,7.5f);
    }
}

public class material_bound
{
    Camera main_camera;
    public float material_bound_limit_x;
    public float material_bound_limit_y;
    AudioSource fall_to_pot_audio;

    // initialize 
    public void init()
    {   
        // define material bound limit
        main_camera = Camera.main;
        material_bound_limit_x = (main_camera.orthographicSize) * (main_camera.aspect + 0.1f);
        material_bound_limit_y = -main_camera.orthographicSize + 1f;

        // get drop sound effect
        fall_to_pot_audio = GameObject.Find("fall_to_pot_sound_effect").GetComponent<AudioSource>();
    }

    public void bound_limit_detector(float gameobject_position_x,float gameobject_position_y,GameObject gameobject)
    {
        // not used
        if(gameobject_position_x >= material_bound_limit_x){
            UnityEngine.Object.Destroy(gameobject);
        }
        // touched and drop to pot
        if(gameobject_position_y <= material_bound_limit_y){
            fall_to_pot_audio.Play();

            // the action about create potion while the material go to pot
            material_falling_controller material_Falling_Controller = new material_falling_controller();
            material_Falling_Controller.material_OnDestroy_action(gameobject);

            UnityEngine.Object.Destroy(gameobject);
        }
    }
}

public class material_manager : MonoBehaviour
{
    material_bound material_Bound;
    material_touch_sensor material_Touch_Sensor;
    material_falling_controller material_Falling_Controller;
    
    // the falling status of the gameobject 
    public bool is_falling;
    public bool is_go_to_pot;
    void Awake(){
        material_Bound = new material_bound();
        material_Bound.init();

        material_Touch_Sensor= new material_touch_sensor();
        material_Touch_Sensor.init();

        material_Falling_Controller = new material_falling_controller();
        material_Falling_Controller.init();

        // init
        is_falling = false;
        is_go_to_pot = false;
    }
    void Start()
    {
        
    }

    void Update()
    {
        material_Bound.bound_limit_detector(this.gameObject.transform.position.x,
                                            this.gameObject.transform.position.y,
                                            this.gameObject);

        // falling effect
        if(is_falling){
            material_Falling_Controller.material_minify(this.gameObject);
        }
        
    }

    void OnMouseDown(){
        material_Touch_Sensor.touch_incident(this.gameObject);

        // set falling status
        is_falling = true;
    }
}
