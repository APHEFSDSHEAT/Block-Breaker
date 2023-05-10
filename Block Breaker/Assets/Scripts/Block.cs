using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breaksound;
    [SerializeField] GameObject blockDestroyVFX;
    int timesHit;
    [SerializeField] Sprite[] blockHitSprites;

    Level levelScript;

    private void Start()
    {
        levelScript = FindObjectOfType<Level>();
        if(tag == "Breakable")
        {
            levelScript.CountBreakableBlocks();
        }
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            timesHit++;
            int maxHits = blockHitSprites.Length + 1;
            if (timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextHitSprite();
            }
           
        }

    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = blockHitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        //the line below is trying to find the script
        FindObjectOfType<GameStatus>().AddToScore();
        AudioSource.PlayClipAtPoint(breaksound, Camera.main.transform.position);
        levelScript.blockDestroyed();
        TriggerBlockBreakVFX();
        // Destroy yourself
        Destroy(gameObject);
    }

    private void TriggerBlockBreakVFX()
    {

        GameObject VFX = Instantiate(blockDestroyVFX, transform.position, transform.rotation);
        Destroy(VFX, 1f); 
    }
}
