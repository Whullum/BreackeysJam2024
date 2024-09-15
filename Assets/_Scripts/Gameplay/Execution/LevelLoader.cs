using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Gameplay.Execution
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        private List<LevelSource> _levels;

        private int _currentLevelID = 0;    

        public LevelSource CurrentLevel
        {
            get
            {
                int levelToReturn = _currentLevelID;
                if (_levels.Count == 0)
                {
                    return null;
                }
                if (_currentLevelID >= _levels.Count || _currentLevelID < 0)
                {
                    levelToReturn = 0;
                }
                return _levels[levelToReturn];
            }
        }

        private void Start()
        {
            
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }

        public void ReloadLevel()
        {
            LoadLevel(_currentLevelID);
        }
        public void LoadNextLevel()
        {
            LoadLevel(_currentLevelID+1);
        }

        public void LoadLevel(int levelID)
        {
            if (_levels.Count == 0)
            {
                Debug.LogError("There are no levels to load!");
                return;
            }
            if ((_currentLevelID >= _levels.Count) || (levelID < 0))
            {
                Debug.LogError("Level out of range!");
                LoadMainMenu();
                return;
            }
            _currentLevelID = levelID;
            LoadLevel();
        }

        private void LoadLevel()
        {
            // if we decide to do a loading screen
            var levelLoad = SceneManager.LoadSceneAsync(1);
            //levelLoad.completed += SetupLevel;
        }

        

    }
}