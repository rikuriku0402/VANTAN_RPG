using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title : MonoBehaviour
{
    [SerializeField]
    private SceneLoader _sceneLoader;

    public async void Oclick()
    {
        await _sceneLoader.FadeIn(SceneLoader.SceneName.Title);//�v�����[�O�V�[���̖��O�������
    }
}
