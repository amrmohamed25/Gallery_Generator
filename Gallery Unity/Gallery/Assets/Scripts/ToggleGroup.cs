using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ToggleGroup : MonoBehaviour
{
    public UnityEngine.UI.ToggleGroup toggleGroup;
    public UnityEngine.UI.Text mytext;
    public GameObject inputField;
    private void Start()
    {
        if (toggleGroup == null) toggleGroup = GetComponent<UnityEngine.UI.ToggleGroup>();
    }
    void Update()
    {
        // May have several selected toggles
        foreach (UnityEngine.UI.Toggle toggle in toggleGroup.ActiveToggles())
        {
            if(toggle.name=="Toggle"){
                Generator.option=1;
                //  Debug.Log();
                // mytext.text=Path.GetDirectoryName(Application.streamingAssetsPath);
          
            }
            else if(toggle.name=="Toggle (1)"){
                 Generator.option=2;
                //  inputField.SetActive(true);
            }
            else{
                Generator.option=3;
            }
        }
    }
}
