using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Animator animator;
    [SerializeField]
    private AudioSource _open;
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
            print("entered");
            animator.enabled = true;
            _open.Play();
            _coins.Play();
            StartCoroutine(OpenChest(.5f));
        }

    }

    IEnumerator OpenChest(float waitTime)
    {
        // suspend execution for 2 seconds
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
