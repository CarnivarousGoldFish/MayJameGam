using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    private Color[] spriteColor;
    private float[] r,g,b;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderers = FindObjectsOfType<SpriteRenderer>();
        i=0;
        r=new float[spriteRenderers.Length];
        g=new float[spriteRenderers.Length];
        b=new float[spriteRenderers.Length];
        foreach(SpriteRenderer spriterenderer in spriteRenderers){
            // spriteColor.Add(spriterenderer.color);
            Debug.Log(spriterenderer.color.r);
            r[i]=spriterenderer.color.r;
            g[i]=spriterenderer.color.g;
            b[i]=spriterenderer.color.b;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setBrightness(float BrightnessValue){
        Debug.Log(BrightnessValue);
        i=0;
        foreach(SpriteRenderer spriterenderer in spriteRenderers){
            spriterenderer.color = new Color(r[i]*BrightnessValue,g[i]*BrightnessValue,b[i]*BrightnessValue,spriterenderer.color.a);
            i++;
        }

    }

}
