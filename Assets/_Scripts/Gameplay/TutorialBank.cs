using UnityEngine;

namespace _Scripts.Gameplay
{
    public class TutorialBank : MonoBehaviour
    {
        [SerializeField] [TextArea(3, 10)]
        private string[] _tutorials;

        private int _currentTutorialIndex;

        public bool TutorialAvailable => _currentTutorialIndex < _tutorials.Length;
        
        public string CurrentTutorial =>  TutorialAvailable ? _tutorials[_currentTutorialIndex] : "";
        
        public void SwitchToNext()
        {
            _currentTutorialIndex++;
        }
    }
}