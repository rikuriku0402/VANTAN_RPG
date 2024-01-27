using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class ButtonManager : MonoBehaviour
{
    public List<Button> SelectButtons => _selectButtons;

    [SerializeField]
    private List<Button> _selectButtons;

    void Start()
    {

    }


}
