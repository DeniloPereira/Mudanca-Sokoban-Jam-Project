using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioSource audSrc;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound(string sound)
    {
        switch(sound){
            case "packing_box":
                gameObject.GetComponent<AudioSource>();
                break;
            case "error":
                break;
            case "box_move":
                break;
        }
    }
}