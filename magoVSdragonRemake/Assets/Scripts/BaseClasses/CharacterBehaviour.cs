using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using JordiArray;

public class CharacterBehaviour : MonoBehaviour
{   
    GameManager gameManager; 
    AudioManager audioManager;

    Collider2D myCollider2D;

    protected float startSpeed;

    protected float currentSpeed;        

    //protected string[] myStartKeys = new string[4];
    //protected string[] myCurrentKeys = new string[4];
    
    protected string startUp;
    protected string startRight;
    protected string startDown; 
    protected string startLeft;

    protected string currentUp;
    protected string currentRight;
    protected string currentDown; 
    protected string currentLeft;
    
    protected float maxSpeedAllowed;
    
    bool canMove = false;    

    //[SerializeField]
    List<Vector3> positions = new List<Vector3>();

    int positionsIndex;

    bool doRewind = false;

    string myStartTag;   

    int[] randomArray = new int[4]; 


    protected virtual void Start()
    {
        //Debug.Log("start " + gameObject.name);                               
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;
        myCollider2D = GetComponent<Collider2D>();
        myStartTag = gameObject.tag;        
    }

    //protected virtual void Update()
    void Update()
    {
        //Debug.Log("update " + gameObject.name);
        if(gameManager.GetIsPlaying())
        {
            Move();                                  
        }       
    }

    void FixedUpdate()
    {
        //Debug.Log("fixed update " + gameObject.tag);
        if (doRewind)
        {
            DoRewind();
        }
        else
        {
            RecordMovement();
        }
    }

    void BeReadyToPlay()
    {
        SetCanMove(true);
    }

    void Move()
    {
        //Debug.Log("canMove= " + canMove);
        if(!canMove)
        {
            return;
        }
        //Debug.Log("move " + gameObject.name);
        if (Input.GetKey(currentUp))
        {
            //Debug.Log("up");
            transform.position += Vector3.up * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(currentRight))
        {
            transform.position += Vector3.right * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(currentDown))
        {
            transform.position += Vector3.down * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(currentLeft))
        {
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        }
    }       

    void RecordMovement()
    {
        positions.Add(transform.position);        
    }

    internal void ResetSpeed()
    {
        currentSpeed = startSpeed;
    }

    internal virtual void ChangeControls()
    {
        //Debug.Log("Change Controls");       
        RandomizeControls();
    }    

    protected void RandomizeControls()
    {
        //Debug.Log("randomize controls");
        
        string[] newKeys = new string[4];
        newKeys[0] = currentUp;
        newKeys[1] = currentRight;          
        newKeys[2] = currentDown;        
        newKeys[3] = currentLeft;

        randomArray = ArrayLibrary.GenerateIrrepetibleArray(randomArray, 0, randomArray.Length);
        /*
        foreach(int randomIndex in randomArray)
        {
            Debug.Log("random index= " + randomIndex);
        }
        */
        currentUp = newKeys[randomArray[0]];
        currentRight = newKeys[randomArray[1]];
        currentDown = newKeys[randomArray[2]];
        currentLeft = newKeys[randomArray[3]];
    }        

    internal void Rewind()
    {
        EnableCollider(false);
        EnableCollider(true);
        positionsIndex = positions.Count - 2;        
        SetCanMove(false);
        SetDoRewind(true);    
        if(gameObject.CompareTag(EnumManager.Tags.Mage.ToString()))
        {
            SetTag(EnumManager.Tags.Dragon.ToString());
        }
        else
        {
            SetTag(EnumManager.Tags.Mage.ToString());
        }
    }

    void EnableCollider(bool _newState)
    {
        myCollider2D.enabled = _newState;
    }

    void DoRewind()
    {
        if(positionsIndex < 0)
        {
            return;
        }                
        //Debug.Log("positions index= " + positionsIndex);
        transform.position = positions[positionsIndex];
        positions.RemoveAt(positionsIndex);    
        positionsIndex -= 1;    
    }

    internal void ResetControls()
    {
        currentUp = startUp;
        currentRight = startRight;
        currentDown = startDown; 
        currentLeft = startLeft;
    }  

    internal void ResetRewind()
    {
        SetCanMove(true);
        SetDoRewind(false);
        ResetTag();
    }

    void ResetTag()
    {
        gameObject.tag = myStartTag;
    }

    /*
    protected void SetMyStartKeys(string _up, string _right, string _down, string _left)
    {
        currentUp = _up;
        currentRight = _right;
        currentDown = _down;
        currentLeft = _left;
    }
    */

    /*
    protected void SetMyKeys(string _up, string _right, string _down, string _left)
    {
        myCurrentKeys[0] = _up;
        myCurrentKeys[1] = _right;
        myCurrentKeys[2] = _down;
        myCurrentKeys[3] = _left;
    }
    */

    internal void ChangeCurrentSpeed(float _amount)
    {
        //Debug.Log("changeCurrentSpeed");
        currentSpeed += _amount;
        //Debug.Log("currentSpeed= " + currentSpeed);
    }

    #region Getters&Setters  
    internal float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    internal void SetCurrentSpeed(float _newValue)
    {
        //Debug.Log("changeCurrentSpeed");
        //Debug.Log("_newValue= " + _newValue);

        if(_newValue > maxSpeedAllowed)
        {
            _newValue = maxSpeedAllowed;
        }
        currentSpeed = _newValue;
        //Debug.Log("currentSpeed= " + currentSpeed);
    }

    void SetCanMove(bool _newState)
    {
        canMove = _newState;
    }

    void SetDoRewind(bool _newState)
    {
        doRewind = _newState;
    }

    void SetTag(string _newTag)
    {
        gameObject.tag = _newTag;
    }
    #endregion

    void OnEnable()
    {
        //Debug.Log("on enable " + gameObject.name);
        GameplaySceneController.onGameStarted += BeReadyToPlay;
    }

    void OnDisable()
    {
        GameplaySceneController.onGameStarted -= BeReadyToPlay;
    }
    
}
