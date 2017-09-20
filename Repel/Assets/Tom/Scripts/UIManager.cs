using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _MenuVisuals, _IngameVisuals, _DeathScreenVisuals, _PauseMenuVisuals;

    [SerializeField]
    private Text _IngameScore;

    [SerializeField]
    private PlayerController _Player;


    //Call all frame updated functions.
    private void Update()
    {
        UpdateIngameScore();
    }


    //Keeps track of the ingame score of this run.
    public void UpdateIngameScore()
    {
        _IngameScore.text = Mathf.Round(_Player.Score).ToString();
    }
}
