using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PatternManager : MonoBehaviour
{

    private bool isChevronOn=false;
    private bool isVectionOn = false;
    private bool isMangaOn = false;
    private bool isWarpOn = false;
    //private bool isSphereOn = true;

    public GameObject chevronObjMain;

    public GameObject topChevron;
    public GameObject bottomChevron;
   /* public GameObject rightChevron;
    public GameObject leftChevron;*/

    public GameObject mangaObj;

    public GameObject vectionObj;

    public GameObject sphereObj;

    public GameObject warpObj;

    private readonly float INCREMENT = 0.001f;
    private readonly float DECREMENT = -0.001f;

    private readonly float MIN_SPEED = 0.2f;
    private readonly float MAX_SPEED = 20.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RespondToKeyInput();
    }

    void RespondToKeyInput()
    {

        if (Input.GetKeyUp("i"))
        {
            isChevronOn = !isChevronOn;
            //WriteTime("Chevron");

            if (chevronObjMain != null)
            {
                chevronObjMain.gameObject.SetActive(isChevronOn);
            }

            if (topChevron != null)
            {
                topChevron.gameObject.SetActive(isChevronOn);
            }
            if (bottomChevron != null)
            {
                bottomChevron.gameObject.SetActive(isChevronOn);
            }
           /* if (leftChevron != null)
            {
                leftChevron.gameObject.SetActive(isChevronOn);
            }
            if (rightChevron != null)
            {
                rightChevron.gameObject.SetActive(isChevronOn);
            }*/
        }

        if (Input.GetKeyUp("o"))
        {
            isVectionOn = !isVectionOn; 
            //WriteTime("Manga");

            if (vectionObj != null)
            {
                vectionObj.gameObject.SetActive(isVectionOn);
            }
        }

        if (Input.GetKeyUp("p"))
        {
            isMangaOn = !isMangaOn;
            //WriteTime("Vection");

            if (mangaObj != null)
            {
                mangaObj.gameObject.SetActive(isMangaOn);
            }
        }

        if (Input.GetKeyUp("u"))
        {
            isWarpOn = !isWarpOn;
            //WriteTime("Vection");

            if (warpObj != null)
            {
                warpObj.gameObject.SetActive(isWarpOn);
            }
        }

        if (Input.GetKey("w")) //x up
        {
            if (sphereObj != null)
            {
                sphereObj.transform.Translate(INCREMENT, 0.0f, 0.0f);
            }
        }

        if (Input.GetKey("s")) //x down
        {
            if (sphereObj != null)
            {
                sphereObj.transform.Translate(DECREMENT, 0.0f, 0.0f);
            }
        }

        if (Input.GetKey("e")) // y up
        {
            if (sphereObj != null)
            {
                sphereObj.transform.Translate(0.0f, INCREMENT, 0.0f);
            }
        }

        if (Input.GetKey("d")) // y down
        {
            if (sphereObj != null)
            {
                sphereObj.transform.Translate(0.0f, DECREMENT, 0.0f);
            }
        }

        if (Input.GetKey("r")) // z up
        {
            if (sphereObj != null)
            {
                sphereObj.transform.Translate(0.0f, 0.0f, INCREMENT*2.0f);
            }
        }

        if (Input.GetKey("f")) // z down
        {
            if (sphereObj != null)
            {
                sphereObj.transform.Translate(0.0f, 0.0f, DECREMENT * 2.0f);
            }
        }

        /*if (Input.GetKeyDown("u")) // toggle sphere
        {
            isSphereOn = !isSphereOn;

            if (sphereObj != null)
            {
                sphereObj.GetComponent<MeshRenderer>().enabled = !isSphereOn;
            }
            
        }*/


        if (Input.GetKey("h")) // faster warp
        {
            if (warpObj != null)
            {

                ParticleSystem ps = warpObj.GetComponent<ParticleSystem>();
                if (ps != null && ps.startSpeed <= MAX_SPEED )
                {
                    ps.startSpeed += INCREMENT * 2.0f;
                }
            }
        }

        if (Input.GetKey("g")) // slower warp
        {
            ParticleSystem ps = warpObj.GetComponent<ParticleSystem>();
            if (ps != null && ps.startSpeed >= MIN_SPEED)
            {
                ps.startSpeed += INCREMENT * 2.0f;
            }
        }

    }

    void WriteTime(string pattern)
    {
        string path = Path.Combine(Application.persistentDataPath, "MyFile.txt");
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(Time.time);
                sw.WriteLine(pattern);

            }
        }

        // This text is always added, making the file longer over time
        // if it is not deleted.
        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine(Time.time);
            sw.WriteLine(pattern);

        }
    }
}
