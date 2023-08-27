using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerControlsSO controls;
    [SerializeField] private GameObject pauseMenu;

    private Movement _PlayerMovement;
    private Jump _PlayerJump;
    private AbilityCache _PlayerCache;

    private bool _IsPaused = false;

    private void Awake()
    {
        _PlayerMovement = GetComponent<Movement>();
        _PlayerJump = GetComponent<Jump>();
        _PlayerCache = GetComponent<AbilityCache>();
    }

    private void Update()
    {
        if (controls.RetrievePause())
            TogglePause();
    }

    public void TogglePause()
    {
        _IsPaused = !_IsPaused;
        PerformPause(_IsPaused);
    }

    public void PerformPause(bool _IsPaused)
    {
        if(_IsPaused)
        {
            pauseMenu.SetActive(true);

            _PlayerCache.enabled = false;
            _PlayerJump.enabled = false;
            _PlayerMovement.enabled = false;
        }
        else
        {
            pauseMenu.SetActive(false);

            _PlayerCache.enabled = true;
            _PlayerJump.enabled = true;
            _PlayerMovement.enabled = true;
        }
    }
}
