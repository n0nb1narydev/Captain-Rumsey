using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rum : MonoBehaviour
{
    [SerializeField]
    private AudioSource _chug;
    private PlatformerController player;
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Pirate_01-Rig").GetComponent<PlatformerController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _chug.Play();
            player.lives++;
            _uiManager.UpdateLivesDisplay(player.lives);
            Destroy(this.gameObject);
        }
    }
}
