using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public static void quitGame(){
            Application.Quit();
            // UnityEditor.EditorApplication.isPlaying = false;
    }
}
