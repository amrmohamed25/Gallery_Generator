using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenFilePanelExample //: EditorWindow
{

    public static void Apply()
    {
        //string path =UnityEditor.EditorUtility.OpenFilePanel("", "", "");
        string path="C:/Users/amrmo/UnityProjects/Gallery/Assets/Photos/Saul Goodman.png" ;
        if (path.Length != 0)
        {
            string dest = path.Substring(0, path.LastIndexOf('/'));
            Generator.imagesPath=dest;
            SceneManager.LoadScene(1);
        }
    }
    
}