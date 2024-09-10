using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{
    private Animator anim1;

    private void Start()
    {
        anim1 = GetComponent<Animator>();
    }
    
    public void PlayerAttackAnimation(int n ) => anim1.SetTrigger("Attack" + n);
    
    public bool IsAttacking() => anim1.GetCurrentAnimatorStateInfo(0).IsTag("attack");

    public void IsIdle() => anim1.GetCurrentAnimatorStateInfo(0).IsTag("idle");
    
    
    //Animation Events

    public void GiveDamage()
    {
        Debug.Log("Give Damage");
    }
    
    
}
