using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _startWindow;

    private void Awake()
    {
        _startWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startWindow.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
