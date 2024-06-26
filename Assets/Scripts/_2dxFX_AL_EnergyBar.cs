using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[Serializable]
[ExecuteInEditMode]
public class _2dxFX_AL_EnergyBar : MonoBehaviour
{
	public Material ForceMaterial;

	public bool ActiveChange = true;

	public bool AddShadow = true;

	public bool ReceivedShadow;

	public int BlendMode;

	private string shader = "2DxFX/AL/EnergyBar";

	public float _Alpha = 1f;

	public float BarProgress = 0.5f;

	public float _Value2 = 0.975f;

	public float _Value3 = 0.5f;

	public float _Value4 = 1f;

	public float _Value5;

	public int ShaderChange;

	private Material tempMaterial;

	private Material defaultMaterial;

	private Image CanvasImage;

	private SpriteRenderer CanvasSpriteRenderer;

	public bool ActiveUpdate = true;

	private void Awake()
	{
		if (base.gameObject.GetComponent<Image>() != null)
		{
			CanvasImage = base.gameObject.GetComponent<Image>();
		}
		if (base.gameObject.GetComponent<SpriteRenderer>() != null)
		{
			CanvasSpriteRenderer = base.gameObject.GetComponent<SpriteRenderer>();
		}
	}

	private void Start()
	{
		ShaderChange = 0;
		XUpdate();
	}

	public void CallUpdate()
	{
		XUpdate();
	}

	private void Update()
	{
		if (ActiveUpdate)
		{
			XUpdate();
		}
	}

	private void XUpdate()
	{
		if (CanvasImage == null && base.gameObject.GetComponent<Image>() != null)
		{
			CanvasImage = base.gameObject.GetComponent<Image>();
		}
		if (CanvasSpriteRenderer == null && base.gameObject.GetComponent<SpriteRenderer>() != null)
		{
			CanvasSpriteRenderer = base.gameObject.GetComponent<SpriteRenderer>();
		}
		if (BarProgress > 1f)
		{
			BarProgress = 1f;
		}
		if (BarProgress < 0f)
		{
			BarProgress = 0f;
		}
		if (ShaderChange == 0 && ForceMaterial != null)
		{
			ShaderChange = 1;
			if (tempMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(tempMaterial);
			}
			if (CanvasSpriteRenderer != null)
			{
				CanvasSpriteRenderer.sharedMaterial = ForceMaterial;
			}
			else if (CanvasImage != null)
			{
				CanvasImage.material = ForceMaterial;
			}
			ForceMaterial.hideFlags = HideFlags.None;
			ForceMaterial.shader = Shader.Find(shader);
		}
		if (ForceMaterial == null && ShaderChange == 1)
		{
			if (tempMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(tempMaterial);
			}
			tempMaterial = new Material(Shader.Find(shader));
			tempMaterial.hideFlags = HideFlags.None;
			if (CanvasSpriteRenderer != null)
			{
				CanvasSpriteRenderer.sharedMaterial = tempMaterial;
			}
			else if (CanvasImage != null)
			{
				CanvasImage.material = tempMaterial;
			}
			ShaderChange = 0;
		}
		if (!ActiveChange)
		{
			return;
		}
		if (CanvasSpriteRenderer != null)
		{
			CanvasSpriteRenderer.sharedMaterial.SetFloat("_Alpha", 1f - _Alpha);
			if (_2DxFX.ActiveShadow && AddShadow)
			{
				CanvasSpriteRenderer.shadowCastingMode = ShadowCastingMode.On;
				if (ReceivedShadow)
				{
					CanvasSpriteRenderer.receiveShadows = true;
					CanvasSpriteRenderer.sharedMaterial.renderQueue = 2450;
					CanvasSpriteRenderer.sharedMaterial.SetInt("_Z", 1);
				}
				else
				{
					CanvasSpriteRenderer.receiveShadows = false;
					CanvasSpriteRenderer.sharedMaterial.renderQueue = 3000;
					CanvasSpriteRenderer.sharedMaterial.SetInt("_Z", 0);
				}
			}
			else
			{
				CanvasSpriteRenderer.shadowCastingMode = ShadowCastingMode.Off;
				CanvasSpriteRenderer.receiveShadows = false;
				CanvasSpriteRenderer.sharedMaterial.renderQueue = 3000;
				CanvasSpriteRenderer.sharedMaterial.SetInt("_Z", 0);
			}
			if (BlendMode == 0)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 0);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 1);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 10);
			}
			if (BlendMode == 1)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 0);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 1);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 1);
			}
			if (BlendMode == 2)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 2);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 1);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 2);
			}
			if (BlendMode == 3)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 4);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 1);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 1);
			}
			if (BlendMode == 4)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 2);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 1);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 1);
			}
			if (BlendMode == 5)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 4);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 10);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 10);
			}
			if (BlendMode == 6)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 0);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 2);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 10);
			}
			if (BlendMode == 7)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 0);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 4);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 1);
			}
			if (BlendMode == 8)
			{
				CanvasSpriteRenderer.sharedMaterial.SetInt("_BlendOp", 2);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_SrcBlend", 7);
				CanvasSpriteRenderer.sharedMaterial.SetInt("_DstBlend", 2);
			}
			CanvasSpriteRenderer.sharedMaterial.SetFloat("_Value1", BarProgress);
			CanvasSpriteRenderer.sharedMaterial.SetFloat("_Value2", 1f - _Value2);
			CanvasSpriteRenderer.sharedMaterial.SetFloat("_Value3", 1f - _Value3);
			CanvasSpriteRenderer.sharedMaterial.SetFloat("_Value4", _Value4);
			CanvasSpriteRenderer.sharedMaterial.SetFloat("_Value5", _Value5);
		}
		else if (CanvasImage != null)
		{
			CanvasImage.material.SetFloat("_Alpha", 1f - _Alpha);
			CanvasImage.material.SetFloat("_Value1", BarProgress);
			CanvasImage.material.SetFloat("_Value2", 1f - _Value2);
			CanvasImage.material.SetFloat("_Value3", 1f - _Value3);
			CanvasImage.material.SetFloat("_Value4", _Value4);
			CanvasImage.material.SetFloat("_Value5", _Value5);
		}
	}

	private void OnDestroy()
	{
		if (Application.isPlaying || !Application.isEditor)
		{
			return;
		}
		if (tempMaterial != null)
		{
			UnityEngine.Object.DestroyImmediate(tempMaterial);
		}
		if (base.gameObject.activeSelf && defaultMaterial != null)
		{
			if (CanvasSpriteRenderer != null)
			{
				CanvasSpriteRenderer.sharedMaterial = defaultMaterial;
				CanvasSpriteRenderer.sharedMaterial.hideFlags = HideFlags.None;
			}
			else if (CanvasImage != null)
			{
				CanvasImage.material = defaultMaterial;
				CanvasImage.material.hideFlags = HideFlags.None;
			}
		}
	}

	private void OnDisable()
	{
		if (base.gameObject.activeSelf && defaultMaterial != null)
		{
			if (CanvasSpriteRenderer != null)
			{
				CanvasSpriteRenderer.sharedMaterial = defaultMaterial;
				CanvasSpriteRenderer.sharedMaterial.hideFlags = HideFlags.None;
			}
			else if (CanvasImage != null)
			{
				CanvasImage.material = defaultMaterial;
				CanvasImage.material.hideFlags = HideFlags.None;
			}
		}
	}

	private void OnEnable()
	{
		if (defaultMaterial == null)
		{
			defaultMaterial = new Material(Shader.Find("Sprites/Default"));
		}
		if (ForceMaterial == null)
		{
			ActiveChange = true;
			tempMaterial = new Material(Shader.Find(shader));
			tempMaterial.hideFlags = HideFlags.None;
			if (CanvasSpriteRenderer != null)
			{
				CanvasSpriteRenderer.sharedMaterial = tempMaterial;
			}
			else if (CanvasImage != null)
			{
				CanvasImage.material = tempMaterial;
			}
		}
		else
		{
			ForceMaterial.shader = Shader.Find(shader);
			ForceMaterial.hideFlags = HideFlags.None;
			if (CanvasSpriteRenderer != null)
			{
				CanvasSpriteRenderer.sharedMaterial = ForceMaterial;
			}
			else if (CanvasImage != null)
			{
				CanvasImage.material = ForceMaterial;
			}
		}
	}
}
