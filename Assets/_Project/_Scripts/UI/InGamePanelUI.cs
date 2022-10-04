using UnityEngine;
using UnityEngine.UI;

namespace UnityLearnCasual
{
	public class InGamePanelUI : MonoBehaviour
	{
		[SerializeField] Button pauseButton;
		[SerializeField] Text scoreText;
        private void Start()
        {
			pauseButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				GameManager.Instance.ChangeState(GameState.Pausing);
			});
		}

        private void Update()
        {
			scoreText.text = "Score: " + GameManager.Instance.score;
        }
    }
}
