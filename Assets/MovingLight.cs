using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MovingLight : MonoBehaviour
{
    public static MovingLight instance;
    public PlayableDirector director;
    public Camera camera;
    public GameObject light;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(director.state == PlayState.Paused)
        {
            Player.instance.FinishedCutscene();
            Destroy(this.gameObject);
        }
        light.transform.position = camera.transform.position;
    }
}
