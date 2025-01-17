﻿using UnityEngine;
using System.Collections;
using UnityEngine.XR;

namespace VRStandardAssets.Examples
{ 
	public class ExampleRenderScale1 : MonoBehaviour
	{
		[SerializeField] private float m_RenderScale = 1f;              //The render scale. Higher numbers = better quality, but trades performance

		void Start ()
		{
			UnityEngine.XR.XRSettings.eyeTextureResolutionScale = m_RenderScale;
		}
	}
}