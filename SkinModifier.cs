using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinModifier : MonoBehaviour
{
    public string SkinName;
    public Image[] ImageUI;
    public Sprite[] SpriteUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ReneverseManager.SkinStats.ContainsKey(SkinName))
        {
            for(int i = 0; i < ImageUI.Length; i++) {
                ImageUI[i].sprite = SpriteUI[i];
            }
        }
    }
}
