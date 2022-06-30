using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    [HideInInspector]
    public PlayerController playerController;
    public GameObject winprefab;
    public GameObject[] obstaclePrefab = new GameObject[4]; 
    public GameObject[] obstacleModel;
    public GameObject playerPrefab;
    public GameObject rotateManagerPrefab;
    public GameObject cylinderObject;
    public Material plateMat;
    public Material baseMat;
    public int levelNo;
    
    private GameObject _rotateManagerParentObject;
    private GameObject _myPlayerPrefab;
    private float _obstaclePosY;
    private float _obstacleRotY;
    private int _randomNumber;
    private int _numberOfObstacle;
    

    private void Awake()
    {
        CreateRotateObject();
        RandomObstacleGenerator();
        CreatePlayer();
    }

    void Start()
    {
        levelNo = PlayerPrefs.GetInt("Level", 0);
        LevelingSystem();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            plateMat.color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            // Bu bizim obstacle objemizin içerisinde olan material
            baseMat.color = plateMat.color + Color.gray;
            _myPlayerPrefab.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = plateMat.color + baseMat.color;
            // Bu renkleri playerdada değişsin
        }
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

    private void LevelingSystem()
    {
        NumberOfObstacleToBeCreated();
        
        for (int i = 0; i < _numberOfObstacle; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (levelNo >= 0 && levelNo < 5) // içerisine atılan o 4 objeleri random bir şekilde oluşturuyoruz.
                {
                    CreateObstaclePrefab(0, 2, 0);
                }

                if (levelNo >= 5 && levelNo < 10)
                {
                    CreateObstaclePrefab(0, 3,0);
                }
                if (levelNo >= 10 && levelNo < 20)
                {
                    CreateObstaclePrefab(0, 3,1);

                }
                if (levelNo >= 20 && levelNo < 25)
                {
                    CreateObstaclePrefab(1, 4,Random.Range(1, 3));
                
                }
                if (levelNo >= 25 && levelNo < 30)
                {
                    CreateObstaclePrefab(1, 3,Random.Range(1, 4));
                }
                if (levelNo >= 30 && levelNo < 40)
                {
                    CreateObstaclePrefab(2, 4,Random.Range(1, 4));
                }
            }
        }
        CreateWinPrefab();
        
    }

    private void NumberOfObstacleToBeCreated()
    {
        #region NumberOfObstacleToBeCreated
        if (levelNo >= 0 && levelNo < 5)
        {
            _numberOfObstacle = 5;
            winprefab.transform.position = new Vector3(0f, -9.95f, 0f);
            cylinderObject.transform.position = new Vector3(0f, -2.54f, 0.05f);
            cylinderObject.transform.localScale = new Vector3(0.9f, -7.4f, 0.92f);
        }
        else if (levelNo >= 5 && levelNo < 20)
        {
            _numberOfObstacle = 10;
            winprefab.transform.position = new Vector3(0f, -20f, 0f);
            cylinderObject.transform.position = new Vector3(0f, -7.8f, 0.05f);
            cylinderObject.transform.localScale = new Vector3(0.9f, -12.8f, 0.92f);
        }

        else if (levelNo >= 20 && levelNo < 30)
        {
            _numberOfObstacle = 15;
            winprefab.transform.position = new Vector3(0f, -30f, 0f);
            cylinderObject.transform.position = new Vector3(0f, -12.5f, 0.05f);
            cylinderObject.transform.localScale = new Vector3(0.9f, -17.67f, 0.92f);
        }
        else if (levelNo >= 25 && levelNo < 30)
        {
            _numberOfObstacle = 15;
            winprefab.transform.position = new Vector3(0f, -30f, 0f);
            cylinderObject.transform.position = new Vector3(0f, -12.5f, 0.05f);
            cylinderObject.transform.localScale = new Vector3(0.9f, -17.67f, 0.92f);
        }
        else if (levelNo >= 30 && levelNo < 40)
        {
            _numberOfObstacle = 20;
            winprefab.transform.position = new Vector3(0f, -40f, 0f);
            cylinderObject.transform.position = new Vector3(0f, -17.5f, 0.05f);
            cylinderObject.transform.localScale = new Vector3(0.9f, -23.38f, 0.92f);
        }
        #endregion
    }

    private void CreateObstaclePrefab(int randomNumber1, int randomNumber2,int randomRotateNumber)
    {
        Instantiate(obstaclePrefab[Random.Range(randomNumber1,randomNumber2)],new Vector3(0f,_obstaclePosY,0f),
            Quaternion.Euler(0f,_obstacleRotY *8f+randomRotateNumber*90f,0f),_rotateManagerParentObject.transform);
                    
        _obstaclePosY -= 0.5f;  _obstacleRotY += 0.5f;
    }
    
    private void CreateWinPrefab()
    {
        Instantiate(winprefab,_rotateManagerParentObject.transform);
    }
    
    public void NextLevel()
    {
       PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
       SceneManager.LoadScene("MyGameScene");
    }
    public void RestartLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level")); // öldüğü levelin aynısı
        SceneManager.LoadScene("MyGameScene");
    }

    private void CreateRotateObject()
    {
        _rotateManagerParentObject = Instantiate(rotateManagerPrefab,transform);
        Instantiate(cylinderObject,transform);
    }
    
    private void CreatePlayer() // oluşturulan prefabın referansını aldık.
    {
        _myPlayerPrefab = Instantiate(playerPrefab,transform);
        playerController = _myPlayerPrefab.GetComponent<PlayerController>();
    }

    #region DestroyAllObject
    // public void DestroyAllChild()
    // {
    //     foreach (Transform child in transform)
    //     {
    //        Destroy(child.gameObject);
    //     }
    //
    //     ResetPos();
    // }
    #endregion

}
 