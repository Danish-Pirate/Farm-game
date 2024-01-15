using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private void Awake() {
        InputManager.EscPress += ExitGame;
    }

    private void ExitGame(object sender, EventArgs e) {
        Application.Quit();
    }
}