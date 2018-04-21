using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/Joystick", 36), RequireComponent(typeof(RectTransform))]
	public class Joystick : UIBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler {
		
		[System.Serializable]
		public class JoystickMoveEvent : UnityEvent<Vector2> { }
		
		[SerializeField, Tooltip("The child graphic that will be moved around.")]
		private RectTransform m_Handle;
		public RectTransform Handle
		{
			get { return this.m_Handle; }
			set {
				this.m_Handle = value;
				UpdateHandle();
			}
		}
		
		[SerializeField, Tooltip("The handling area that the handle is allowed to be moved in.")]
		private RectTransform m_HandlingArea;
		public RectTransform HandlingArea
		{
			get { return this.m_HandlingArea; }
			set { this.m_HandlingArea = value; }
		}
		
		[SerializeField, Tooltip("The child graphic that will be shown when the joystick is active.")]
		private Image m_ActiveGraphic;
		public Image ActiveGraphic
		{
			get { return this.m_ActiveGraphic; }
			set { this.m_ActiveGraphic = value; }
		}
		
		[SerializeField] private Vector2 m_Axis;

		[SerializeField, Tooltip("How fast the joystick will go back to the center")]
		private float m_Spring = 25f;
		public float Spring
		{
			get { return this.m_Spring; }
			set { this.m_Spring = value; }
		}

		[SerializeField,  Tooltip("How close to the center that the axis will be output as 0")]
		private float m_DeadZone = 0.1f;
		public float DeadZone {
			get { return this.m_DeadZone; }
			set { this.m_DeadZone = value; }
		}
		
		[Tooltip("Customize the output that is sent in OnValueChange")]
		public AnimationCurve outputCurve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));

		public JoystickMoveEvent OnValueChange;

		private bool m_IsDragging = false;
		[HideInInspector] private bool m_DontCallEvent;

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();
			this.UpdateHandle();
		}
#endif
		
		protected override void OnEnable()
		{
			base.OnEnable();
			
			if (this.m_HandlingArea == null)
				this.m_HandlingArea = this.transform as RectTransform;
			
			if (this.m_ActiveGraphic != null)
				this.m_ActiveGraphic.canvasRenderer.SetAlpha(0f);
		}
		
		protected void Update()
		{
			if (this.isActiveAndEnabled && this.m_IsDragging)
			{
				if (!this.m_DontCallEvent)
				{
					if (this.OnValueChange != null)
						this.OnValueChange.Invoke(this.JoystickAxis);
				}
			}
		}
		
		protected void LateUpdate()
		{
			if (this.isActiveAndEnabled && !this.m_IsDragging)
			{
				if (this.m_Axis != Vector2.zero)
				{
					Vector2 newAxis = this.m_Axis - (this.m_Axis * Time.unscaledDeltaTime * this.m_Spring);
					
					if (newAxis.sqrMagnitude <= .0001f)
						newAxis = Vector2.zero;
					
					this.SetAxis(newAxis);
				}
			}
			
			this.m_DontCallEvent = false;
		}
		
		public Vector2 JoystickAxis {
			get {
				Vector2 outputPoint = this.m_Axis.magnitude > this.m_DeadZone ? this.m_Axis : Vector2.zero;
				float magnitude = outputPoint.magnitude;
				
				outputPoint *= outputCurve.Evaluate(magnitude);
				
				return outputPoint;
			}
			set { this.SetAxis(value); }
		}
		
		public void SetAxis(Vector2 axis)
		{
			this.m_Axis = Vector2.ClampMagnitude(axis, 1);
			
			Vector2 outputPoint = this.m_Axis.magnitude > this.m_DeadZone ? this.m_Axis : Vector2.zero;
			float magnitude = outputPoint.magnitude;
			
			outputPoint *= outputCurve.Evaluate(magnitude);
			
			if (!this.m_DontCallEvent)
			{
				if (this.OnValueChange != null)
				{
					this.OnValueChange.Invoke(outputPoint);
				}
			}
			
			this.UpdateHandle();
		}
		
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.IsActive())
				return;

			Vector2 newAxis = this.m_HandlingArea.InverseTransformPoint(eventData.position);
			newAxis.x /= this.m_HandlingArea.sizeDelta.x * 0.5f;
			newAxis.y /= this.m_HandlingArea.sizeDelta.y * 0.5f;

			this.SetAxis(newAxis);
			this.m_IsDragging = true;
			this.m_DontCallEvent = true;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			this.m_IsDragging = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			Vector2 axis = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandlingArea, eventData.position, eventData.pressEventCamera, out axis);
			
			axis -= this.m_HandlingArea.rect.center;
			axis.x /= this.m_HandlingArea.sizeDelta.x * 0.5f;
			axis.y /= this.m_HandlingArea.sizeDelta.y * 0.5f;
			
			this.SetAxis(axis);
			this.m_DontCallEvent = true;
		}
		
		private void UpdateHandle()
		{
			if (this.m_Handle)
			{
				this.m_Handle.anchoredPosition = new Vector2(this.m_Axis.x * this.m_HandlingArea.sizeDelta.x * 0.5f, this.m_Axis.y * this.m_HandlingArea.sizeDelta.y * 0.5f);
			}
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_ActiveGraphic != null)
				this.m_ActiveGraphic.CrossFadeAlpha(1f, 0.2f, false);
		}
		
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_ActiveGraphic != null)
				this.m_ActiveGraphic.CrossFadeAlpha(0f, 0.2f, false);
		}
	}
}