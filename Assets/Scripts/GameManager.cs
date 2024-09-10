using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [Header("Controllers")]
    [SerializeField]private ArthurMovement _arthur;
    [SerializeField]private SceneController _sceneController;
    [SerializeField]private SpawnController _spawnController;
    [SerializeField]private TimerController _timerController;
    
    public enum CurrentLevel
    {
        Level1=0,
        Level2=1,
        Level3=2,
        Win=3,
        Loose=4
    }

    [Header("Madness Icons")]
    [SerializeField] private GameObject madnesSkull1;
    [SerializeField] private GameObject madnesSkull2;
    [SerializeField] private GameObject madnesSkull3;

    
    [Header("Feedbacks interfaces")]
    [SerializeField] private GameObject nextLevelFeedback;
    [SerializeField] private GameObject looseFeedback;
    [SerializeField] private GameObject winFeedback;

    [SerializeField] private GameObject settings;
    
    
    [Header("Level Variables")]
    public CurrentLevel currentLevel;
    [SerializeField] private float[] secondsLeft; //the timer before the level ends
    [SerializeField] private float[] cdSpawn;

    private void Awake()
    {
        SetUpLevel();
        Time.timeScale = 1;
    }

    private void SetUpLevel()
    {
        switch (currentLevel)
        {
            case CurrentLevel.Level1:
                _spawnController.cdSpawn = cdSpawn[0];
                _timerController.timeLeft = secondsLeft[0];
                _spawnController.isReady = true;
                break;

            case CurrentLevel.Level2:
                _spawnController.cdSpawn = cdSpawn[1];
                _timerController.timeLeft = secondsLeft[1];
                StartCoroutine(ToggleActiveFeedback(nextLevelFeedback));
                _spawnController.isReady = true;
                break;

            case CurrentLevel.Level3:
                _spawnController.cdSpawn = cdSpawn[2];
                _timerController.timeLeft = secondsLeft[2];
                StartCoroutine(ToggleActiveFeedback(nextLevelFeedback));
                _spawnController.isReady = true;
                break;

            case CurrentLevel.Win:
                KillPigeons();
                winFeedback.SetActive(true);
                settings.SetActive(true);
                _spawnController.stopAll = true;
                break;

            case CurrentLevel.Loose:
                KillPigeons();
                looseFeedback.SetActive(true);
                settings.SetActive(true);
                _spawnController.stopAll = true;
                break;
        }
    }

    void FixedUpdate()
    {
        Nextlevel();
        UpdateSkulls();
        if (_arthur==null)
        {
            Debug.Log("Je murio e gato");
        }
    }
    
    void UpdateSkulls()
    {
        if (_arthur.madness == 2)
        {
            madnesSkull1.SetActive(true);
        }
        else if (_arthur.madness == 1)
        {
            madnesSkull1.SetActive(true);
            madnesSkull2.SetActive(true);
        }
        else if (_arthur.madness == 0)
        {
            madnesSkull1.SetActive(true);
            madnesSkull2.SetActive(true);
            madnesSkull3.SetActive(true);
            Animator animatorCam = _camera.GetComponent<Animator>();
            animatorCam.SetBool("isCrazy", true);
        }
    }

    void Nextlevel()
    {
        if (currentLevel == CurrentLevel.Level1 && _arthur.madness > 0 && _timerController.timeLeft <= 0)
        {
            currentLevel = CurrentLevel.Level2;
            SetUpLevel();
            _spawnController.isReady = false;
            
        }else if (currentLevel == CurrentLevel.Level2 && _arthur.madness > 0 && _timerController.timeLeft <= 0)
        {
            currentLevel = CurrentLevel.Level3;
            SetUpLevel();
            _spawnController.isReady = false;
        }else if (currentLevel == CurrentLevel.Level3 && _arthur.madness > 0 && _timerController.timeLeft <= 0)
        {
            currentLevel = CurrentLevel.Win;
            SetUpLevel();
            _spawnController.isReady = false;
            
        }else if ( _arthur.madness <= 0 || !_arthur)
        {
            currentLevel = CurrentLevel.Loose;
            SetUpLevel();
            _spawnController.isReady = false;
        }
    }

    void KillPigeons()
    {
        var pigeons = FindObjectsOfType<PigeonMovement>();
        foreach (var p in pigeons)
        {
            Destroy(p.gameObject);
            Debug.Log("Palochau");
        }
        
    }

    IEnumerator ToggleActiveFeedback(GameObject obj)
    {
        KillPigeons();
        _spawnController.stopAll=true;
        obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        obj.SetActive(false);
        _spawnController.stopAll=false;
    }
    
    public void PauseButton()
    {
        settings.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        settings.SetActive(false);
        Time.timeScale = 1;
        
    }
}
