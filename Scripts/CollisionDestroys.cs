using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroys : MonoBehaviour
{
    [SerializeField]
    private AudioSource _coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(PickupCoins(.2f));
            _coins.Play();
        }
    }

    IEnumerator PickupCoins(float waitTime)
    {
        // suspend execution for 2 seconds
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}

