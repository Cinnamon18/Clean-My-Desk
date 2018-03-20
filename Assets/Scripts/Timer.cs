﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour {
	public float time = 30;
	public GameObject endMenu;
	public GameObject itemsContainer;
	private TextMeshProUGUI text;
	private bool continueCount = true;
	private bool start = false;
	// Use this for initialization
	void Start () {
		endMenu.transform.Find("MainModeUI").gameObject.SetActive(false);
		endMenu.transform.localScale = new Vector3(0.0f,1,1);
		text = transform.GetComponent<TextMeshProUGUI>();
		SetTime();
		//text.text = time.ToString();
		StartCoroutine(InitCountdown());
	}

	IEnumerator InitCountdown() {
		text.text = "3";
		yield return new WaitForSeconds(1);
		text.text = "2";
		yield return new WaitForSeconds(1);
		text.text = "1";
		yield return new WaitForSeconds(1);
		text.text = "GO!";
		GetComponent<Animator>().SetBool("Start", true);
		yield return new WaitForSeconds(0.5f);
		text.text = time.ToString();
		start = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (continueCount && start) {
			if (itemsContainer.transform.childCount == 0) {
				continueCount = false;
				endMenu.transform.Find("TimeModeUI/Fail").gameObject.SetActive(false);
				SaveData.TimeModeLevel++;
				//trigger menu
				endMenu.GetComponent<Menuing>().ToggleMenu();
			} else if (time < 0) {
				time = 0;
				continueCount = false;
				endMenu.transform.Find("TimeModeUI/Success").gameObject.SetActive(false);
				//trigger menu
				endMenu.GetComponent<Menuing>().ToggleMenu();
			}
			if (time >= 10) {
				text.text = (time.ToString()).Substring(0, 2);
			} else {
				text.text = (time.ToString()).Substring(0, 1);
			}
			time -= Time.deltaTime;
		}
	}

	private void SetTime() {
		time -= (SaveData.TimeModeLevel * SaveData.TimeModeLevel);
	}
}