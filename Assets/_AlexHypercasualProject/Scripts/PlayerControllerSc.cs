using System.Collections;
using System.Collections.Generic;
using AlexHyperCasualGames;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerControllerSc : MonoBehaviour
{

    public static PlayerControllerSc Instance { get; private set; }
    public GameObject EquippedWeapon;
    public List<GameObject> Weapons;
    public float _runSpeed;
    public float _maxSpeed;
    public float _vSpeed;
    public float _zSpeed;


    private bool CanMoveLeft = true;
    private bool CanMoveRight = true;
    public float RoateSpeed = 1f;
    private Constants.PlayerState _characterState;
    public Animator _characterAnimator;
    private Transform _ActualModel;
    public delegate void OnGamePlay();
    public static OnGamePlay onGamePlay;
    private GameObject FinishLine;
    public ParticleSystem GameOverParticle;
    public ParticleSystem GameWonParticle;
    public ParticleSystem ChanageWeaponParticles;
    public GameObject Cart;
    private float screenCenterX;
    public int IndexWeapon;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private bool FingerDown = false;
    private Vector3 _screenPoint;
    private Vector3 _offset;
    private void Awake()
    {

        #region Singleton
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion
        //IndexWeapon = 0;
        _characterAnimator = GetComponentInChildren<Animator>();
        _ActualModel = GetComponentInChildren<Animator>().transform;
        FinishLine = GameObject.FindGameObjectWithTag("FinishLine");
        SetCharacterState(Constants.PlayerState.Idle);
        screenCenterX = Screen.width * 0.5f;
        FindObjectwithTag("Weapon");
        Weapons[IndexWeapon].gameObject.SetActive(true);
        EquippedWeapon = Weapons[IndexWeapon];

        

    }
    public void FindObjectwithTag(string _tag)
    {
        Weapons.Clear();
        Transform parent = transform;
        GetChildObject(parent, _tag);
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                Weapons.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }



    private void OnMouseDown()
    {
        //if (myState.Equals(Constants.PlayerState.Lose) || myState.Equals(Constants.PlayerState.Win) || myState.Equals(Constants.PlayerState.Idle)) return;
        if (_characterState == Constants.PlayerState.Run)
        {
            var position = gameObject.transform.position;
            _screenPoint = GameControllerSc.Instance.mainCam.WorldToScreenPoint(position);
            _offset = position - GameControllerSc.Instance.mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
        }

    }
    private void OnMouseDrag()
    {
        if (_characterState == Constants.PlayerState.Run)
        {
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z); // hardcode the y and z for your use
            var curPosition = GameControllerSc.Instance.mainCam.ScreenToWorldPoint(curScreenPoint) + _offset;
            var myTransform = transform;
            var myPos = myTransform.position;
            myPos = new Vector3(myPos.x, myPos.y, Mathf.Clamp(curPosition.z, -6f, 6f));
            transform.position = Vector3.Lerp(transform.position, myPos, _zSpeed * Time.deltaTime);
        }
       
    }
    private void OnInput()
    {
        
           

        if ((Input.GetKey(KeyCode.Space) || Input.touchCount > 0) && _characterState == Constants.PlayerState.Idle && GameControllerSc.Instance.CurrentGameState != Constants.GameState.Shopping)
        {
            Touch touch = Input.GetTouch(0);
            
            //Debug.Log("Touch position : " + touch.deltaPosition);
            if (Input.GetTouch(0).position.y < (Screen.height / 2)  )
            {
                Debug.Log("Touch position Now Play");
                onGamePlay?.Invoke();
                SetCharacterState(Constants.PlayerState.Run);
                _characterAnimator.SetBool("Run", true);


                CanvasControllerCs.Instance.SetRaceStats(this.transform.position.x, FinishLine.transform.position.x);

            }

            
            
        }

        /*
        if (Input.GetKey(KeyCode.RightArrow) && _characterState == Constants.PlayerState.Run)
        {
            
            MovementToLeft();
        }
        if (Input.GetKey(KeyCode.LeftArrow) && _characterState == Constants.PlayerState.Run)
        {
            MovementToRight();
        }
        */
        if (Input.touches.Length > 0 && _characterState == Constants.PlayerState.Run)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
                FingerDown = true;

            }
            if (FingerDown)
            {

                currentSwipe = new Vector3(t.position.x - firstPressPos.x, t.position.y - firstPressPos.y);
               
                //normalize the 2d vector
                //currentSwipe.Normalize();
                //swipe left
                if (currentSwipe.x < 0 )
                {
                    /*
                    var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z); // hardcode the y and z for your use
                    var curPosition = GameController.Instance.mainCam.ScreenToWorldPoint(curScreenPoint) + _offset;
                    var myTransform = transform;
                    var myPos = myTransform.position;
                    myPos = new Vector3(myPos.x, myPos.y, Mathf.Clamp(curPosition.z, -6f, 6f));
                    transform.position = Vector3.Lerp(transform.position, myPos, dragSpeed * Time.deltaTime);
                    */
                    
                    //transform.position = Vector3.Lerp(transform.position, myPos, _zSpeed * Time.deltaTime);
                    //MovementToRight(currentSwipe.x);
                }
                //swipe right
                if (currentSwipe.x > 0 )
                {
                    //MovementToLeft(currentSwipe.x);
                }
            }
            if (t.phase == TouchPhase.Ended)
            {
               
                FingerDown = false;

            }
        }

        








        
       
    }
    private void Update()
    {
        if (_characterState == Constants.PlayerState.Run)
        {
            (transform).Translate(_runSpeed * Time.deltaTime, _vSpeed * Time.deltaTime, 0);
            CanvasControllerCs.Instance.SetRaceBar(this.transform.position.x);
        }

        if (_characterState == Constants.PlayerState.Idle)
        {
            OnInput();
        }

        if (_characterState == Constants.PlayerState.Run)
        {
            OnInput();
            HandleMovement();
        }
    }

  
    public void HandleMovement()
    {
        
       
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection((Vector3.forward)), out hit, 2f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection((Vector3.forward)) * 2f, Color.yellow);
            Debug.Log(hit.transform.gameObject.tag);
            if (hit.transform.CompareTag("Bounds"))
            {

                CanMoveLeft = false;
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(-(Vector3.forward)), out hit, 2f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-(Vector3.forward)) * 2f, Color.yellow);
            Debug.Log(hit.transform.gameObject.tag);
            if (hit.transform.CompareTag("Bounds"))
            {

                CanMoveRight = false;
            }
        }

        // Does the ray intersect any objects excluding the player layer


    }
    private void MovementToLeft(float xTouch)
    {
        if (CanMoveLeft)
        {
            (transform).Translate(0f, 0f, (xTouch * (_zSpeed * Time.deltaTime)));

            //_ActualModel.Rotate(new Vector3(0f, -12f, 0) * Time.deltaTime * RoateSpeed);
            
            CanMoveRight = true;
        }
       

    }
    private void MovementToRight(float xTouch)
    {
        if (CanMoveRight)
        {
            (transform).Translate(0f, 0f, (xTouch * (_zSpeed*Time.deltaTime)));
            //_ActualModel.Rotate(new Vector3(0f, 12f, 0) * Time.deltaTime * RoateSpeed);
            CanMoveLeft = true;
        }
        
    }

    public void SetCharacterState(Constants.PlayerState ChangedState)
    {
        _characterState = ChangedState;
    }

    public void GameOver()
    {
        EnemyManagerSc.SharedInstance.LaughAllEnemies();
        GameOverParticle.gameObject.SetActive(true);
        
        Cart.SetActive(false);
        _characterAnimator.SetBool("Lost",true);
        SetCharacterState(Constants.PlayerState.Lose);
        GameControllerSc.Instance.SetState(Constants.GameState.WaitingForInput);
        //_ActualModel.gameObject.SetActive(false);
        StartCoroutine(BringGameOverPanel());
        
    }
    public IEnumerator BringGameOverPanel()
    {
        yield return new WaitForSeconds(2.5f);
        //GameOverParticle.gameObject.SetActive(false);
        CanvasControllerCs.Instance.GameOverCanvas();
    }


    public void ChangeWeapon()
    {
        EquippedWeapon.SetActive(false);
        ChanageWeaponParticles.Play();
        IndexWeapon++;

        EquippedWeapon = Weapons[IndexWeapon];
        EquippedWeapon.SetActive(true);
    }

}
