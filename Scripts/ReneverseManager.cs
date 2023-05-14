using ReneVerse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReneverseManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        var ReneAPI = ReneAPIManager.API();
        Debug.Log(ReneAPI.AuthToken);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
