using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    // public LevelSpawner _levelSpawner;
    // public CameraManager cameraManager;
    //public GameObject winCanvas;
    
    private PlayerController _playerController;
    private Transform _playerTransform;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraManager.instance.SuccessCamManager();
            GameManager.instance._winCanvas.SetActive(true);
            // FindObjectOfType<CameraManager>().SuccessCamManager(); 
            //GameObject.FindWithTag("winCanvas").SetActive(true);
        }
    }       

    public void ReachToPlayerPrefabFromPrefab()
    {
        FindObjectOfType<PlayerController>().gameObject.SetActive(false);
    }


    void ReachPlayerPrefab()
    {
        //_playerController = _levelSpawner.playerController; // prefaba ulaştım
        //_playerController.gameObject.SetActive(false);
    }

    void ReachPlayerDifferentWay()
    {
       // _playerTransform = FindObjectOfType<PlayerController>().transform;
        //_playerTransform.position = Vector3.zero;
        //Debug.Log(_playerTransform.position);
    }


}
