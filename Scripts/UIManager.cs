using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _lootText;
    [SerializeField]
    private Text _livesText;
    [SerializeField]
    private Text _winningText;


    public void UpdateCoinDisplay(int loot)
    {
        _lootText.text = "Loot: " + loot;
    }

    public void UpdateLivesDisplay(int lives)
    {
        _livesText.text = "Lives: " + lives;
    }

    public void YouWin(int loot)
    {
        _winningText.text = "Huzzah! You have made it to yer ship with " + loot + " out of 100g werth o' booty.";
    }
}
