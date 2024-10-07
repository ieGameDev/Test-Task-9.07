using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Logic
{
    public class FinishTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
                ReloadCurrentLevel();
        }

        private void ReloadCurrentLevel() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
