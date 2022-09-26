using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityLearnCasual
{
	enum Item { item_1, item_2}
	[CreateAssetMenu(fileName = "Test")]
	public class TestScriptableObject : ScriptableObject
	{
		[SerializeField] Item item;

		string question;
		List<string> givenAnswers = new List<string>();
		string correctAnswer;

		#region Editor
#if UNITY_EDITOR

		[CustomEditor(typeof(TestScriptableObject))]
		public class TestEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                TestScriptableObject testScriptableObject = (TestScriptableObject)target;

                DrawLayout(testScriptableObject);

                List<string> m_giveAnswers = testScriptableObject.givenAnswers;
                int size = Mathf.Max(0, EditorGUILayout.IntField("Size", m_giveAnswers.Count));

                while (size > m_giveAnswers.Count)
                {
                    m_giveAnswers.Add(null);
                }

                while (size < m_giveAnswers.Count)
                {
                    m_giveAnswers.RemoveAt(m_giveAnswers.Count - 1);
                }

                for (int i = 0; i < m_giveAnswers.Count; i++)
                {
                    m_giveAnswers[i] = EditorGUILayout.TextField("Element " + i, m_giveAnswers[i]);
                }
            }

            private static void DrawLayout(TestScriptableObject testScriptableObject)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Test item 1");
                EditorGUILayout.BeginVertical();

                EditorGUILayout.LabelField("Question", GUILayout.MaxHeight(50));
                testScriptableObject.question = EditorGUILayout.TextField(testScriptableObject.question);

                //
                //

                EditorGUILayout.LabelField("Correct Answer", GUILayout.MaxHeight(50));
                testScriptableObject.correctAnswer = EditorGUILayout.TextField(testScriptableObject.correctAnswer);

                EditorGUILayout.EndVertical();
            }
        }
#endif
		#endregion
	}

}
