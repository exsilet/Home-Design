using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace GGMatch3
{
	public class RocketPieceBehaviour : MonoBehaviour
	{
		private sealed class _003CDoRemoveFromGameAfter_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RocketPieceBehaviour _003C_003E4__this;

			public float duration;

			private float _003Ctime_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CDoRemoveFromGameAfter_003Ed__14(int _003C_003E1__state)
			{
				this._003C_003E1__state = _003C_003E1__state;
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				int num = _003C_003E1__state;
				RocketPieceBehaviour rocketPieceBehaviour = _003C_003E4__this;
				switch (num)
				{
				default:
					return false;
				case 0:
					_003C_003E1__state = -1;
					_003Ctime_003E5__2 = 0f;
					GGUtil.Hide(rocketPieceBehaviour.rocketTransform);
					break;
				case 1:
					_003C_003E1__state = -1;
					break;
				}
				if (_003Ctime_003E5__2 < duration)
				{
					_003Ctime_003E5__2 += Time.deltaTime;
					_003C_003E2__current = null;
					_003C_003E1__state = 1;
					return true;
				}
				rocketPieceBehaviour.RemoveFromGame();
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}
		}

		[SerializeField]
		private Transform rotatorTransform;

		[SerializeField]
		private Transform rocketTransform;

		[SerializeField]
		private List<Transform> verticalWidgets = new List<Transform>();

		[SerializeField]
		private List<Transform> horizontalWidgets = new List<Transform>();

		public Vector3 localPosition
		{
			get
			{
				return base.transform.localPosition;
			}
			set
			{
				base.transform.localPosition = value;
			}
		}

		public Vector3 localScale
		{
			get
			{
				return base.transform.localScale;
			}
			set
			{
				base.transform.localScale = value;
			}
		}

		public void SetDirection(IntVector2 direction)
		{
			Vector2 direction2 = new Vector2(direction.x, direction.y);
			SetDirection(direction2);
		}

		public void SetDirection(Vector2 direction)
		{
			float num = Vector3.Angle(Vector3.up, direction);
			if (direction.x < 0f || direction.y > 0f)
			{
				num += 180f;
			}
			rotatorTransform.localRotation = Quaternion.AngleAxis(num, Vector3.forward);
			GGUtil.SetActive(verticalWidgets, Mathf.Abs(direction.y) > 0f);
			GGUtil.SetActive(horizontalWidgets, Mathf.Abs(direction.x) > 0f);
		}

		public void Init()
		{
			base.gameObject.SetActive(value: true);
		}

		public void RemoveFromGameAfter(float duration)
		{
			GGUtil.Hide(rocketTransform);
			StartCoroutine(DoRemoveFromGameAfter(duration));
		}

		private IEnumerator DoRemoveFromGameAfter(float duration)
		{
			return new _003CDoRemoveFromGameAfter_003Ed__14(0)
			{
				_003C_003E4__this = this,
				duration = duration
			};
		}

		public void RemoveFromGame()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
