﻿using System.Drawing;
using ReClassNET.Nodes;
using ReClassNET.UI;

namespace UnrealEngineClassesPlugin.Nodes
{
	public class FQWordNode : BaseNode
	{
		public override int MemorySize => 8;

		public override Size Draw(ViewInfo view, int x, int y)
		{
			if (IsHidden)
			{
				return DrawHidden(view, x, y);
			}

			DrawInvalidMemoryIndicator(view, y);

			var origX = x;

			AddSelection(view, x, y, view.Font.Height);

			x += TextPadding;

			x = AddIcon(view, x, y, Icons.Class, HotSpot.NoneId, HotSpotType.None);
			x = AddAddressOffset(view, x, y);

			x = AddText(view, x, y, view.Settings.TypeColor, HotSpot.NoneId, "FQWord") + view.Font.Width;
			x = AddText(view, x, y, view.Settings.NameColor, HotSpot.NameId, Name) + view.Font.Width;
			x = AddText(view, x, y, view.Settings.NameColor, HotSpot.NoneId, "=") + view.Font.Width;

			var a = view.Memory.ReadInt32(Offset);
			var b = view.Memory.ReadInt32(Offset + sizeof(int));

			x = AddText(view, x, y, view.Settings.ValueColor, 0, $"(A: {a}, B: {b})") + view.Font.Width;

			x = AddComment(view, x, y);

			AddTypeDrop(view, y);
			AddDelete(view, y);

			return new Size(x - origX, view.Font.Height);
		}

		public override int CalculateDrawnHeight(ViewInfo view)
		{
			return IsHidden ? HiddenHeight : view.Font.Height;
		}
	}
}
