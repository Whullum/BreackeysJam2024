using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected float health;
    
    //Rendering
    protected SpriteRenderer[] sprites;
    
    //Health
    public bool IsDead => health <= 0;
    
    //Spots
    private Spot currentSpot;
    private bool hasSpotToAlign; //For null check Optimization
    
    [Inject]
    private SpotManager spotManager;

    [Inject]
    public void Construct(SpotManager sm)
    {
        this.spotManager = sm;
        
    } 

    
    // Start is called before the first frame update
    protected void InitCharacter()
    {
        Debug.Log($"start");
        
        //Spot manager is not injected, idk why
        sprites = GetComponentsInChildren<SpriteRenderer>();
        Spot newSpot = spotManager.GetRandomAvailableSpot();
        newSpot.Occupy(this);
    }

    [Button]
    public void Kill()
    {
        health = 0;
        Debug.Log($"Add death animation here");
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        AlignToSpot();
    }
    
    private void AlignToSpot()
    {
        //Optimization
        if(hasSpotToAlign == false) return;
        
        //Check Distance
        float distance = Vector3.SqrMagnitude(transform.position - currentSpot.transform.position);
        if (distance < 0.05f)
        {
            hasSpotToAlign = false;
            return;
        }
        
        //Lerp
        transform.position = Vector3.Lerp(transform.position, currentSpot.transform.position, Time.deltaTime * 5f);
    }
    
    public void SetSpot(Spot spot, bool teleport = false)
    {
        if (spot == null)
        {
            currentSpot = null;
            hasSpotToAlign = false;
            return;
        }
        
        currentSpot = spot;
        
        if (teleport)
        {
            transform.position = spot.transform.position;
            hasSpotToAlign = false;
            return;
        } 
        
        hasSpotToAlign = true;
    }
}
