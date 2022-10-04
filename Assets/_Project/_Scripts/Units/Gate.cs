using UnityEngine;

namespace UnityLearnCasual
{
	public class Gate : MonoBehaviour
	{
        public Question.Difficulty difficulty;
        private void OnTriggerEnter(Collider other)
        {
            GameManager.Instance.currentDifficulty = difficulty;
            GameManager.Instance.ChangeState(GameState.Answering);
        }
    }
}
