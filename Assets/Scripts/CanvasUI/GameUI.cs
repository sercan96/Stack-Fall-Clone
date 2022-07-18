using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    private static GameUI _instance;

    public static GameUI Instance
    {
        get => _instance;
    }

    public Image levelSlider;
    public Image currentLevelImage;
    public Image nextLevelImage;
    
    public GameObject InvictableObj;
    public Image InvictableSlider;

    // public GameObject SettingButton;
    public GameObject SoundOnOffButton;
    public GameObject HomeUI;
    public GameObject tapToPlayObj;

    public LevelSpawner levelSpawner;
    private Material _playerMaterial;
    private PlayerController _playerController;
    private bool _buttonSettingBo;
    public  Text currentLevelText;
    public Ease easyType;
    public GameObject winparticle;
    
    public bool isWin = true;

    void OnEnable()
    {
        EventManager.onLevelSuccess += CloseInvincableTimer;
        EventManager.onLevelFailed += CloseInvincableTimer;
        EventManager.onLevelSuccess += CurrentLevelTxT;
        EventManager.onLevelFailed += CurrentLevelTxT;
        EventManager.onLevelSuccess += WinParticle;
    }

    void OnDisable()
    {
        EventManager.onLevelSuccess -= CloseInvincableTimer;
        EventManager.onLevelFailed -= CloseInvincableTimer;
        EventManager.onLevelSuccess -= CurrentLevelTxT;
        EventManager.onLevelFailed -= CurrentLevelTxT;
        EventManager.onLevelSuccess -= WinParticle;
    }
    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        TapToPlayTextScale();
        _playerController = levelSpawner.playerController;

        _playerMaterial = levelSpawner.playerController.transform.GetChild(0).GetComponent<MeshRenderer>().material;
        levelSlider.transform.GetComponent<Image>().color = _playerMaterial.color + Color.gray;
        currentLevelImage.color = _playerMaterial.color;
        nextLevelImage.color = _playerMaterial.color;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !IgnoreUI() &&
            _playerController.playerState == PlayerStateMachine.PlayerState.Prepare)
        {
            _playerController.playerState = PlayerStateMachine.PlayerState.Playing;
            HomeUI.SetActive(false);
        }
    }

    void MakeSingleton()
    {
        if (_instance != null) Destroy(gameObject);
        else
        {
            _instance = this;
        }
    }

    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void SettingShow()
    {
        _buttonSettingBo = !_buttonSettingBo;
        SoundOnOffButton.SetActive(_buttonSettingBo);
    }

    public bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        #region Comment Line
        // for (int i = 0; i < raycastResults.Count; i++)
        // {
        //     if (raycastResults[i].gameObject.GetComponent<IgnoreGameUI>() != null)
        //     {
        //         raycastResults.RemoveAt(i);
        //         i++;
        //     }
        // }
        #endregion
        
        return raycastResults.Count > 0;
    }

    public void TapToPlayTextScale()
    {
        tapToPlayObj.transform.DOScale(4, 0.3f).SetEase(easyType).
            OnComplete(() => tapToPlayObj.transform.DOScale(3, 0.3f).SetEase(easyType).OnComplete(() => TapToPlayTextScale()));
    }

    public void CloseInvincableTimer()
    {
        InvictableObj.SetActive(false);
    }

    public void CurrentLevelTxT()
    {
        currentLevelText.text = "Level " + PlayerPrefs.GetInt("Level"); // O anki leveli al.
    }
    
    public void WinParticle()
    {
        Vector3 dir = new Vector3(0f, 3f, 0f);
        if (isWin)
        {
            Instantiate(winparticle,levelSpawner.winprefab.transform.position + dir,Quaternion.identity,levelSpawner.transform);
            isWin = false;
        }
    }
}
             
