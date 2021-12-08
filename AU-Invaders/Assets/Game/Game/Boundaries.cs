using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        //gør så halvdelen af spriten ikke går oop 
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }
    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0f, 685f - objectWidth),//926 er fullscreen, 685 er sidepanel trukket fra 
            Mathf.Clamp(transform.position.y, 0f, 520f - objectHeight), transform.position.z);//needs scaling so the playermodel wont clip oop

    }
}
