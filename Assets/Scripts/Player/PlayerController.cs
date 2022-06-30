using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject invinsibleParticle;
    
    [HideInInspector] 
    public PlayerStateMachine.PlayerState playerState = PlayerStateMachine.PlayerState.Prepare;

    [SerializeField] public AudioClip win, death, idestory, destory, bounce;

    public int currentObstacleNumber;
    public int totalObstacleNumber;
    
    private bool _ishit ;
    private Rigidbody _rigidbody;
    private float _currentTime;
    private bool _gameState;
    private bool _invinsableState;
    
    private LevelSpawner _levelSpawner;

    
    void Awake()
    {
        _levelSpawner = transform.parent.GetComponent<LevelSpawner>();
        
    }
    void Start()
    {
        totalObstacleNumber = FindObjectsOfType<ObstacleController>().Length;  // Objelerin tamamını yakalama
        _gameState = true;
        _rigidbody = GetComponent<Rigidbody>();
        currentObstacleNumber = 0;
        // Debug.Log("Singleton Value :" + Singletonn.Instance.singletonValue);
    }

    void Update()
    {
        PlayerStates();
        InvinsableSlider();
    }
    
    public void PlayerStates()
    {
        if (playerState == PlayerStateMachine.PlayerState .Playing)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _ishit = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _ishit = false;
            }
        }

        if (playerState == PlayerStateMachine.PlayerState.Finish)
        {
            EventManager.onLevelSuccess.Invoke();
        }
        if (playerState == PlayerStateMachine.PlayerState.Died)
        {
            EventManager.onLevelFailed.Invoke();
        }
    }
    void FixedUpdate()
    {
        if (!_gameState) return;
        MouseClick();
    }
    public void MouseClick()
    {
        if (_ishit)
        {
            _rigidbody.velocity = new Vector3(0f, -100f * Time.deltaTime * 7f, 0f);
        }
    }
    
    private void OnCollisionStay(Collision collision) // temas ettiği sürece gireceği için kullanırız.
    {
        if (_ishit != (true))
        {
            _rigidbody.velocity = new Vector3(0f, 30f * Time.fixedDeltaTime * 7f, 0f);
            AudioManager.Instance.PlayClipFx(bounce,0.5f);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!_gameState) return;
        if (_ishit != (true))
        {
            _rigidbody.velocity = new Vector3(0f, 50f * Time.fixedDeltaTime * 5f, 0f);
        }
        else
        {
            if(collision.gameObject.CompareTag("enemy"))
            {
                collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles(); // Parçalama methodu
                ScoreManager.Instance.IncreaseScore(1); // 1 artsın
                AudioManager.Instance.PlayClipFx(destory,0.5f);
                currentObstacleNumber++;
                GameUI.Instance.InvictableObj.SetActive(true);
                // Destroy(collision.transform.parent.gameObject);
            }
            else if((collision.gameObject.CompareTag("plane") || collision.gameObject.CompareTag("enemy")) && _currentTime >= 0 && invinsibleParticle.activeInHierarchy)
            {
                _invinsableState = true;
                Debug.Log("Invictable current neden girmiyor..... : " + _currentTime);
                collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles(); // Parçalama methodu
                ScoreManager.Instance.IncreaseScore(2); // 2 artsın
                AudioManager.Instance.PlayClipFx(idestory,0.5f);
                currentObstacleNumber++;
            }
            else if (collision.gameObject.CompareTag("plane"))  // Siyah objeler
            {
                playerState = PlayerStateMachine.PlayerState.Died;
                AudioManager.Instance.PlayClipFx(death,0.5f);
                _gameState = false;

                #region Comment Line
                // GameManager.instance._loseCanvas.SetActive(true);
                // CameraManager.instance.FailedCamManager();
                // GameUI.Instance.CloseInvincableTimer();
                // AudioManager.Instance.SoundOnOff();
                // ScoreManager.Instance.ResetScore();
                //Destroy(gameObject);
                #endregion
     
            }
            else if (collision.gameObject.CompareTag("win"))
            {
                playerState = PlayerStateMachine.PlayerState.Finish;
                AudioManager.Instance.PlayClipFx(win,0.5f);
                
                #region Comment Line
                // CameraManager.instance.SuccessCamManager();
                // GameManager.instance._winCanvas.SetActive(true);
                // FindObjectOfType<CameraManager>().SuccessCamManager(); 
                //GameObject.FindWithTag("winCanvas").SetActive(true);
                #endregion

            }
        }
        //Debug.Log("curretObstacle" + currentObstacleNumber);
        GameUI.Instance.LevelSliderFill(currentObstacleNumber / (float)totalObstacleNumber);
    }
    
    
    public void InvinsableSlider()
    {
        if (_ishit)
        {
            if (GameUI.Instance.InvictableObj.activeInHierarchy && !_invinsableState)
            {
                if (_currentTime >= 1)
                {
                    invinsibleParticle.SetActive(true);
                    _currentTime = 1;
                    _invinsableState = true;
                    return;
                }
                
                _currentTime += Time.deltaTime*1.5f;
                GameUI.Instance.InvictableSlider.color = Color.white;
                GameUI.Instance.InvictableSlider.fillAmount = _currentTime  / 1f;
                Debug.Log("!invinsibleParticle" +_currentTime);
            }
        
            else if (GameUI.Instance.InvictableObj.activeInHierarchy && _invinsableState)
            {
                if (_currentTime <= 0)
                {
                    invinsibleParticle.SetActive(false);
                    _currentTime = 0;
                    GameUI.Instance.InvictableObj.SetActive(false);
                    _invinsableState = false;
                    return;
                }
            
                _currentTime -= Time.deltaTime*0.3f;
                Debug.Log("invinsibleParticle : "+ _currentTime);
                GameUI.Instance.InvictableSlider.color = Color.red;
                GameUI.Instance.InvictableSlider.fillAmount = _currentTime;
            }
        }
        else
        {
            if (_currentTime <= 0)
            {
                invinsibleParticle.SetActive(false);
                _currentTime = 0;
                GameUI.Instance.InvictableObj.SetActive(false);
                return;
            }
            
            _currentTime -= Time.deltaTime*0.3f;
            GameUI.Instance.InvictableSlider.fillAmount = _currentTime  / 1f;
        }
    }

    
}
