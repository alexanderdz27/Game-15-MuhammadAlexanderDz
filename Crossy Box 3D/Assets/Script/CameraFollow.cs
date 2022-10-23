using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject karakter;
    [SerializeField] Vector3 offset;

    private void Start() 
    {
         offset = this.transform.position - karakter.transform.position ;
    }

    Vector3 lastKarakterPos;
    void Update()
    {
        if (lastKarakterPos == karakter.transform.position)
        {
            return;
        }

        var targetKarakterPos =  new Vector3(karakter.transform.position.x, 0, karakter.transform.position.z );

        transform.position = targetKarakterPos + offset;
        lastKarakterPos = karakter.transform.position;
    }
}
