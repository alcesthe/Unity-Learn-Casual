using UnityEngine;
using System.Collections;

namespace UnityLearnCasual
{
	public class Test : MonoBehaviour
	{
	    IEnumerator Start()
 	    {
            StartCoroutine("DoSomething", 2.0f);
            yield return new WaitForSeconds(5);
            StopCoroutine("DoSomething");

            IEnumerator DoSomething(float someParameter)
            {
                while (true)
                {
                    Debug.Log("DoSomething Loop");

                    // Yield execution of this coroutine and return to the main loop until next frame
                    yield return null;
                }
            }
        }
	}
}
