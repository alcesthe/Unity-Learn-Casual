using UnityEngine;

namespace UnityLearnCasual
{
	public class MainCamera : MonoBehaviour
	{
		[SerializeField] Player player;

        private Vector3 offsetPos;

        private void Start()
        {
            offsetPos = transform.position - player.transform.position;
        }

        private void LateUpdate()
        {
            transform.position = player.transform.position + offsetPos;
        }
    }
}
