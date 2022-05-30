using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    
    public class EndGame : MonoBehaviour {

        LevelText _levelText;

        void Awake()
        {
            GetLevelText();
        }

        void GetLevelText()
        {
            _levelText = FindObjectOfType<LevelText>();
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_levelText == null) GetLevelText();
                _levelText.TriggerEndGame();
            }
        }
    }
}
