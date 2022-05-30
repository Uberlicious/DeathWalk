using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {
	
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class TypeText : MonoBehaviour {
		[SerializeField] float typingSpeed = .1f;
		[SerializeField] float pauseTime = 2f;
		[SerializeField] TextMeshProUGUI _textBox;

		bool _isTyping;

		void Awake()
		{
			int typeText = FindObjectsOfType<TypeText>().Length;
			if (typeText > 1)
			{
				Destroy(gameObject);
			}
			else
			{
				DontDestroyOnLoad(gameObject);
			}
		}

		public void StartText(string text)
		{
			StartCoroutine(StartTyping(text));
		}

		IEnumerator StartTyping(string text)
		{
			_textBox.text = "";
			foreach (char c in text)
			{
				_textBox.text = $"{_textBox.text}{c}";
				yield return new WaitForSeconds(typingSpeed);
			}
			yield return new WaitForSeconds(pauseTime);
			_textBox.text = "";
		}

		public void StartText(List<string> textList, Action callback)
		{
			StartCoroutine(StartTyping(textList, callback));
		}
		
		IEnumerator StartTyping(List<string> textList, Action callback)
		{
			if (!_isTyping)
			{
				foreach (string text in textList)
				{
					_textBox.text = "";
					foreach (char c in text)
					{
						_textBox.text = $"{_textBox.text}{c}";
						yield return new WaitForSeconds(typingSpeed);
					}
					yield return new WaitForSeconds(pauseTime);
					_textBox.text = "";
				}
				callback();
			}
		}
		
		public List<string> GetStartLines()
		{
			List<string> _startLines = new List<string>();
			_startLines.Add("You thought you had it all");
			_startLines.Add("Can you get it back...");
			return _startLines;
		}

		public List<string> GetEndLines()
		{
			List<string> _endLines = new List<string>();
			_endLines.Add("You're so close");
			_endLines.Add("You will never finish");
			_endLines.Add("You will forever be stuck in this loop");
			_endLines.Add("Death was only the beginning...");
			return _endLines;
		}
	}
}
