using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public bool isGameStarted;
    public GameObject cube;
    public float rotationSpeed = 100.0f;

    //Ratation direction
    private float horizontalInput;
    private float verticalInput;

    //For Default  size of Cube
    private Quaternion original;

    //Timer
    public float timeRemaining = 80;
    private float time;
    
    //UI Timer
    public TextMeshProUGUI timeText;



    // Start is called before the first frame update
    void Start()
    {
        time = timeRemaining;
        original = cube.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {


        //Starting and Exit Game using isGameStarted variable
        if (Input.GetAxis("Submit") >0 || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKey("enter"))
        {
            //Enable Cube and start  Timer
            isGameStarted = true;
            timeRemaining = time;          
        }
        else if (Input.GetKeyDown(KeyCode.Q) || timeRemaining ==0)
        {
            isGameStarted = false;
        }



        //When  isGameStarted == true
        if (isGameStarted)
        {
            //Chacking Timer 
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("Time has run out!");
                timeRemaining = 0;
            }


            //Assigning Input Values to Arrows keys
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            
            
            //Cube enable
            cube.SetActive(true);


            //Chacking Horizontal input value Left , Right Ratation
            if (horizontalInput < 0)
            {
                cube.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
            }
            else if (horizontalInput > 0)
            {
                cube.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime, Space.World);
            }


            //Vertical Horizontal input value for Up , Down Ratation
            if (verticalInput > 0)
            {
                cube.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.World);
            }
            else if (verticalInput < 0)
            {
                cube.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime, Space.World);
            }


            //Increasing and Descreasing Size of Cube
            if (Input.GetButton("Space"))
            {
               cube.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);

            }
            else if (!Input.GetButton("Space"))
            {
                if (cube.transform.localScale != new Vector3(1, 1, 1))
                {
                  cube.transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
                }
            }

            //Displaying Timer 
            DisplayTime(timeRemaining);

        }
        //When  isGameStarted not  true
        else if (!isGameStarted)
        {
            //Reset cube size
            cube.transform.localScale = new Vector3(1, 1, 1);
            //Default rotation of cube 
            cube.transform.rotation = original;
            cube.SetActive(false);
            timeRemaining = 0;
            DisplayTime(timeRemaining);
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        //Converting time to minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
