
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public GameObject _winCanvas;
   public GameObject _loseCanvas;
   public LevelSpawner levelSpawner;

   void OnEnable()
   {
      EventManager.onLevelSuccess += OpenWinCanvas;
      EventManager.onLevelFailed += OpenLoseCanvas;
   }
   void OnDisable()
   {
      EventManager.onLevelSuccess -= OpenWinCanvas;
      EventManager.onLevelFailed -= OpenLoseCanvas;
   }
   void Awake()
   {
      instance = this; // İNSTANCE BU SCRİPTİN BAĞLI OLDUĞU OBJEYE EŞİTTİR!!
   }
   
   private void WinButtonClick()
   {
      _winCanvas.SetActive(false);
      levelSpawner.NextLevel();
      // levelSpawner.playerController.playerState = PlayerStateMachine.PlayerState.Finish;
       //FindObjectOfType<PlayerController>().playerState = PlayerStateMachine.PlayerState.Finish;
   }
   private void LoseButtonClick()
   {
      _loseCanvas.SetActive(false);
      levelSpawner.RestartLevel();
      // levelSpawner.playerController.playerState = PlayerStateMachine.PlayerState.Died;
      //FindObjectOfType<PlayerController>().playerState = PlayerStateMachine.PlayerState.Died;
   }

   public void OpenWinCanvas()
   {
      _winCanvas.SetActive(true);
   }
   
   public void OpenLoseCanvas()
   {
      _loseCanvas.SetActive(true);
   }
}
