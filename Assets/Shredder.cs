using UnityEngine;

namespace UnityLearnCasual
{
	public class Shredder : MonoBehaviour
	{
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}
