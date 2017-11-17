using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickTest : MonoBehaviour {
    public Button myButton;
	// Use this for initialization
	void Start () {
        myButton.GetComponent<Button>().onClick.AddListener(TaskClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskClick() {
        Debug.Log("Clicked");
    }
}
