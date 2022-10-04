using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityLearnCasual
{
	public class StartPanelUI : MonoBehaviour
	{
		[SerializeField] Button startButton;
		[SerializeField] Button quitButton;

        void Start()
 	    {
			startButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				GameManager.Instance.ChangeState(GameState.Starting);
			});

			quitButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				Application.Quit();
			});
		}
    }
}
