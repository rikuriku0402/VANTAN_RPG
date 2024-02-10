using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(
  fileName = "SceneData",
  menuName = "ScriptableObject/SceneData")]
public class SceneBase : ScriptableObject
{
    [SerializeField] List<SceneBaseObj> _scenebase = new();
    public List<SceneBaseObj> ST_scenebase => _scenebase;
}
[System.Serializable]
public class SceneBaseObj
{
    public int Xpoint => _Xpoint;
    public int Ypoint => _Ypoint;

    public SceneLoader.SceneName Test => _test;

    [SerializeField]
    private SceneLoader.SceneName _test;

    [SerializeField]
    int _Xpoint;

    [SerializeField]
    int _Ypoint;



}