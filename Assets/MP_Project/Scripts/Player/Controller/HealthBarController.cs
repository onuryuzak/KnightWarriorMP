using System.Collections;
using System.Collections.Generic;
using Domain.Entities;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;

    [SerializeField] private HealthComponent _playerHealth;

    private void OnEnable()
    {
        if (_playerHealth != null)
        {
            if (_playerHealth.OnHealthChanged != null) _playerHealth.OnHealthChanged.AddListener(UpdateHealthBar);
        }
    }

    private void OnDisable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnHealthChanged.RemoveListener(UpdateHealthBar);
        }
    }

    private void UpdateHealthBar()
    {
        if (_healthBarImage != null)
        {
            _healthBarImage.fillAmount = _playerHealth.GetHealthPercentage();
        }
    }
}