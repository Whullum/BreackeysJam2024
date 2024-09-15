using System;
using _Scripts.Gameplay.Execution;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Scripts.Gameplay.UI
{
    public class ComboMeter : MonoBehaviour
    {
        [FormerlySerializedAs("_label")] [SerializeField]
        private TMP_Text _message;
        
        [SerializeField]
        private string _baseText;
        
        [SerializeField]
        private string _upcommingSuffix;
        
        [SerializeField]
        private Color _upcommingColor;
        
        [SerializeField]
        private string _activeSuffix;
        
        [SerializeField]
        private Gradient _activeColor;
        
        [SerializeField]
        private int _maxCombo;

        [Inject]
        private ComboSystem _comboSystem;
        
        private void Update()
        {
            if (_comboSystem.IsComboActive)
            {
                _message.text = _baseText + _activeSuffix + _comboSystem.CurrentCombo;
                _message.color = _activeColor.Evaluate((float) _comboSystem.CurrentCombo / _maxCombo);
            }
            else if (_comboSystem.IsComboUpcoming)
            {
                _message.text = _baseText + _upcommingSuffix;
                _message.color = _upcommingColor;
            }
            else
            {
                _message.text = "";
            }
        }
    }
}