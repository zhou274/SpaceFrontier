﻿using UnityEngine;
using System.Collections;
using TMPro;

namespace SpaceFrontier
{
	public class CounterController : MonoBehaviour
	{
		/// <summary>
		/// The counter object.
		/// It is used in the first run of the game. counts from 3 to 1 and then disappears.
		/// </summary>

		public static bool initCounter;                     //static flag

		public GameObject GC;                               //Game controller
		public GameObject tapToStartGo;                     //text object
		public GameObject uiButtonUpgrade;                  //upgrade button
		public GameObject uiButtonNewSkin;                  //new skin button
		public GameObject uiButtonHireAstronaut;            //hire new astronaut button
		public GameObject uiButtonSwitchStartingPlanet;     //swtich between available starting planets
		public GameObject uiHiredAstronautCell;             //information cell in UI which shows the current hired astronaut inside the spaceship
		public GameObject uiOnPlanetName;                   //Text object used to show current starting planet name
		public GameObject startingParticles;                //starting flames under the rocket

		public static int startDelay = 3;
		public Texture2D[] number;                  //available number images
		private bool runOnce = false;               //flag for playing the countdown just once

		public AudioClip countSfx;
		public AudioClip launchSfx;

		//Scale of digits on the viewport
		private Vector3 startingScale = new Vector3(2, 2.65f, 0.001f);
		private Vector3 targetScale = new Vector3(3, 4, 0.001f);


		/// <summary>
		/// Init
		/// </summary>
		void Awake()
		{
			GetComponent<Renderer>().material.mainTexture = number[0];
			transform.localScale = startingScale;
			uiButtonUpgrade.SetActive(true);
			uiButtonNewSkin.SetActive(true);
			GetComponent<Renderer>().enabled = false;
			initCounter = false;
		}


		/// <summary>
		/// Start the countdown upon mouse/touch input
		/// </summary>
		void OnMouseDown()
		{
			if (!GameController.isGameStarted && !runOnce)
				StartCoroutine(countdown());
		}

		/// <summary>
		/// Init
		/// </summary>
		IEnumerator Start()
		{
			//set the correct name for the starting planet on UI
			if (GameController.startingPlanetID == 0)
				uiOnPlanetName.GetComponent<TextMeshPro>().text = "地球";
			
			else if (GameController.startingPlanetID == 1)
				uiOnPlanetName.GetComponent<TextMeshPro>().text = "火星";

			yield return new WaitForSeconds(0.75f);
			//for the PlanetName object, move it to right
			StartCoroutine(movePlanetNameToRight());
		}


		/// <summary>
		/// Countdown from 3 to 1.
		/// </summary>
		IEnumerator countdown()
		{
			runOnce = true;
			initCounter = true;

			GetComponent<Renderer>().enabled = true;
			tapToStartGo.SetActive(false);
			startingParticles.SetActive(true);

			GC.GetComponent<GameController>().enableUI(false);

			//hide ui buttons
			uiButtonUpgrade.SetActive(false);
			uiButtonNewSkin.SetActive(false);
			uiButtonHireAstronaut.SetActive(false);
			uiButtonSwitchStartingPlanet.SetActive(false);
			uiOnPlanetName.SetActive(false);

			//move the uiHiredAstronautCell to top of the UI
			StartCoroutine(moveInformationCellToTop());

			playSfx(countSfx);
			StartCoroutine(animate());
			yield return new WaitForSeconds(startDelay / 3);

			playSfx(countSfx);
			transform.localScale = startingScale;
			StartCoroutine(animate());
			GetComponent<Renderer>().material.mainTexture = number[1];
			yield return new WaitForSeconds(startDelay / 3);

			playSfx(countSfx);
			transform.localScale = startingScale;
			StartCoroutine(animate());
			GetComponent<Renderer>().material.mainTexture = number[2];
			yield return new WaitForSeconds(startDelay / 3);

			//start the game
			GameController.isGameStarted = true;

			//play sfx
			playSfx(launchSfx);

			//hide the counter
			GetComponent<Renderer>().enabled = false;

			yield return new WaitForSeconds(3);
			startingParticles.SetActive(false);
			Destroy(gameObject);
		}


		/// <summary>
		/// changes the scale of the given object over time
		/// </summary>
		IEnumerator animate()
		{
			float t = 0;
			while (t < 1)
			{
				t += Time.deltaTime * 1;
				transform.localScale = new Vector3(Mathf.SmoothStep(startingScale.x, targetScale.x, t),
												   Mathf.SmoothStep(startingScale.y, targetScale.y, t),
												   0.001f);
				yield return 0;
			}
		}


		/// <summary>
		/// Plaies the given audio
		/// </summary>
		/// <param name="_clip">Clip.</param>
		void playSfx(AudioClip _clip)
		{
			GetComponent<AudioSource>().clip = _clip;
			if (!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
		}


		/// <summary>
		/// Moves the information cell to top.
		/// </summary>
		IEnumerator moveInformationCellToTop()
		{
			Vector3 startingPosition = uiHiredAstronautCell.transform.position;
			float t = 0;
			while (t < 1)
			{
				t += Time.deltaTime * 1.2f;
				uiHiredAstronautCell.transform.position = new Vector3(Mathf.SmoothStep(startingPosition.x, 0, t),
																		Mathf.SmoothStep(startingPosition.y, 6.8f, t),
																		startingPosition.z);
				yield return 0;
			}
		}


		/// <summary>
		/// Moves the planet name text to right.
		/// </summary>
		IEnumerator movePlanetNameToRight()
		{
			Vector3 startingPosition = uiOnPlanetName.transform.localPosition;
			float startingTextSize = uiOnPlanetName.GetComponent<TextMeshPro>().fontSize;
			float t = 0;
			while (t < 1)
			{
				t += Time.deltaTime * 0.75f;
				uiOnPlanetName.transform.localPosition = new Vector3(Mathf.SmoothStep(startingPosition.x, -0.55f, t),
																	Mathf.SmoothStep(startingPosition.y, 0.75f, t),
																	startingPosition.z);

				uiOnPlanetName.GetComponent<TextMeshPro>().fontSize = Mathf.SmoothStep(startingTextSize, 0.08f, t);
				yield return 0;
			}
		}
	}
}