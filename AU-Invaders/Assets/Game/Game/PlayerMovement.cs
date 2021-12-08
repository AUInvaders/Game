using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float MaxSpeed = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        //pos.y += Input.GetAxis("Vertical");
        pos.y += Input.GetAxis("Vertical") * MaxSpeed * Time.deltaTime;
        pos.x += Input.GetAxis("Horizontal") * MaxSpeed * Time.deltaTime;

        transform.position = pos;
        
    }
}
