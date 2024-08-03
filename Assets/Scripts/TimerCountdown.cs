using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerCountdown : MonoBehaviour
{
    public float _currentTime = 0f;
    public float _startTime = 50f;
    public TMP_Text _timer;
    public PuzzleManager puzzlescript;
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _startTime;
        _timer.text = _currentTime.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if(puzzlescript.win == true && _currentTime > 0f)
        {
            SceneManager.LoadScene("GameOverWin");
        }
        else if(_currentTime <= 0)
        {
            SceneManager.LoadScene("GameOverLose");
        }
        _currentTime -= 1 * Time.deltaTime;
        
        if (_currentTime <= 0)
        {
            _currentTime = 0;
        }
        _timer.text = _currentTime.ToString("F2");
    }
}
