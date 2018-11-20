using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public GameObject stage3x3;
    public GameObject stage4x4;
    public GameObject stage5x5;
    public GameObject stage5x5six;

    public int currentSceneIndex = 1;
    public GameObject currentScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            currentScene.SetActive(false);
            NextScene();
        }

        switch (currentSceneIndex)
        {
            case 1:
                stage3x3.SetActive(true);
                currentScene = stage3x3;
                break;
            case 2:
                stage4x4.SetActive(true);
                currentScene = stage4x4;
                break;
            case 3:
                stage5x5.SetActive(true);
                currentScene = stage5x5;
                break;
            case 4:
                stage5x5six.SetActive(true);
                currentScene = stage5x5six;
                break;
            case 5:
                currentSceneIndex = 1;
                break;
        }
    }

    public void NextScene()
    {
        currentSceneIndex++;
        
    }
}
