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
        EventManager.onLevelSuccess += FollowPlayerWithCam;
        EventManager.onLevelSuccess += GameplayCamManager;
        EventManager.onLevelSuccess += ResetCamPos;
    }
    void OnDisable()
    {
        EventManager.onLevelSuccess -= FollowPlayerWithCam;
        EventManager.onLevelSuccess -= GameplayCamManager;
        EventManager.onLevelSuccess -= ResetCamPos;
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

    public void FollowPlayerWithCam()
    {
        if (_playerTransform != null) 
        {
            _playerTransform = levelSpawner.playerController.transform;
        }
        gameplayCam.Follow = _playerTransform;
        gameplayCam.LookAt = _playerTransform;
    }

    public void ResetCamPos()
    {
        gameplayCam.transform.position = new Vector3(0f, 6f, -15f);
    }
    public void GameplayCamManager()
    {
        gameplayCam.Priority = 12;
        successCam.Priority = 10;
        failedCam.Priority = 10;
    }
    public void SuccessCamManager()
    {
        successCam.Priority = 12;
        gameplayCam.Priority = 10;
        failedCam.Priority = 10;
    }
    public void FailedCamManager()
    {
        failedCam.Priority = 12;
        successCam.Priority = 10;
        gameplayCam.Priority = 10;
    }
}
