using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletonn : MonoBehaviour
{
   private static Singletonn _instance;
   public int singletonValue = 10;

   public static Singletonn Instance   // Bir yerden örneği alınmışsa o objeyi otomatik referans alarak işlem yapılır.
   {
      get
      {
         if (_instance == null) 
         {
            _instance = FindObjectOfType<Singletonn>();
            if (_instance == null)
            {
               _instance = new GameObject().AddComponent<Singletonn>();
            }
         }
         return _instance;
      }
   }

   // void Awake()
   // {
   //    _instance = this;
   // }
      
}
