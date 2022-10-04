using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityLearnCasual
{
	public class LostPanelUI : MonoBehaviour
	{
		[SerializeField] Text scoreText;
		[SerializeField] Button retryButton;
	    void Awake()
 	    {
			retryButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				GameManager.Instance.ResetAction();
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			});
		}

        private void OnEnable()
        {
			scoreText.text = "Your score: " + GameManager.Instance.score;
        }
    }
}
