using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Target : MonoBehaviour
{
   public event Action<Target> OnDestroyed;

   private void OnDestroy()
   {
     OnDestroyed?.Invoke(this);
   }

}
