﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputController : MonoBehaviour{

    public static UserInputController instance;

    public KeyCode[] playerKeyCode = new KeyCode[4];

    private void Awake() {
        instance = instance ? instance : this;
        DontDestroyOnLoad(instance);
    }


    private void Update() {
        /*
        System.Array values = System.Enum.GetValues(typeof(KeyCode));
        foreach (KeyCode code in values) {
            if (Input.GetKeyDown(code)) { print(System.Enum.GetName(typeof(KeyCode), code)); }
        }
*/
        // If the game starts
        if (LogicController.instance.gameStart){

            // Check each input
            for (int i = 0; i < playerKeyCode.Length; i++){

                // If a valid input is pressed
                if(Input.GetKeyDown(playerKeyCode[i])){

                    // If it matches the current button
                    if(ButtonSpawner.instance.currentPlayer == i){
                        LogicController.instance.SymbolWasMatched();
                    }else{

                        // Otherwise it is a mismatch
                        LogicController.instance.SymbolWasMisMatched();
                    }
                }
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.Space)) {
            StoryController.instance.SymbolWasMatched();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            StoryController.instance.SymbolWasMisMatched();
        }
        */
    }
}