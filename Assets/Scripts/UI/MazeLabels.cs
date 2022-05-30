using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
	
	[ExecuteAlways]
	[RequireComponent(typeof(TextMeshPro))]
	public class MazeLabels : MonoBehaviour {

		TextMeshPro _label;
		Vector3Int _coordinates;

		void Awake()
		{
			_label = GetComponent<TextMeshPro>();
		}

		void Update()
		{
			if (!Application.isPlaying)
			{
				UpdateObjectName();
			}
			DisplayCoordinates();
		}

		void UpdateObjectName()
		{
			transform.parent.name = _coordinates.ToString();
		}

		void DisplayCoordinates()
		{
			Vector3 tilePos = transform.parent.localPosition;
			_coordinates.x = Mathf.RoundToInt(tilePos.x);
			_coordinates.z = Mathf.RoundToInt(tilePos.z);
			_label.text = $"{_coordinates.x},{_coordinates.z}";
		}
	}
}
