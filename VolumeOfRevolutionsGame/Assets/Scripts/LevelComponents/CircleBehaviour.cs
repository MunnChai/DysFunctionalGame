using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehaviour : MonoBehaviour
{
    float amount;
    // Start is called before the first frame update
    void Start()
    {
        amount = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.localScale.x >= 11) {
            amount = -1f;
        } else if (transform.localScale.x <= -11) {
            amount = 1f;
        } 
        transform.localScale += new Vector3(amount * Time.deltaTime, amount * Time.deltaTime, amount * Time.deltaTime);
    }
}
