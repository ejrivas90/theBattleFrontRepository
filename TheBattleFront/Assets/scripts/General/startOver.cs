using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startOver : MonoBehaviour {
    float time = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > 8.0)
        {
            SceneManager.LoadScene("level one");
        }
	}
}
