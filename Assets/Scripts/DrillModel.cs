using UnityEngine;

public class DrillModel : MonoBehaviour
{
	[SerializeField]
	private Transform turnerTransform;

	public void Show(Vector3 position, Quaternion rotation)
	{
		GGUtil.Show(this);
		base.transform.position = position;
		base.transform.rotation = rotation;
	}

	public void SetActive(bool active)
	{
		GGUtil.SetActive(this, active);
	}

	public void Hide()
	{
		GGUtil.Hide(this);
	}
}
