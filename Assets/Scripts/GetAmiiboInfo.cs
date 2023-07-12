using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public partial class GetAmiiboInfo : MonoBehaviour
{
    private string HostURL = "https://www.amiiboapi.com/api/amiibo/";

    [SerializeField] TMP_InputField inputField;

    [SerializeField] private GameObject Prefab;
    [SerializeField] Transform ListContent;

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
        amiiboInfos.amiibo.ForEach((info) =>
        {
            GameObject go = Instantiate(Prefab, ListContent);
            go.GetComponent<InfoViewer>().AssignInfo(info);
        });
    }
}