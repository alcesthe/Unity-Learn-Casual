using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityLearnCasual
{
	public class PausePanelUI : MonoBehaviour
	{
		[SerializeField] Button resumeButton;
		[SerializeField] Button quitButton;
		void Awake()
 	    {
			resumeButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				GameManager.Instance.ChangeState(GameState.Starting);
			});

			quitButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				GameManager.Instance.ResetAction();
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);				
				//GameManager.Instance.ChangeState(GameState.Idle);
			});
		}
	}
}
