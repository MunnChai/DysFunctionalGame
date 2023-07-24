using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionCube : MonoBehaviour
{
    private MathFunction function;

    // Start is called before the first frame update
    void Start()
    {
        function = GameObject.Find("FunctionManager").GetComponent<MathFunction>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(function.speed * Time.deltaTime, 0, 0);

        if (transform.position.x < Constants.leftBound - transform.localScale.x / 2)
            Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            var playerScript = other.GetComponent<PlayerHealth>();
            playerScript.TakeDamage(1);
        }
    }
}
