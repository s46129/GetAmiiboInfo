using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class GetAmiiboInfo : MonoBehaviour
{
    private string HostURL = "https://www.amiiboapi.com/api/amiibo/";

    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private GameObject Prefab;
    [SerializeField] Transform ListContent;
    [SerializeField] private TMP_Dropdown _dropdown;

    private readonly List<InfoViewer> _infoViewers = new List<InfoViewer>();
    [SerializeField] List<InfoViewer> _infoViewersObjectPool = new List<InfoViewer>();
    private IEnumerator _getCoroutine;


    public void Search()
    {
        CleanRecorder();
        if (_getCoroutine != null)
        {
            StopCoroutine(_getCoroutine);
        }

        var searchType = _dropdown.options[_dropdown.value].text;
        Debug.Log("searchType " + searchType);
        _getCoroutine = APIManager.Get($"{HostURL}/?{searchType}={inputField.text}",
            (response) =>
            {
                if (response.isSuccess)
                {
                    var amiiboInfos = JsonConvert.DeserializeObject<AmiiboInfoList>(response.result);
                    UpdateList(amiiboInfos);
                }
                else
                {
                    Debug.Log(response.result);
                }
            });
        StartCoroutine(_getCoroutine);
    }

    private void CleanRecorder()
    {
        for (var index = 0; index < _infoViewers.Count; index++)
        {
            var viewer = _infoViewers[index];
            viewer.Dispose();
            viewer.gameObject.SetActive(false);
            _infoViewersObjectPool.Add(viewer);
        }

        _infoViewers.Clear();
    }

    private void UpdateList(AmiiboInfoList amiiboInfos)
    {
        amiiboInfos.amiibo.ForEach(info => { GetInfoViewer().AssignInfo(info); });
    }

    private InfoViewer GetInfoViewer()
    {
        var infoViewer = _infoViewersObjectPool.Count > 0 ? GetInfoViewerFromPool() : CreatInfoViewer();
        _infoViewers.Add(infoViewer);
        infoViewer.gameObject.SetActive(true);
        infoViewer.gameObject.name = infoViewer.name;
        return infoViewer;
    }

    private InfoViewer GetInfoViewerFromPool()
    {
        var infoViewer = _infoViewersObjectPool[0];
        _infoViewersObjectPool.Remove(infoViewer);
        return infoViewer;
    }

    private InfoViewer CreatInfoViewer()
    {
        var creatInfoViewer = Instantiate(Prefab, ListContent).GetComponent<InfoViewer>();
        return creatInfoViewer;
    }
}