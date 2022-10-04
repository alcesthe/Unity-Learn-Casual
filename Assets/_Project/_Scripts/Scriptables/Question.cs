using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// #if UNITY_EDITOR*
using UnityEditor;
// #endif

namespace UnityLearnCasual
{
    [CreateAssetMenu(fileName = "Question_new", menuName = "Create Question")]
    [System.Serializable]
    public class Question : ScriptableObject
    {
        public enum QuestionType { MultiSelect, Select1to4, Input }
        public enum Difficulty { Easy, Hard }
        [SerializeField] public QuestionType questionType;
        [SerializeField] public Difficulty difficulty;
        [SerializeField] public float scoreMultiplier = 1f;
        [SerializeField] public string question;

        [HideInInspector] public List<string> listOfGivenAnswers = new List<string>();
        [HideInInspector] public List<bool> listOfCorrectAnswersIndex = new List<bool>();
        [HideInInspector] public int correctAnswerIndex;
        [HideInInspector] public string correctAnswerString;
        [HideInInspector] public bool showListOfGivenAnswers = true;
        [HideInInspector] public bool showListOfCorrectAnswer = true;


        #region Editor
#if UNITY_EDITOR

        [CustomEditor(typeof(Question))]
		public class QuestionEditor : Editor
        {
            [SettingsProvider]
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                Question question = (Question)target;
                EditorUtility.SetDirty(question);

                var so = new SerializedObject(question);
                so.Update();
                

                int size = 0;
                switch (question.questionType)
                {
                    case (QuestionType.MultiSelect):
                        question.showListOfGivenAnswers = EditorGUILayout.Foldout(question.showListOfGivenAnswers, "List Of Given Answers", true);
                        if (question.showListOfGivenAnswers)
                        {
                            EditorGUI.indentLevel++;

                            size = Mathf.Max(0, EditorGUILayout.IntField("Size", question.listOfGivenAnswers.Count));
                            EditorGUILayout.Space();

                            while (size > question.listOfGivenAnswers.Count) { question.listOfGivenAnswers.Add(null); }
                            while (size < question.listOfGivenAnswers.Count) { question.listOfGivenAnswers.RemoveAt(question.listOfGivenAnswers.Count - 1); }
                            
                            for (int i = 0; i < question.listOfGivenAnswers.Count; i++)
                            {
                                question.listOfGivenAnswers[i] = EditorGUILayout.TextField("Answers " + i, question.listOfGivenAnswers[i]);
                            }

                            EditorGUI.indentLevel--;
                        }

                        question.showListOfCorrectAnswer = EditorGUILayout.Foldout(question.showListOfCorrectAnswer, "List Of Correct Answers ", true);
                        if (question.showListOfCorrectAnswer)
                        {
                            EditorGUI.indentLevel++;

                            while (question.listOfCorrectAnswersIndex.Count < question.listOfGivenAnswers.Count) { question.listOfCorrectAnswersIndex.Add(false); }
                            while (question.listOfCorrectAnswersIndex.Count > question.listOfGivenAnswers.Count) { question.listOfCorrectAnswersIndex.RemoveAt(question.listOfGivenAnswers.Count - 1); }

                            for (int i=0; i < question.listOfGivenAnswers.Count; i++)
                            {
                                question.listOfCorrectAnswersIndex[i] = EditorGUILayout.Toggle(question.listOfGivenAnswers[i], question.listOfCorrectAnswersIndex[i]);
                                //Debug.Log("Index " + i + ": " + question.listOfCorrectAnswersIndex[i]);
                            }
                            
                            EditorGUI.indentLevel--;
                        }
                        break;
                    case (QuestionType.Select1to4):
                        question.showListOfGivenAnswers = EditorGUILayout.Foldout(question.showListOfGivenAnswers, "List Of Given Answers", true);
                        if (question.showListOfGivenAnswers)
                        {
                            EditorGUI.indentLevel++;
                            int fixedSize = 4;
                            while (fixedSize > question.listOfGivenAnswers.Count) { question.listOfGivenAnswers.Add(null); }
                            while (fixedSize < question.listOfGivenAnswers.Count) { question.listOfGivenAnswers.RemoveAt(question.listOfGivenAnswers.Count - 1); }

                            for (int i = 0; i < question.listOfGivenAnswers.Count; i++)
                            {
                                question.listOfGivenAnswers[i] = EditorGUILayout.TextField("Answers " + i, question.listOfGivenAnswers[i]);
                            }

                            EditorGUI.indentLevel--;
                        }
                        if (question.showListOfCorrectAnswer)
                        {
                            EditorGUI.indentLevel++;

                            string[] refListOfGivenAnswers = question.listOfGivenAnswers.ToArray();
                            question.correctAnswerIndex = GUILayout.SelectionGrid(question.correctAnswerIndex, refListOfGivenAnswers, 1, EditorStyles.radioButton);

                            EditorGUI.indentLevel--;
                        }
                        
                        break;
                    case (QuestionType.Input):
                        question.correctAnswerString = EditorGUILayout.TextField("Answer: ", question.correctAnswerString);
                        break;
                    default:
                        EditorGUILayout.LabelField("Select Type");
                        break;
                }

                so.ApplyModifiedProperties();
            }
        }
		#endif
#endregion

	}
}
