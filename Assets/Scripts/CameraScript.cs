using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float left_limit, right_limit; 
    // Update is called once per frame
    void Update()
    {
        if(target.position.x > left_limit && target.position.x < right_limit)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }
        
    }
}
