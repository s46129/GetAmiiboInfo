using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public partial class GetAmiiboInfo : MonoBehaviour
{
    private string HostURL = "https://www.amiiboapi.com/api/amiibo/";

    [SerializeField] TMP_InputField inputField;

    [SerializeField] private GameObject Prefab;
    [SerializeField] Transform ListContent;

    private readonly List<InfoViewer> _infoViewers = new List<InfoViewer>();
    List<InfoViewer> _infoViewersObjectPool = new List<InfoViewer>();

    public void Search()
    {
        StartCoroutine(APIManager.Get(HostURL + $"/?name={inputField.text}", (response) =>
        {
            if (response.isSuccess)
            {
                AmiiboInfoList amiiboInfos = JsonConvert.DeserializeObject<AmiiboInfoList>(response.result);
                Debug.Log(amiiboInfos.amiibo[0].name);
                UpdateList(amiiboInfos);
            }
            else
            {
                Debug.Log(response.result);
            }
        }));
    }

    private void UpdateList(AmiiboInfoList amiiboInfos)
    {
        for (var index = 0; index < _infoViewers.Count; index++)
        {
            var viewer = _infoViewers[index];
            viewer.Dispose();
            viewer.gameObject.SetActive(false);
            _infoViewersObjectPool.Add(viewer);
            _infoViewers.Remove(viewer);
        }

        amiiboInfos.amiibo.ForEach((info) =>
        {
            InfoViewer infoViewer;
            if (_infoViewersObjectPool.Count > 0)
            {
                infoViewer = _infoViewersObjectPool[0];
                infoViewer.gameObject.SetActive(true);
                _infoViewersObjectPool.Remove(infoViewer);
            }
            else
            {
                infoViewer = CreatInfoViewer(info);
            }

            _infoViewers.Add(infoViewer);
        });
    }

    private InfoViewer CreatInfoViewer(AmiiboInfo info)
    {
        GameObject go = Instantiate(Prefab, ListContent);
        var infoViewer = go.GetComponent<InfoViewer>();
        infoViewer.AssignInfo(info);
        return infoViewer;
    }
}