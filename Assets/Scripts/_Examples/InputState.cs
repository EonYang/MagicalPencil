using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonState{
	public bool value;
	public float holdTime = 0;
}



public class InputState : MonoBehaviour {

	private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();
       
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//foreach(KeyValuePair<Buttons, ButtonState> state in buttonStates){
		//	Debug.Log("button state " + state.Key + " " + state.Value.value);
		//}
	}

	public void SetButtonValue(Buttons key, bool value){
		if (!buttonStates.ContainsKey(key)) buttonStates.Add(key, new ButtonState());
		var state = buttonStates[key];
		if (state.value && !value)
		{
			Debug.Log("button" + key + "released");
			state.holdTime = 0;
		} else if(state.value && value){
			
			state.holdTime += Time.deltaTime;
			Debug.Log("button" + key + "down, time: " + state.holdTime);
		}
		state.value = value;
	}

	public bool GetButtonValue(Buttons key){
		if (buttonStates.ContainsKey(key)) return buttonStates[key].value;
		else return false;
	}
}
