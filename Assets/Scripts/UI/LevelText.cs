using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class LevelText : MonoBehaviour {
		[SerializeField] TypeText _typeText;
		[SerializeField] Image _image;
		[SerializeField] float fadeTime = 10f;
		[SerializeField] float initialSpeedDecrease = 3f;

		Color _fromColor = new Color(0, 0, 0, 0);
		Color _toColor = new Color(0, 0, 0, 1);
		
		ThirdPersonMovement _thirdPersonMovementScript;
		float _thirdPersonMovementStartSpeed;
		SceneManage _sceneManage;
		
		int _clears;
		
		void Awake()
		{
			int levelText = FindObjectsOfType<LevelText>().Length;
			if (levelText > 1)
			{
				Destroy(gameObject);
			}
			else
			{
				DontDestroyOnLoad(gameObject);
			}
		}

		void Start()
		{
			RetrieveElements();
			StartGame();
		}
		
		void GetSceneManage()
		{
		    _sceneManage = FindObjectOfType<SceneManage>();
		}

		void GetTpm()
		{
			_thirdPersonMovementScript = FindObjectOfType<ThirdPersonMovement>();
			_thirdPersonMovementStartSpeed = _thirdPersonMovementScript.speed;
		}
		
		void StartGame()
		{
			if (_thirdPersonMovementScript == null) GetTpm();
			_thirdPersonMovementScript.enabled = false;
			_typeText.StartText(_typeText.GetStartLines(), EnableTpm);
			LeanTween.value(_image.gameObject, SetColor, _toColor, _fromColor, fadeTime);
		}

		void EnableTpm()
		{
			if (_thirdPersonMovementScript == null) GetTpm();
			_thirdPersonMovementScript.enabled = true;
		}

		void RetrieveElements()
		{
			if (_thirdPersonMovementScript == null) GetTpm();
			_thirdPersonMovementScript = FindObjectOfType<ThirdPersonMovement>();
		}

		public void TriggerEndGame()
		{
			Debug.Log("FadeColor");
			_typeText.StartText(_typeText.GetEndLines(), TriggerReload);
			LeanTween.value(_image.gameObject, SetMoveSpeed, _thirdPersonMovementScript.speed - initialSpeedDecrease, 0f, fadeTime);
			LeanTween.value(_image.gameObject, SetColor, _fromColor, _toColor, fadeTime);
		}

		void SetColor(Color c)
		{
			_image.color = c;
		}

		void SetMoveSpeed(float s)
		{
			_thirdPersonMovementScript.speed = s;
		}

		void TriggerReload()
		{
			if (!_sceneManage)
			{
				GetSceneManage();
			}
			_image.color = _fromColor;
			_thirdPersonMovementScript.speed = _thirdPersonMovementStartSpeed;
			_sceneManage.ReloadScene();
		}
	}
}
