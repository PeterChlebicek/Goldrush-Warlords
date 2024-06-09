using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceTextCanvas : MonoBehaviour
{
    private Camera _cam;
    public TextMeshProUGUI _textMeshPro;

    void Start()
    {
        _cam = Camera.main;
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    public void UpdateInventoryText(int inventoryCapacity)
    {
        if (_textMeshPro != null)
        {
            _textMeshPro.text = "" + inventoryCapacity;
        }
    }
}
