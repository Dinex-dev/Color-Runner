using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    // on button click
        public void StartGameButton()
    {
        // load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    
}
