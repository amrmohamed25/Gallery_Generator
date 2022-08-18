using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Proyecto26;

public class GetData : MonoBehaviour
{
    public UnityEngine.UI.Text mytext;
    public GameObject myButton;
    public TexturesModel floorModel;
    public TexturesModel ceilModel;
    public TexturesModel wallModel;

    public DownloadTextureModel floorDownloadModel;
    public DownloadTextureModel ceilDownloadModel;
    public DownloadTextureModel wallDownloadModel;

    void Start()
    {
        // putData();
        // putResources();
        StartCoroutine(getSettingsData());
        StartCoroutine(activateButton());


        // StartCoroutine(requestTexture("ceil"));
        // StartCoroutine(requestTexture("wall"));

        //  putFireStoreData();
    }

    void putData()
    {
        SettingsData newData = new SettingsData();
        newData.height = 50;
        newData.radius = 500;
        RestClient.Put<SettingsData>("https://gallery-3d1e5-default-rtdb.firebaseio.com/test" + ".json", newData);
        // return null;
    }

    void putResources()
    {
        SettingsData newData = new SettingsData();
        newData.height = 50;
        newData.radius = 500;
        ImageTextModel img = new ImageTextModel();
        img.imgText = "https://firebasestorage.googleapis.com/v0/b/gallery-3d1e5.appspot.com/o/resources%2FWW.jpg?alt=media&token=37224c77-3ec1-46e2-ad7b-f568b5a2bdb4,https://firebasestorage.googleapis.com/v0/b/gallery-3d1e5.appspot.com/o/resources%2FWW.txt?alt=media&token=be426699-0d80-4349-89fa-3549f30d45f7";
        ResourcesModel myModel = new ResourcesModel();
        myModel.myResources = new List<string>();
        myModel.myResources.Add("heheheheahah");
        // myModel.myResources.Add(img);
        RestClient.Put<ResourcesModel>("https://gallery-3d1e5-default-rtdb.firebaseio.com/imageText" + ".json", myModel);

        // return null;
    }

    IEnumerator getResources()
    {

        RestClient.Get<ResourcesModel>("https://gallery-3d1e5-default-rtdb.firebaseio.com/imageText" + ".json").Then(response =>
               {
                   for (int i = 0; i < response.myResources.Count; i++)
                   {
                       //  Debug.Log(response.myResources[0].Split(',')[i]);
                       //  Debug.Log(response.myResources[0].Split(',')[i]);
                       if (i % 2 == 0)
                           Generator.filePaths.Add(response.myResources[i]);
                       //  Generator.filePaths.Add(response.myResources[i].Split(',')[0]);
                       else
                       {
                           StartCoroutine(getText(response.myResources[i]));
                       }
                       //  StartCoroutine(getText(response.myResources[i].Split(',')[1]));
                   }
               });

        yield return null;
        // StartCoroutine(getLinkForTextures());
        // yield return null;
        
        

    }
    IEnumerator activateButton(){
        yield return getResources();
        myButton.SetActive(true);
    }

    IEnumerator requestTexture()
    {
        string option;
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
                option = "floor";
            else if (i == 1)
            {
                option = "ceil";
            }
            else
            {
                option = "wall";
            }
            TexturesModel myModel;
            DownloadTextureModel downModel = new DownloadTextureModel();
            if (option == "floor")
            {
                myModel = floorModel;
                floorDownloadModel = downModel;
            }
            else if (option == "ceil")
            {
                myModel = ceilModel;
                ceilDownloadModel = downModel;
            }
            else
            {
                myModel = wallModel;
                wallDownloadModel = downModel;
            }

            if (myModel.albedo != "")
            {
                Debug.Log("YAYAYAYAYAYA");
                WWW www = new WWW(myModel.albedo);                  // "download" the first file from disk
                yield return www;                                                               // Wait unill its loaded
                Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
                www.LoadImageIntoTexture(new_texture);
                downModel.albedo = new_texture;
                Debug.Log("YAYAYAYAYAYA");
            }

            if (myModel.ao != "")
            {
                WWW www = new WWW(myModel.ao);                  // "download" the first file from disk
                yield return www;                                                               // Wait unill its loaded
                Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
                www.LoadImageIntoTexture(new_texture);
                downModel.albedo = new_texture;
            }
            if (myModel.roughness != "")
            {
                WWW www = new WWW(myModel.roughness);                  // "download" the first file from disk
                yield return www;                                                               // Wait unill its loaded
                Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
                www.LoadImageIntoTexture(new_texture);
                downModel.albedo = new_texture;
            }
            if (myModel.normal != "")
            {
                WWW www = new WWW(myModel.normal);                  // "download" the first file from disk
                yield return www;                                                               // Wait unill its loaded
                Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
                www.LoadImageIntoTexture(new_texture);
                downModel.albedo = new_texture;
            }

            if (myModel.height != "")
            {
                WWW www = new WWW(myModel.height);                  // "download" the first file from disk
                yield return www;                                                               // Wait unill its loaded
                Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
                www.LoadImageIntoTexture(new_texture);
                downModel.albedo = new_texture;
            }
            if (myModel.metallic != "")
            {
                WWW www = new WWW(myModel.metallic);                  // "download" the first file from disk
                yield return www;                                                               // Wait unill its loaded
                Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
                www.LoadImageIntoTexture(new_texture);
                downModel.albedo = new_texture;
            }
        }
        // yield return new_texture;
    }
    IEnumerator getLinkForTextures()
    {
        string textureType;
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                textureType = "floor";
            }
            else if (i == 1)
            {
                textureType = "ceil";
            }
            else
            {
                textureType = "wall";
            }
            RestClient.Get<TexturesModel>("https://gallery-3d1e5-default-rtdb.firebaseio.com/" + textureType + "Textures" + ".json").Then(response =>
                           {
                               TexturesModel myTexture = new TexturesModel();
                               if (textureType == "floor")
                               {
                                   floorModel = myTexture;
                               }
                               else if (textureType == "ceil")
                               {
                                   ceilModel = myTexture;
                               }
                               else if (textureType == "wall")
                               {
                                   wallModel = myTexture;
                               }
                               myTexture.albedo = response.albedo;
                               myTexture.ao = response.ao;
                               myTexture.roughness = response.roughness;
                               myTexture.normal = response.normal;
                               myTexture.height = response.height;
                               myTexture.metallic = response.metallic;
                               Debug.Log("lol");
                               Debug.Log(myTexture.albedo);
                           });
        }
        yield return null;
        StartCoroutine(requestTexture());

    }

    IEnumerator getText(string url)
    {
        // WWWForm form=new WWWForm();
        // var headers=form.headers;
        // headers["Access-Control-Allow-Origin"]="*";
        // headers["X-Parse-Application-Id"]="1:899326896937:web:56b9f5e12bceafb8e76538";
        // headers["X-Parse-REST-API-Key"]="AIzaSyBVup_uatKfnBl3_eobNZppETgxyiCUqr8";
        // headers["Content-Type"]="application/json";
        WWW www=new WWW(url);
        yield return www;
        Debug.Log(www.text);
        Generator.imageTexts.Add(www.text);
        // UnityWebRequest www = UnityWebRequest.Get(url);

        // yield return www.SendWebRequest();

        // // if (www.isError)
        // // {
        // //     Debug.Log(www.error);
        // // }
        // // else
        // // {
        //     Debug.Log(www.downloadHandler.text);
        //     Generator.imageTexts.Add(www.downloadHandler.text);
        // // }
    }
    IEnumerator getSettingsData()
    {
        RestClient.Get<SettingsData>("https://gallery-3d1e5-default-rtdb.firebaseio.com/currentSettings" + ".json").Then(response =>
        {
            Debug.Log(response.height);
            Debug.Log(response.radius);
            Generator.radius = response.radius;
            Generator.height = response.height;
            // mytext.text = response.radius + " " + response.height;
            // myButton.SetActive(true);
        });
        yield return null;
    }

    // void putFireStoreData(){
    //     SettingsData newData=new SettingsData();
    //     newData.height=50;
    //     newData.radius=500;
    //     RestClient.Put<SettingsData>("https://firestore.googleapis.com/v1/projects/gallery-3d1e5/databases/(default)/documents/t.json?key=AIzaSyBVup_uatKfnBl3_eobNZppETgxyiCUqr8",newData);
    //     // return null;
    // }
    //     IEnumerator getSettingsFireStoreData(){
    //   RestClient.Get<SettingsData>("https://gallery-3d1e5-default-rtdb.firebaseio.com/test"+".json").Then(response =>{
    //     Debug.Log(response.height);
    //     Debug.Log(response.radius);
    //   });
    //   yield return null;
    //     }
}


























// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
// using Proyecto26;

// public class GetData : MonoBehaviour
// {
//     public UnityEngine.UI.Text mytext;
//     public GameObject myButton;
//     public TexturesModel floorModel = new TexturesModel();
//     public TexturesModel ceilModel = new TexturesModel();
//     public TexturesModel wallModel = new TexturesModel();

//     public DownloadTextureModel floorDownloadModel;
//     public DownloadTextureModel ceilDownloadModel;
//     public DownloadTextureModel wallDownloadModel;//=new DownloadTextureModel()

//     void Start()
//     {
//         // putData();
//         // putResources();
//         StartCoroutine(getSettingsData());
//         StartCoroutine(getResources());
//         // StartCoroutine(getAll());

//         // StartCoroutine(requestTexture("ceil"));
//         // StartCoroutine(requestTexture("wall"));

//         //  putFireStoreData();
//     }

//     IEnumerator getAll()
//     {
//         yield return StartCoroutine(getSettingsData());
//         yield return StartCoroutine(requestTexture());

//     }
//     void putData()
//     {
//         SettingsData newData = new SettingsData();
//         newData.height = 50;
//         newData.radius = 500;
//         RestClient.Put<SettingsData>("https://gallery-3d1e5-default-rtdb.firebaseio.com/test" + ".json", newData);
//         // return null;
//     }

//     void putResources()
//     {
//         SettingsData newData = new SettingsData();
//         newData.height = 50;
//         newData.radius = 500;
//         ImageTextModel img = new ImageTextModel();
//         img.imgText = "https://firebasestorage.googleapis.com/v0/b/gallery-3d1e5.appspot.com/o/resources%2FWW.jpg?alt=media&token=37224c77-3ec1-46e2-ad7b-f568b5a2bdb4,https://firebasestorage.googleapis.com/v0/b/gallery-3d1e5.appspot.com/o/resources%2FWW.txt?alt=media&token=be426699-0d80-4349-89fa-3549f30d45f7";
//         ResourcesModel myModel = new ResourcesModel();
//         myModel.myResources = new List<string>();
//         myModel.myResources.Add("heheheheahah");
//         // myModel.myResources.Add(img);
//         RestClient.Put<ResourcesModel>("https://gallery-3d1e5-default-rtdb.firebaseio.com/imageText" + ".json", myModel);

//         // return null;
//     }

//     IEnumerator getResources()
//     {

//         RestClient.Get<ResourcesModel>("https://gallery-3d1e5-default-rtdb.firebaseio.com/imageText" + ".json").Then(response =>
//                {
//                    for (int i = 0; i < response.myResources.Count; i++)
//                    {
//                        Debug.Log(response.myResources[i]);
//                        //  Debug.Log(response.myResources[0].Split(',')[i]);
//                        //  Debug.Log(response.myResources[0].Split(',')[i]);
//                        if (i % 2 == 0)
//                        {
//                            Generator.filePaths.Add(response.myResources[i]);
//                            mytext.text += " " + response.myResources[i];
//                        }
//                        //  Generator.filePaths.Add(response.myResources[i].Split(',')[0]);
//                        else
//                        {
//                            mytext.text += " " + response.myResources[i];
//                            StartCoroutine(getText(response.myResources[i]));

//                        }
//                        //  StartCoroutine(getText(response.myResources[i].Split(',')[1]));
//                    }
//                    myButton.SetActive(true);
//                });

//         // yield return null;
//         // StartCoroutine(getLinkForTextures());
//         // yield return StartCoroutine(bringTexture("floor"));
//         //  Debug.Log("MSTNY 2");
//         // yield return StartCoroutine(bringTexture("ceil"));
//         //  Debug.Log("MSTNY 3");
//         // // while (wallModel == null)
//         // yield return StartCoroutine(bringTexture("wall"));
//         //  Debug.Log("MSTNY 4");
//         yield return null;

//         // myButton.SetActive(true);

//     }


//     IEnumerator requestTexture()
//     {
//         yield return StartCoroutine(getResources());
//         // yield return null;
//         // Debug.Log("mstnyyy 1");
//         // yield return StartCoroutine(getLinkForTextures());
//         // Debug.Log("MSTNY 2");
//         // while (floorModel == null)
//         // while (ceilModel == null)


//         // yield return new WaitForSeconds(10);
//         // string option;
//         // for (int i = 0; i < 3; i++)
//         // {
//         //     if (i == 0)
//         //         option = "floor";
//         //     else if (i == 1)
//         //     {
//         //         option = "ceil";
//         //     }
//         //     else
//         //     {
//         //         option = "wall";
//         //     }
//         //     TexturesModel myModel;
//         //     DownloadTextureModel downModel = new DownloadTextureModel();
//         //     if (option == "floor")
//         //     {
//         //         Debug.Log("Hahah");
//         //         Debug.Log(floorModel.albedo);
//         //         myModel = floorModel;
//         //         floorDownloadModel = downModel;
//         //     }
//         //     else if (option == "ceil")
//         //     {
//         //         myModel = ceilModel;
//         //         ceilDownloadModel = downModel;
//         //     }
//         //     else
//         //     {
//         //         myModel = wallModel;
//         //         wallDownloadModel = downModel;
//         //     }

//         //     if (myModel.albedo != "")
//         //     {
//         //         Debug.Log("YAYAYAYAYAYA");
//         //         Debug.Log(myModel.albedo);
//         //         WWW www = new WWW(myModel.albedo);                  // "download" the first file from disk
//         //         yield return www;                                                               // Wait unill its loaded
//         //         Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
//         //         www.LoadImageIntoTexture(new_texture);
//         //         downModel.albedo = new_texture;
//         //         Debug.Log(downModel);
//         //         Debug.Log("YAYAYAYAYAYA");
//         //     }

//         //     if (myModel.ao != "")
//         //     {
//         //         WWW www = new WWW(myModel.ao);                  // "download" the first file from disk
//         //         yield return www;                                                               // Wait unill its loaded
//         //         Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
//         //         www.LoadImageIntoTexture(new_texture);
//         //         downModel.albedo = new_texture;
//         //     }
//         //     if (myModel.roughness != "")
//         //     {
//         //         WWW www = new WWW(myModel.roughness);                  // "download" the first file from disk
//         //         yield return www;                                                               // Wait unill its loaded
//         //         Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
//         //         www.LoadImageIntoTexture(new_texture);
//         //         downModel.albedo = new_texture;
//         //     }
//         //     if (myModel.normal != "")
//         //     {
//         //         WWW www = new WWW(myModel.normal);                  // "download" the first file from disk
//         //         yield return www;                                                               // Wait unill its loaded
//         //         Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
//         //         www.LoadImageIntoTexture(new_texture);
//         //         downModel.albedo = new_texture;
//         //     }

//         //     if (myModel.height != "")
//         //     {
//         //         WWW www = new WWW(myModel.height);                  // "download" the first file from disk
//         //         yield return www;                                                               // Wait unill its loaded
//         //         Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
//         //         www.LoadImageIntoTexture(new_texture);
//         //         downModel.albedo = new_texture;
//         //     }
//         //     if (myModel.metallic != "")
//         //     {
//         //         WWW www = new WWW(myModel.metallic);                  // "download" the first file from disk
//         //         yield return www;                                                               // Wait unill its loaded
//         //         Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
//         //         www.LoadImageIntoTexture(new_texture);
//         //         downModel.albedo = new_texture;
//         //     }
//         // }
//         // // yield return new_texture;
//     }

//     IEnumerator bringTexture(string textureType)
//     {
//         Debug.Log("gowa texture");
//         RestClient.Get<TexturesModel>("https://gallery-3d1e5-default-rtdb.firebaseio.com/" + textureType + "Textures" + ".json").Then(response =>
//                            {
//                                Debug.Log("gowaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
//                                Debug.Log(textureType);
//                                //    TexturesModel myTexture = new TexturesModel();
//                                if (textureType == "floor")
//                                {
//                                    // Debug.Log("ahahahazzzzzzzzzzz");
//                                    //    floorModel = new TexturesModel();
//                                    floorModel.albedo = response.albedo;
//                                    floorModel.ao = response.ao;
//                                    floorModel.roughness = response.roughness;
//                                    floorModel.normal = response.normal;
//                                    floorModel.height = response.height;
//                                    floorModel.metallic = response.metallic;
//                                    Debug.Log("lol");
//                                    Debug.Log(floorModel.albedo);
//                                }
//                                else if (textureType == "ceil")
//                                {
//                                    //    ceilModel = new TexturesModel();
//                                    ceilModel.albedo = response.albedo;
//                                    ceilModel.ao = response.ao;
//                                    ceilModel.roughness = response.roughness;
//                                    ceilModel.normal = response.normal;
//                                    ceilModel.height = response.height;
//                                    ceilModel.metallic = response.metallic;
//                                }
//                                else if (textureType == "wall")
//                                {
//                                    //    wallModel = new TexturesModel();
//                                    wallModel.albedo = response.albedo;
//                                    wallModel.ao = response.ao;
//                                    wallModel.roughness = response.roughness;
//                                    wallModel.normal = response.normal;
//                                    wallModel.height = response.height;
//                                    wallModel.metallic = response.metallic;
//                                }



//                            });
//         yield return null;
//     }
//     // IEnumerator getLinkForTextures()
//     // {
//     //     string textureType;
//     //     for (int i = 0; i < 3; i++)
//     //     {

//     //         if (i == 0)
//     //         {
//     //             textureType = "floor";
//     //         }
//     //         else if (i == 1)
//     //         {
//     //             textureType = "ceil";
//     //         }
//     //         else
//     //         {
//     //             textureType = "wall";
//     //         }
//     //         Debug.Log("AHOOO");
//     //         Debug.Log(textureType);
//     //         yield return StartCoroutine(bringTexture(textureType));
//     //     // while (ceilModel == null)
//     //     // yield return StartCoroutine(bringTexture("ceil"));
//     //     // while (wallModel == null)
//     //     // yield return StartCoroutine(bringTexture("wall"));

//     //     }
//     //     yield return null;
//     //     // WWW www=new WWW();



//     // }

//     IEnumerator getText(string url)
//     {
//         UnityWebRequest www = UnityWebRequest.Get(url);
//         Debug.Log("hahah");
//         yield return www.SendWebRequest();

//         // if (www.isError)
//         // {
//         // Debug.Log(www.error);
//         // }
//         // else
//         // {
//         Debug.Log(www.downloadHandler.text);
//         Generator.imageTexts.Add(www.downloadHandler.text);
//         // }
//     }
//     IEnumerator getSettingsData()
//     {
//         RestClient.Get<SettingsData>("https://gallery-3d1e5-default-rtdb.firebaseio.com/currentSettings" + ".json").Then(response =>
//         {
//             Debug.Log(response.height);
//             Debug.Log(response.radius);
//             Generator.radius = response.radius;
//             Generator.height = response.height;
//             mytext.text = response.radius + " " + response.height;
//             // myButton.SetActive(true);
//         });
//         yield return null;
//     }

//     // void putFireStoreData(){
//     //     SettingsData newData=new SettingsData();
//     //     newData.height=50;
//     //     newData.radius=500;
//     //     RestClient.Put<SettingsData>("https://firestore.googleapis.com/v1/projects/gallery-3d1e5/databases/(default)/documents/t.json?key=AIzaSyBVup_uatKfnBl3_eobNZppETgxyiCUqr8",newData);
//     //     // return null;
//     // }
//     //     IEnumerator getSettingsFireStoreData(){
//     //   RestClient.Get<SettingsData>("https://gallery-3d1e5-default-rtdb.firebaseio.com/test"+".json").Then(response =>{
//     //     Debug.Log(response.height);
//     //     Debug.Log(response.radius);
//     //   });
//     //   yield return null;
//     //     }
// }
