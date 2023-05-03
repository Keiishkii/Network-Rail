using System;
using UnityEngine.UIElements;

public class SquareElement : VisualElement
{
	private MainSide _type;

	public SquareElement()
	{
		RegisterCallback<GeometryChangedEvent>(GeometryChanged);
	}

	private void GeometryChanged(GeometryChangedEvent evt)
	{
		switch (_type)
		{
			case MainSide.Height:
				style.width = resolvedStyle.height;
				break;
			case MainSide.Width:
				style.height = resolvedStyle.width;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public new class UxmlTraits : VisualElement.UxmlTraits
	{
		private readonly UxmlEnumAttributeDescription<MainSide> _side = new()
			{ name = "Type" };

		public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
			base.Init(ve, bag, cc);
			((SquareElement)ve)._type = _side.GetValueFromBag(bag, cc);
		}
	}

	public new class UxmlFactory : UxmlFactory<SquareElement, UxmlTraits> { }

	private enum MainSide
	{
		Width,
		Height,
	}
}