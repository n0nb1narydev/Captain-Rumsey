using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    private PlatformerController player;

    [SerializeField]
    private AudioSource _deathSound;

    public bool isAttacking = false;

    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Pirate_01-Rig").GetComponent<PlatformerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Die();
        }

        if (player.numAttacks >= 5)
        {
            isDead = true;
            isAttacking = false;
            _deathSound.Play();
            StopCoroutine("KillPlayer");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isAttacking = true;
            FightPlayer();

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isAttacking = false;
        player.isFighting = false;
        StopCoroutine("KillPlayer");
        animator.SetBool("isAttacking", false);
    }

    private void Die()
    {
        animator.SetBool("isDead", true);
        StartCoroutine(Dead(1.5f));
    }
    IEnumerator Dead(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
        player.numAttacks = 0;
        player.isFighting = false;
    }

    IEnumerator KillPlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        player.Death();
    }

    private void FightPlayer()
    {
        StartCoroutine(KillPlayer(10f));
        animator.SetBool("isAttacking", isAttacking);
        player.isFighting = true;

    }
}
