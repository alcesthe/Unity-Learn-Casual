using UnityEngine;

namespace UnityLearnCasual
{
	public class Spike : MonoBehaviour
	{
        private void OnTriggerEnter(Collider other)
        {
            GameManager.Instance.ChangeState(GameState.Lost);
        }
    }
}
