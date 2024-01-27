using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour
{
    public IReadOnlyList<string> SaveKeyName => _saveKeyName;

    [SerializeField] 
    [Header("保存場所")] 
    private string _fileName;

    [SerializeField]
    [Header("ゲームデータのキー")]
    private List<string> _saveKeyName;

    [SerializeField]
    [Header("読みこみたいセーブデータの番号")]
    [Range(0, 9)]
    private int _saveCount;

    [SerializeField] 
    [Header("セーブボタン")]
    private Button _saveButton;
    
    [SerializeField] 
    [Header("ロードボタン")]
    private Button _loadButton;

    [SerializeField]
    private GSSReader _gssReader;

    [SerializeField]
    private CharacterStatusData _charaStatusData;

    private const int START_LINE = 2;

    private async void Start()
    {
        var token = this.GetCancellationTokenOnDestroy();
        await UniTask.WaitUntil(() => !_gssReader.IsLoading, cancellationToken: token);

        var data = _gssReader.Datas;

        for (int i = 0; i < data.Length; i++)
        {
            if (data[i][1] == null) return;
            _saveKeyName.Add($"CHARACTER_DATA_{i}");
        }
        Debug.Log("セーブデータの読み込み開始");

        LoadData();

        _saveButton.onClick.AsObservable()
            .Subscribe(_ => OverWriteSaveData())
            .AddTo(this);

        _loadButton.onClick.AsObservable()
            .Subscribe(_ => LoadData())
            .AddTo(this);

    }

    /// <summary>ロード</summary>
    public void LoadData()
    {
        var data = _gssReader.Datas;

        for (int i = 0; i < data.Length; i++)
        {
            var saveData = JsonSaveManager<SaveData>.Load(_fileName + "/" + _saveKeyName[i]);

            //セーブデータの存在確認
            if (saveData == null)
            {
                Debug.Log("新規作成");
                saveData = new SaveData()
                {
                    /*初期ステータスを代入*/
                    name = data[i + START_LINE][0],
                    hp = int.Parse(data[i + START_LINE][1]),
                    atk = int.Parse(data[i + START_LINE][2]),
                    def = int.Parse(data[i + START_LINE][3]),
                    magicAtk = int.Parse(data[i + START_LINE][4]),
                    magicDef = int.Parse(data[i + START_LINE][5]),
                    speed = int.Parse(data[i + START_LINE][6]),
                    type = data[i + START_LINE][7],
                };
            }
            else Debug.Log("セーブデータが存在したため読み込みました");

            _charaStatusData.SetGameDataValue(saveData, i);
        }
    }

    /// <summary>アプリケーション終了時に呼び出す</summary>
    // private void OnApplicationQuit() => OverWriteSaveNovelData(_saveCount);
    
    /// <summary>上書き保存</summary>
    private void OverWriteSaveData()
    {
        var data = _gssReader.Datas;

        for (int i = 0; i < data.Length; i++)
        {
            var saveData = new SaveData()
            {
                /*ここにバトル後のステータスを代入*/
                name = _charaStatusData.SaveData[i].name,
                hp = _charaStatusData.SaveData[i].hp,
                atk = _charaStatusData.SaveData[i].atk,
                def = _charaStatusData.SaveData[i].def,
                magicAtk = _charaStatusData.SaveData[i].magicAtk,
                magicDef = _charaStatusData.SaveData[i].magicDef,
                speed = _charaStatusData.SaveData[i].speed,
                type = _charaStatusData.SaveData[i].type,
            };

            JsonSaveManager<SaveData>.Save(saveData, _fileName + "/" + _saveKeyName[i]);
        }

        Debug.Log("セーブしました");
    }
}
