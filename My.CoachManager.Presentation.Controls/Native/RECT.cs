using System.Runtime.InteropServices;

namespace My.CoachManager.Presentation.Controls.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int left, top, right, bottom;
    }
}