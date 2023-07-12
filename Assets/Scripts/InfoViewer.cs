using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amiiboSeries;
    [SerializeField] TextMeshProUGUI character;
    [SerializeField] TextMeshProUGUI gameSeries;
    [SerializeField] TextMeshProUGUI name;

    public void AssignInfo(AmiiboInfo info)
    {
        amiiboSeries.text = $"amiiboSeries: {info.amiiboSeries}";
        character.text = $"character: {info.character}";
        gameSeries.text = $"gameSeries: {info.gameSeries}";
        name.text = $"name: {info.name}";
    }
}