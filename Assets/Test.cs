using UnityEngine;

namespace UnityLearnCasual
{
	public class Test : MonoBehaviour
	{
		[SerializeField] Question[] questions;
	    void Awake()
 	    {
 	       for (int i = 0; i < questions.Length; i++)
           {
				Debug.Log(questions[i].name + ": " + questions[i].listOfGivenAnswers.Count);
           }
	    }
	}
}
