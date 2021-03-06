﻿using System;
using System.Drawing;
using System.Text;
using ReClassNET.Nodes;
using ReClassNET.UI;

namespace UnrealEngineClassesPlugin.Nodes
{
	public class FStringNode : BaseNode
	{
		public override int MemorySize => IntPtr.Size + sizeof(int) * 2;

		public override Size Draw(ViewInfo view, int x, int y)
		{
			if (IsHidden)
			{
				return DrawHidden(view, x, y);
			}

			var ptr = view.Memory.ReadIntPtr(Offset);
			var length = view.Memory.ReadInt32(Offset + IntPtr.Size);
			var text = view.Memory.Process.ReadRemoteString(Encoding.Unicode, ptr, length);

			DrawInvalidMemoryIndicator(view, y);

			var origX = x;

			AddSelection(view, x, y, view.Font.Height);

			x += TextPadding;
			x = AddIcon(view, x, y, Icons.Text, HotSpot.NoneId, HotSpotType.None);
			x = AddAddressOffset(view, x, y);

			x = AddText(view, x, y, view.Settings.TypeColor, HotSpot.NoneId, "FString") + view.Font.Width;
			x = AddText(view, x, y, view.Settings.NameColor, HotSpot.NameId, Name) + view.Font.Width;

			x = AddText(view, x, y, view.Settings.TextColor, HotSpot.NoneId, "= '");
			x = AddText(view, x, y, view.Settings.TextColor, HotSpot.NoneId, text);
			x = AddText(view, x, y, view.Settings.TextColor, HotSpot.NoneId, "'") + view.Font.Width;

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
