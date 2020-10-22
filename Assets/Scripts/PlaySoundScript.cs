using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundScript : MonoBehaviour
{

    public SceneManagerScript sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneManager.progression == 2) {
            GetComponent<AudioSource>().Play();
        }
    }

}
