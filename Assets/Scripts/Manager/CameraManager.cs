using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    
    public LevelSpawner levelSpawner;
    private Transform _playerTransform;
    public CinemachineVirtualCamera gameplayCam;
    public CinemachineVirtualCamera successCam;
    public CinemachineVirtualCamera failedCam;

    void OnEnable()
    {
        EventManager.onLevelSuccess += SuccessCamManager;
        EventManager.onLevelFailed += RunDelayToFailedCamManager;
    }

    void OnDisable()
    {
        EventManager.onLevelSuccess -= SuccessCamManager;
        EventManager.onLevelFailed -= RunDelayToFailedCamManager;
    }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _playerTransform = levelSpawner.playerController.transform; // Prefab objeyi yakaladık.
        FollowPlayerWithCam();
    }

    private void FollowPlayerWithCam()
    {
        if (_playerTransform != null) 
        {
            _playerTransform = levelSpawner.playerController.transform;
        }
        gameplayCam.Follow = _playerTransform;
        gameplayCam.LookAt = _playerTransform;
    }
    
    private void GameplayCamManager()
    {
        gameplayCam.Priority = 12;
        successCam.Priority = 10;
        failedCam.Priority = 10;
    }
    private void SuccessCamManager()
    {
        successCam.Priority = 12;
        gameplayCam.Priority = 10;
        failedCam.Priority = 10;
        
        successCam.Follow = _playerTransform;
        successCam.LookAt = _playerTransform;
    }
    private void FailedCamManager()
    {
        failedCam.Priority = 12;
        successCam.Priority = 10;
        gameplayCam.Priority = 10;
    }
    
    private void RunDelayToFailedCamManager()
    {
        Invoke("FailedCamManager",0.5f);
    }
}
