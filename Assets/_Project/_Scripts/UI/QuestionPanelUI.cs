using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace UnityLearnCasual
{
	public class QuestionPanelUI : MonoBehaviour
	{
		[SerializeField] Text typeText;
		[SerializeField] Text timeText;
		[SerializeField] Text questionText;
		[SerializeField] GameObject answersPanel;
		[SerializeField] GameObject answerInputField;
		[SerializeField] Button submitButton;

		[SerializeField] Toggle togglePrefab;

		private Question refCurrentQuestion;
		private List<Toggle> listOfToggle = new List<Toggle>();

		private ToggleGroup toggleGroup;


        private void Awake()
		{
			toggleGroup = GetComponent<ToggleGroup>();
			submitButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				if(CheckingAnswer() == false)
                {
					GameManager.Instance.ChangeState(GameState.Lost);
				}
                else
                {
					GameManager.Instance.AddScoreWhenAnswerRight();
					GameManager.Instance.ChangeState(GameState.Starting);
				}
				StopCoroutine(CountdownTimer(GameManager.Instance.timeForEachQuestion)); //Stop counting
				

				// Clear Section
				foreach (Toggle toggle in listOfToggle){Destroy(toggle.gameObject);}
				listOfToggle.Clear();
				answerInputField.GetComponent<InputField>().text = "";
			});
		}

		void OnEnable()
		{
			refCurrentQuestion = GameManager.Instance.currentQuestion;
			typeText.text = refCurrentQuestion.questionType.ToString();
			StartCoroutine(CountdownTimer(GameManager.Instance.timeForEachQuestion));
			questionText.text = refCurrentQuestion.question;

			listOfToggle.Clear(); //Clear the list
			// Show answer table based on condition
			switch (refCurrentQuestion.questionType)
			{
				case (Question.QuestionType.Input):
					answersPanel.SetActive(false);
					answerInputField.SetActive(true);
					break;
				case (Question.QuestionType.MultiSelect):
					answersPanel.SetActive(true);
					answerInputField.SetActive(false);
					for (int i = 0; i < refCurrentQuestion.listOfGivenAnswers.Count; i++)
					{
						Toggle tempToggle = Instantiate(togglePrefab, transform.position, transform.rotation, answersPanel.transform) as Toggle;
						tempToggle.isOn = false;
						tempToggle.transform.Find("Label").GetComponent<Text>().text = refCurrentQuestion.listOfGivenAnswers[i];
						listOfToggle.Add(tempToggle);
					}
					break;
				case (Question.QuestionType.Select1to4):
					answersPanel.SetActive(true);
					answerInputField.SetActive(false);
					for (int i = 0; i < refCurrentQuestion.listOfGivenAnswers.Count; i++)
					{
						var tempToggle = Instantiate(togglePrefab, transform.position, transform.rotation, answersPanel.transform);
						tempToggle.isOn = false;
						tempToggle.transform.Find("Label").GetComponent<Text>().text = refCurrentQuestion.listOfGivenAnswers[i];
						tempToggle.group = toggleGroup;
						listOfToggle.Add(tempToggle);
					}
					break;
			}
		}

        private IEnumerator CountdownTimer(float totalTime)
        {
            while (true)
            {
				totalTime -= 1;
				timeText.text = "Remain time: " + Mathf.RoundToInt(totalTime);
				if (totalTime <= 0)
                {
					GameManager.Instance.ChangeState(GameState.Lost);
					break;
                }
				yield return new WaitForSeconds(1);
			}			
        }


		private bool CheckingAnswer()
		{
			switch (refCurrentQuestion.questionType)
			{
				case (Question.QuestionType.Input):
					return String.Equals(refCurrentQuestion.correctAnswerString, answerInputField.GetComponent<InputField>().text);
				case (Question.QuestionType.MultiSelect):
					List<bool> listOfToggleValue = new List<bool>();
					foreach (Toggle toggle in listOfToggle)
					{
						listOfToggleValue.Add(toggle.isOn);
					}
					return refCurrentQuestion.listOfCorrectAnswersIndex.SequenceEqual(listOfToggleValue);
				case (Question.QuestionType.Select1to4):
					return refCurrentQuestion.correctAnswerIndex == toggleGroup.ActiveToggles().FirstOrDefault().transform.GetSiblingIndex();
				default:
					return false;
			}
		}
	}
}
