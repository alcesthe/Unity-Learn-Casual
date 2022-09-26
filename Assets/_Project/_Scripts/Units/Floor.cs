using UnityEngine;

namespace UnityLearnCasual
{
	public class Floor : MonoBehaviour
	{
		Transform _movePoint;
		[SerializeField] float _speed = 10f;

	    void Update()
 	    {
			transform.Translate(Vector3.back * Time.deltaTime * _speed);
	    }
	}
}
