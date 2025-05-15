using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Owl : MonoBehaviour
{
    private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField]
    private float TimePerOwl;

    private float timer;
    private List<GameObject> Owls = new List<GameObject>();
    [HideInInspector]
    public bool TimerCanGo;
    void Awake(){
        foreach (Transform child in transform)
        {
            Owls.Add(child.gameObject);
            audioSources.Add(child.gameObject.GetComponent<AudioSource>());
        }
    }
    void Update()
    {
        if(TimerCanGo){
            timer += Time.deltaTime;   
            if(timer>TimePerOwl){
                timer = 0;
                int a = Random.Range(1, Owls.Count);
                audioSources[a].Play();
            } 
        }

        
    }
}
