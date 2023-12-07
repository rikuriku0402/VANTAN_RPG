using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaveDataManager : MonoBehaviour
{
    public List<string> SaveKeyName => _saveKeyName;

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
    private ScenarioData _scenarioData;

    [SerializeField]
    private ScenarioManager _scenarioManager;
    
    private const int MAX_SAVEDATA = 10;
    private const int NAME_LINE = 0;
    private const int TALK_LINE = 1;

    private void Start()
    {
        for (int i = 0; i < MAX_SAVEDATA; i++)
        {
            _saveKeyName.Add($"SAVE_DATA_{i}");
        }
        
        Debug.Log("セーブデータの読み込み開始");
        LoadNovelData(_saveCount);

        _saveButton.onClick.AsObservable()
            .Subscribe(_ => OverWriteSaveNovelData(_saveCount))
            .AddTo(this);

        _loadButton.onClick.AsObservable()
            .Subscribe(_ => LoadNovelData(_saveCount))
            .AddTo(this);

    }

    private void LoadNovelData(int saveNum)
    {
        var saveData = JsonSaveManager<SaveData>.Load(_fileName + "/" + _saveKeyName[saveNum]);
        
        //セーブデータが存在確認
        if (saveData == null)
        {
            Debug.Log("新規作成");
            saveData = new SaveData()
            {
                _lineNum = new(1),
                _name = "",
                _talk = "",
            };
        }
        else
        {
            Debug.Log("セーブデータが存在したため読み込みました");
        }

        _scenarioData.SetGameDataValue(saveData);
    }

    /// <summary>アプリケーション終了時に呼び出す</summary>
    // private void OnApplicationQuit() => OverWriteSaveNovelData(_saveCount);

    private void OverWriteSaveNovelData(int saveNum)
    {
        var saveData = new SaveData()
        {
            _lineNum = _scenarioData.LineNum,
            _name = _scenarioManager.Datas[_scenarioData.LineNum.Value][NAME_LINE],
            _talk = _scenarioManager.Datas[_scenarioData.LineNum.Value][TALK_LINE],
        };
        
        JsonSaveManager<SaveData>.Save(saveData,_fileName + "/" + _saveKeyName[saveNum]);
        Debug.Log("セーブしました");
    }
}
