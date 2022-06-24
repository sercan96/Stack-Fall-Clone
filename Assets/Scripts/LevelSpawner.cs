using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [HideInInspector]
    public PlayerController playerController;
    public GameObject winprefab;
    public GameObject[] obstaclePrefab = new GameObject[4]; 
    public GameObject[] obstacleModel;
    public GameObject playerPrefab;
    public GameObject rotateManagerPrefab;
    private GameObject _rotateManagerParentObject;
    public GameObject cylindeObject;

    private float _obstaclePosY;
    private float _obstacleRotY;
    private int _randomNumber;
    public int levelNo;
    private int randomNumber;

    void OnEnable()
    {
        EventManager.onLevelSuccess += DestroyAllChild;
        EventManager.onLevelSuccess += CreateRotateObject;
        EventManager.onLevelSuccess += LevelingSystem;
        EventManager.onLevelSuccess += RandomObstacleGenerator;
        EventManager.onLevelSuccess += IncreaseLevelNo;
        EventManager.onLevelSuccess += CreatePlayer;
    }
    
    void OnDisable()
    {
        EventManager.onLevelSuccess -= DestroyAllChild;
        EventManager.onLevelSuccess -= CreateRotateObject;
        EventManager.onLevelSuccess -= LevelingSystem;
        EventManager.onLevelSuccess -= RandomObstacleGenerator;
        EventManager.onLevelSuccess -= IncreaseLevelNo;
        EventManager.onLevelSuccess -= CreatePlayer;

    }
    
    private void Awake()
    {
        CreateRotateObject();
        RandomObstacleGenerator();
        LevelingSystem();
        CreatePlayer();
    }
    
    private void RandomObstacleGenerator()
    {
        _randomNumber = Random.Range(0, 4);
        switch (_randomNumber)
        {
            case 0:
                for (var i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i]; // ilk 4 objeyi aktarıyoruz model olarak.
                }
                break;
            case 1:
                for (var i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i+4];
                }
                break;
            case 2 :
                for (var i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i+8];
                }
                break;
            case 3 :
                for (var i = 0; i < 4; i++)
                {
                    obstaclePrefab[i] = obstacleModel[i+12]; // 12.elemanını 0. eleman olarak ekle
                }
                break;
        }
    }

    public void LevelingSystem()
    {
       
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (levelNo >= 0 && levelNo < 5) // içerisine atılan o 4 objeleri random bir şekilde oluşturuyoruz.
                {
                    CreateObstaclePrefab(0, 2);
                }

                if (levelNo >= 5 && levelNo < 10)
                {
                    CreateObstaclePrefab(1, 3);
                }
                if (levelNo >= 10 && levelNo < 20)
                {
                    CreateObstaclePrefab(0, 4);

                }
                if (levelNo >= 20 && levelNo < 25)
                {
                    CreateObstaclePrefab(1, 4);
                
                }
                if (levelNo >= 25 && levelNo < 30)
                {
                    CreateObstaclePrefab(2, 4);
                }
                if (levelNo >= 30 && levelNo < 40)
                {
                    CreateObstaclePrefab(3, 4);
                }
            }
        }
        CreateWinPrefab();
        
    }

    private void CreateObstaclePrefab(int randomNumber1, int randomNumber2)
    {
        randomNumber = Random.Range(1, 4);
                    
        Instantiate(obstaclePrefab[Random.Range(randomNumber1,randomNumber2)],new Vector3(0f,_obstaclePosY,0f),
            Quaternion.Euler(0f,_obstacleRotY *8f+randomNumber*90f,0f),_rotateManagerParentObject.transform);
                    
        _obstaclePosY -= 0.5f;  _obstacleRotY += 0.5f;
    }
    
    private void CreateWinPrefab()
    {
        Instantiate(winprefab, new Vector3(0f, -8f, 0f), Quaternion.identity, transform);
    }
    
    public void IncreaseLevelNo()
    {
        levelNo++;
    }

    public void CreateRotateObject()
    {
        _rotateManagerParentObject = Instantiate(rotateManagerPrefab,transform);
        Instantiate(cylindeObject,transform);
    }

    public void DestroyAllChild()
    {
        foreach (Transform child in transform)
        {
           Destroy(child.gameObject);
        }

        ResetPos();
    }

    private void ResetPos()
    {
        _obstaclePosY = 0;
        _obstacleRotY = 0;
    }
    
    public void CreatePlayer() // oluşturulan prefabın referansını aldık.
    {
        GameObject myPlayerPrefab = Instantiate(playerPrefab,transform);
        playerController = myPlayerPrefab.GetComponent<PlayerController>();
    }
    
}
