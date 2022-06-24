using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public GameObject _winCanvas;

   void Awake()
   {
      instance = this;
   }
   
   public void ButtonClick()
   {
      EventManager.onLevelSuccess.Invoke();
      _winCanvas.SetActive(false);
   }
}
