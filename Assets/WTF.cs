using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTF : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"frame {Time.frameCount} update, pos = {transform.position}");    
    }

    private void LateUpdate()
    {
        Debug.Log($"frame {Time.frameCount} lateUpdate, pos = {transform.position}");
    }
}
