using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
   [CreateAssetMenu(fileName = "PigeonSO")]
   public class PigeonSo : ScriptableObject
   {
      [SerializeField] private List<Pigeon> _pigeons = new List<Pigeon>();
   }
}