using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected GameObject playerObject;
    [SerializeField] protected float fallSpeed;

    protected Player player;
    protected PlayerHealth playerHealth;

    // Start is called before the first frame update
    protected void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
        if (transform.position.y <= Constants.bottomBound - transform.localScale.y / 2) {
            Destroy(gameObject);
        }
    }
}
