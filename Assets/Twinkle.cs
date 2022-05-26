using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Twinkle : MonoBehaviour
{
    public float totalSeconds;
    public float maxIntensity;
    private float waitTime;
    UnityEngine.Rendering.Universal.Light2D myLight;
    
    // Start is called before the first frame update
    void Start()
    {
        myLight = gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        waitTime = totalSeconds / 2;
        StartCoroutine(TwinkleLight()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TwinkleLight()
    {

        while (true)
        {

            while (myLight.intensity < maxIntensity)
            {

                myLight.intensity += Time.deltaTime / waitTime;
                yield return null;
            }
            
            while (myLight.intensity > 9)
            {

                myLight.intensity -= Time.deltaTime / waitTime;
                yield return null;
            }
            yield return null;
        }
        

    }
}
