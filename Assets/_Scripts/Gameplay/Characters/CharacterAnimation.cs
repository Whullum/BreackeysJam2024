using System;
using System.Collections;
using _Scripts.Gameplay.Execution;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Characters
{
    public class CharacterAnimation : MonoBehaviour
    {
        private const string IdleState = "idle";
        private const string PunchState = "punch";
        private const string KickState = "kick";
        private const string WeaponState = "weapon";
        private const string HurtState = "hurt";
        
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _restoreTime;
        
        private CharacterMarker _marker;
        private CharacterMarker Marker => _marker ??= GetComponent<CharacterMarker>();
        
        [Inject]
        private FightPlayer _fightPlayer;

        private void Awake()
        {
            Marker.Attack.Punched += () => PlayState(PunchState);
            Marker.Attack.Kicked += () => PlayState(KickState);
            Marker.Attack.UsedWeapon += () => PlayState(WeaponState);
            Marker.Life.TookHit += () => PlayState(HurtState);
        }

        private void Update()
        {
            _animator.speed = _fightPlayer.IsPlanning ? 0 : 1;
        }

        private void PlayState(string state, bool persistent = false)
        {
            Debug.Log($"{gameObject} {state}");
            _animator.Play(state);
            StopAllCoroutines();
            if (persistent)
                return;
            StartCoroutine(RestoreState());
        }

        private IEnumerator RestoreState()
        {
            yield return new WaitForSeconds(_restoreTime);
            PlayState(IdleState, true);
        }
    }
    
}