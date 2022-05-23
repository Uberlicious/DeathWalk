using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	Interface _interface;
	bool _havePotion = false;
	String _potionColor;
	public bool HavePotion => _havePotion;

	void Start()
	{
		_interface = FindObjectOfType<Interface>();
	}

	public bool AddPotion(String color)
	{
		_havePotion = true;
		_potionColor = color;
		_interface.ShowBottle(color);
		return true;
	}

	public void RemovePotion()
	{
		_havePotion = false;
		_interface.HideBottle();
	}
}
