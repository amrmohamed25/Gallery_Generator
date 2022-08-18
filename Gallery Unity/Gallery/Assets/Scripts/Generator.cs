using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Proyecto26;
public class Generator : MonoBehaviour
{
    // public static List<Texture2D> spriteImages = new List<Texture2D>();
    public static List<string> imageTexts = new List<string>();
    private bool isLoaded;
    public static string imagesPath = Directory.GetCurrentDirectory() + "\\Assets\\Photos";
    // public List<Texture2D> mytextures;
    public GameObject wallTile;
    public GameObject LongWallTile;
    public GameObject cubePrefab;
    public GameObject textPrefab;
    private TextPrefab textShape;
    private CubeScript myShape;
    public GameObject corridorPlane;
    public GameObject roofPlane;
    public static int option = 3;
    public GameObject spotLight;
    public GameObject squareRoof;
    public GameObject squarePlane;
    public GameObject polygonRoof;
    public GameObject polygonPlane;
    public static List<string> filePaths=new List<string>();
    public List<string> textPaths;
    public static float radius=200;
    public static float height=0;
    void Start()
    {
        //  Application.streamingAssetsPath.
        //  wallTile.GetComponent<Renderer>().material.mainTexture=;
        Debug.Log(Path.GetDirectoryName(Application.dataPath));
        Debug.Log(radius);
        Debug.Log(height);
        // DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        // FileInfo[] info = dir.GetFiles("*.*");
        // Debug.Log(info.Length);
        // Debug.Log(Directory.GetCurrentDirectory() + "\\Assets\\Photos");
        // List<string> filePaths = new List<string>(Directory.GetFiles(imagesPath, "*.png"));
        // List<string> textPaths = new List<string>(Directory.GetFiles(imagesPath, "*.txt"));
        // filePaths.AddRange(Directory.GetFiles(imagesPath, "*.jpg"));
        // filePaths.AddRange(Directory.GetFiles(imagesPath, "*.jpeg"));
        // load_texts(textPaths);
        // if (filePaths.Count != textPaths.Count)
        // {
        //     QuitGame.quitGame();
        // }
        loadLayout(filePaths);
    }
    
    void load_texts(List<string> textPaths)
    {
        // for (int i = 0; i < textPaths.Count; i++)
        // {
        //     string word = File.ReadAllText(textPaths[i]);
        //     Debug.Log(textPaths[i]);
        //     Debug.Log(word);
        //     imageTexts.Add(word);
        // }
    }
    void loadLayout(List<string> filePaths)
    {
        // option=2;
        // Debug.Log(option);
        if (option == 1)
        {
            StartCoroutine(generateCorridorLayout(filePaths));
        }
        else if (option == 2)
        {
            StartCoroutine(generateSquareLayout(filePaths));
            // generateSquareLayout(filePaths);
        }
        else if (option == 3)
        {
            StartCoroutine(generatePolygonLayout(filePaths));
            // generatePolygonLayout(filePaths);
        }
    }
    IEnumerator generatePolygonLayout(List<string> filePaths)
    {
        //angle to rotate=360/n
        float requiredAngle = 360.0f / (float)filePaths.Count;

        // int radius = 200;
        
        // float lengthOfSide =2*radius/(Mathf.Tan(2f*(float)Mathf.PI/(float)filePaths.Count));
        float lengthOfSide=Mathf.Tan(2f*Mathf.PI/(2*(float)filePaths.Count))*radius*2;
        Debug.Log(lengthOfSide);
        setRoofAndPlane();
        for (int i = 0; i < filePaths.Count; i++)
        {
            Vector3 backward;
            backward = transform.forward;
            GameObject mywall = Instantiate(wallTile, new Vector3(transform.position.x, 3, transform.position.z) - radius * new Vector3(backward.x, 0, backward.z), transform.rotation);
            mywall.transform.localScale = new Vector3(lengthOfSide, 9+height, 1);
            mywall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(lengthOfSide, lengthOfSide / 1.5f);
            transform.Rotate(0, requiredAngle, 0);
            var filePath = filePaths[i];
            var text = imageTexts[i];
            WWW www = new WWW(filePath);                  // "download" the first file from disk
            yield return www;                                                               // Wait unill its loaded
            Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
            www.LoadImageIntoTexture(new_texture);                           // put the downloaded image file into the new Texture2D
            backward = transform.forward;
            myShape = Instantiate(cubePrefab, new Vector3(mywall.transform.position.x, 3, mywall.transform.position.z) - 0.5f * new Vector3(backward.x, 0, backward.z), mywall.transform.rotation).GetComponent<CubeScript>();
            // myShape.transform.Rotate(0, 180, 0);
            myShape.transform.Translate(0, 0, 1, Space.Self);
            myShape.GetComponent<Renderer>().material.mainTexture = new_texture;           // put the new image into the current material as defuse material for testing.
            myShape.GetComponent<Renderer>().material.mainTexture.wrapMode = TextureWrapMode.Clamp;
            setObjectDimensions();
            // Debug.Log(myShape.gameObject.name);
            // Debug.Log(Path.GetFileName(filePath));
            if (Path.GetFileName(filePath) != "Door.jpg")
            {
                myShape.myObject = Instantiate(textPrefab, new Vector3(myShape.transform.position.x, 1, myShape.transform.position.z) + 1f * new Vector3(backward.x, 0, backward.z), Quaternion.Euler(mywall.transform.rotation.eulerAngles.x, mywall.transform.rotation.eulerAngles.y + 180, mywall.transform.rotation.eulerAngles.z)).GetComponent<TextPrefab>();
                myShape.myObject.myText.text = text;
                myShape.myObject.transform.Translate(0.25f, 0, 0, Space.Self);
                backward = myShape.transform.forward;
                Instantiate(spotLight, new Vector3(myShape.transform.position.x, 3, myShape.transform.position.z) + 7 * new Vector3(backward.x, 0, backward.z), Quaternion.Euler(myShape.transform.rotation.eulerAngles.x, myShape.transform.rotation.eulerAngles.y - 180, myShape.transform.rotation.eulerAngles.z));
            }
            else
            {
                myShape.transform.Translate(0, 0, -0.5f, Space.Self);
                myShape.transform.localScale = new Vector3(4, 8, 1);
            }
        }

    }
    IEnumerator generateSquareLayout(List<string> filePaths)
    {
        int imagesInEachWall = (int)Mathf.Ceil(filePaths.Count * 1.0f / 4);
        int resizer = imagesInEachWall + 1;//+1 resize the wall but the photos have same offsets.[]
        if (imagesInEachWall == 1)
            resizer = 2;
        int totalWalls = 4;
        int nextItemIndex = 0;
        List<float> offsetList = new List<float>();
        int imagesSize = imagesInEachWall;
        Debug.Log(imagesInEachWall);
        if (imagesInEachWall % 2 == 1)
        {
            offsetList.Add(0);
            imagesSize--;
        }
        bool temp = true;
        float tempVar = 7;
        for (int i = 0; i < imagesSize; i++)
        {
            if (temp == true)
            {
                offsetList.Add(tempVar);
                temp = false;
            }
            else
            {
                tempVar = -tempVar;
                offsetList.Add(tempVar);
                tempVar = -tempVar;
                tempVar += 7;
                temp = true;
            }
        }
        Debug.Log(offsetList);
        Debug.Log(offsetList.Count);
        setRoofAndPlane();
        for (int i = 0; i < totalWalls; i++)
        {
            GameObject mywall = Instantiate(wallTile, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 0.5f), transform.rotation);
            mywall.transform.localScale = new Vector3(7 * resizer, 9+height, 1);
            mywall.GetComponent<Renderer>().material.mainTextureScale = new Vector2(resizer, resizer / 1.5f);
            if (nextItemIndex < filePaths.Count)
            {
                for (int j = 0; j < imagesInEachWall; j++)
                {
                    var filePath = filePaths[nextItemIndex];
                    var text = imageTexts[nextItemIndex];
                    WWW www = new WWW(filePath);                  // "download" the first file from disk
                    yield return www;                                                               // Wait unill its loaded
                    Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
                    www.LoadImageIntoTexture(new_texture);                           // put the downloaded image file into the new Texture2D
                    Vector3 backward;
                    backward = transform.forward;
                    myShape = Instantiate(cubePrefab, new Vector3(transform.position.x, 3, transform.position.z) - 0.5f * new Vector3(backward.x, 0, backward.z), transform.rotation).GetComponent<CubeScript>();
                    myShape.transform.Translate(offsetList[j], 0, 0, Space.Self);
                    Debug.Log(offsetList[j]);
                    if (transform.rotation.eulerAngles.y == 180)
                    {
                        myShape.transform.Translate(0, 0, -0.5f, Space.Self);
                    }
                    if (transform.rotation.eulerAngles.y == 0)
                    {
                        myShape.transform.Translate(0, 0, 0.5f, Space.Self);
                    }
                    myShape.transform.Rotate(0, 180, 0);
                    myShape.GetComponent<Renderer>().material.mainTexture = new_texture;           // put the new image into the current material as defuse material for testing.
                    myShape.GetComponent<Renderer>().material.mainTexture.wrapMode = TextureWrapMode.Clamp;
                    setObjectDimensions();

                    backward = myShape.transform.forward;
                    if (Path.GetFileName(filePath) != "Door.jpg")
                    {
                        myShape.myObject = Instantiate(textPrefab, new Vector3(myShape.transform.position.x, 1, myShape.transform.position.z) + 1f * new Vector3(backward.x, 0, backward.z), transform.rotation).GetComponent<TextPrefab>();
                        myShape.myObject.myText.text = text;
                        backward = myShape.transform.forward;
                        Instantiate(spotLight, new Vector3(myShape.transform.position.x, 3, myShape.transform.position.z) + 7 * new Vector3(backward.x, 0, backward.z), transform.rotation);
                    }
                    else
                    {
                        myShape.transform.Translate(0, 0, -0.25f, Space.Self);
                        myShape.transform.localScale = new Vector3(4, 8, 1);
                    }
                    nextItemIndex++;
                    if (nextItemIndex == filePaths.Count)
                        break;
                }
            }
            transform.Translate(3.5f * resizer, 0, -3.5f * resizer, Space.Self);
            transform.Rotate(0, 90, 0, Space.Self);
        }

    }
    void setRoofAndPlane()
    {
        GameObject pointer = Instantiate(squarePlane, new Vector3(transform.position.x - 3f, transform.position.y - 4f, transform.position.z - 0.5f), transform.rotation);
        pointer.transform.localScale = new Vector3(10000.0f, 1.0f, 10000);
        pointer.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1000, 1000);
        pointer = Instantiate(squareRoof, new Vector3(transform.position.x + 3f, transform.position.y + 3.5f+height, transform.position.z - 0.5f), transform.rotation);
        pointer.transform.localScale = new Vector3(10000.0f, 1.0f, 10000f);
        pointer.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1000, 1000);
    }
    IEnumerator generateCorridorLayout(List<string> filePaths)
    {
        GameObject mywall=Instantiate(LongWallTile, new Vector3(transform.position.x + 4f, transform.position.y + 1+height/2, transform.position.z - 6.5f), Quaternion.Euler(0, 90, 0));
        mywall.transform.localScale = new Vector3(mywall.transform.localScale.x, 9+height, 1);
        mywall= Instantiate(wallTile, new Vector3(transform.position.x, transform.position.y + 1+height/2, transform.position.z + 0.5f), transform.rotation);
        mywall.transform.localScale = new Vector3(7, 9+height, 1);
        mywall=Instantiate(wallTile, new Vector3(transform.position.x, transform.position.y + 1+height/2, transform.position.z - 13.5f), transform.rotation);
        mywall.transform.localScale = new Vector3(7, 9+height, 1);
        Instantiate(corridorPlane, new Vector3(transform.position.x + 0.5f, transform.position.y - 3.5f, transform.position.z - 0.5f), transform.rotation);
        transform.Translate(-7f, 0f, 0f);
        Debug.Log(Application.streamingAssetsPath);
        // filePaths=new List<string>();
        // Application.streamingAssetsPath.
        // filePaths.Add(Path.Combine(Application.streamingAssetsPath, "WW.jpg"));
        // imageTexts.Add("lolllllll");
        for (int i = 0; i < filePaths.Count; i++)
        {
            var filePath = filePaths[i];
            var text = imageTexts[i];
        // UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://www.my-server.com/image.png");
        // yield return www.SendWebRequest();

        // Texture myTexture = DownloadHandlerTexture.GetContent(www);
            // RestClient.g
            WWW www = new WWW(filePath);                  // "download" the first file from disk
            yield return www;                                                               // Wait unill its loaded
            Texture2D new_texture = new Texture2D(512, 512);               // create a new Texture2D (you could use a gloabaly defined array of Texture2D )
            www.LoadImageIntoTexture(new_texture);                           // put the downloaded image file into the new Texture2D
            // spriteImages.Add(new_texture);
            mywall=Instantiate(wallTile, new Vector3(transform.position.x, transform.position.y + 1+height/2, transform.position.z + 0.5f), transform.rotation);
mywall.transform.localScale = new Vector3(7, 9+height, 1);
            if (i % 2 == 0)
            {
                myShape = Instantiate(cubePrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation).GetComponent<CubeScript>();
                if (Path.GetFileName(filePath) != "Door.jpg")
                    Instantiate(spotLight, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 6f), Quaternion.Euler(20, 0, 0));
            }
            else
            {
                myShape = Instantiate(cubePrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z - 13f), Quaternion.Euler(0, 180, 0)).GetComponent<CubeScript>();
                if (Path.GetFileName(filePath) != "Door.jpg")
                    Instantiate(spotLight, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 6f), Quaternion.Euler(170, 0, 0));
            }


            myShape.transform.Rotate(0, 180, 0);
            myShape.GetComponent<Renderer>().material.mainTexture = new_texture;           // put the new image into the current material as defuse material for testing.
            myShape.GetComponent<Renderer>().material.mainTexture.wrapMode = TextureWrapMode.Clamp;
            Instantiate(corridorPlane, new Vector3(transform.position.x + 0.5f, transform.position.y - 3.5f, transform.position.z - 0.5f), transform.rotation);
            mywall=Instantiate(wallTile, new Vector3(transform.position.x, transform.position.y + 1+height/2, transform.position.z - 13.5f), transform.rotation);
            mywall.transform.localScale = new Vector3(7, 9+height, 1);
            Instantiate(roofPlane, new Vector3(transform.position.x + 7f, transform.position.y + 6f+height, transform.position.z - 0.5f), Quaternion.Euler(0, 90, 0));
            setObjectDimensions();
            // Debug.Log(Path.GetFileName(filePath));
            if (Path.GetFileName(filePath) != "Door.jpg")
            {
                if (i % 2 == 0)
                {
                    myShape.myObject = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 0.5f), transform.rotation).GetComponent<TextPrefab>();
                }
                else
                {
                    myShape.myObject = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z - 12.5f), Quaternion.Euler(0, 180, 0)).GetComponent<TextPrefab>();
                }
                myShape.myObject.myText.text = text;
                // setObjectDimensions();
            }
            else
            {
                myShape.transform.Translate(0, 0, -0.25f, Space.Self);
                myShape.transform.localScale = new Vector3(4, 8, 1);
                // Debug.Log(Path.GetFileName(filePath));
                // Debug.Log(myShape.myObject.myText.text);
            }
            transform.Translate(-7f, 0f, 0f);
        }

        Instantiate(corridorPlane, new Vector3(transform.position.x + 0.5f, transform.position.y - 3.5f, transform.position.z - 0.5f), transform.rotation);
        mywall=Instantiate(wallTile, new Vector3(transform.position.x, transform.position.y + 1+height/2, transform.position.z + 0.5f), transform.rotation);
        mywall.transform.localScale = new Vector3(7, 9+height, 1);
        mywall=Instantiate(wallTile, new Vector3(transform.position.x, transform.position.y + 1+height/2, transform.position.z - 13.5f), transform.rotation);
        mywall.transform.localScale = new Vector3(7, 9+height, 1);
        mywall=Instantiate(LongWallTile, new Vector3(transform.position.x - 3f, transform.position.y + 1+height/2, transform.position.z - 6.5f), Quaternion.Euler(0, 90, 0));
        mywall.transform.localScale = new Vector3(mywall.transform.localScale.x, 9+height, 1);
        Instantiate(roofPlane, new Vector3(transform.position.x + 7f, transform.position.y + 6f+height, transform.position.z - 0.5f), Quaternion.Euler(0, 90, 0));
        transform.Translate(-7f, 0f, 0f);
        Instantiate(roofPlane, new Vector3(transform.position.x + 7f, transform.position.y + 6f+height, transform.position.z - 0.5f), Quaternion.Euler(0, 90, 0));
    }


    void setObjectDimensions()
    {
        if (myShape.GetComponent<Renderer>().material.mainTexture.width / myShape.GetComponent<Renderer>().material.mainTexture.height > 10 || myShape.GetComponent<Renderer>().material.mainTexture.height / myShape.GetComponent<Renderer>().material.mainTexture.width > 10)
        {
            if (myShape.GetComponent<Renderer>().material.mainTexture.width / myShape.GetComponent<Renderer>().material.mainTexture.height > 10)
            {
                myShape.transform.localScale = new Vector3(4.0f, 1.0f, 0.1f);
            }
            if (myShape.GetComponent<Renderer>().material.mainTexture.height / myShape.GetComponent<Renderer>().material.mainTexture.width > 10)
            {
                myShape.transform.localScale = new Vector3(1.0f, 4.0f, 0.1f);
            }
        }
        else
        {
            if (myShape.GetComponent<Renderer>().material.mainTexture.width / myShape.GetComponent<Renderer>().material.mainTexture.height > 1)
            {
                myShape.transform.localScale = new Vector3(4.0f * myShape.GetComponent<Renderer>().material.mainTexture.width / myShape.GetComponent<Renderer>().material.mainTexture.height, 4.0f, 0.1f);
            }
            else
            {
                myShape.transform.localScale = new Vector3(4.0f, 4.0f * myShape.GetComponent<Renderer>().material.mainTexture.height / myShape.GetComponent<Renderer>().material.mainTexture.width, 0.1f);
            }
        }
    }
}