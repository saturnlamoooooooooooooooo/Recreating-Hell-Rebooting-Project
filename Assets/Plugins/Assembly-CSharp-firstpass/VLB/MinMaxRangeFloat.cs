using System;
using UnityEngine;

namespace VLB
{
	[Serializable]
	public struct MinMaxRangeFloat
	{
		[SerializeField]
		private float m_MinValue;

		[SerializeField]
		private float m_MaxValue;

		public float minValue
		{
			get
			{
				return m_MinValue;
			}
		}

		public float maxValue
		{
			get
			{
				return m_MaxValue;
			}
		}

		public float randomValue
		{
			get
			{
				return UnityEngine.Random.Range(minValue, maxValue);
			}
		}

		public Vector2 asVector2
		{
			get
			{
				return new Vector2(minValue, maxValue);
			}
		}

		public float GetLerpedValue(float lerp01)
		{
			return Mathf.Lerp(minValue, maxValue, lerp01);
		}

		public MinMaxRangeFloat(float min, float max)
		{
			m_MinValue = min;
			m_MaxValue = max;
		}
	}
}
