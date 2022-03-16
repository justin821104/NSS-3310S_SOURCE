using freeLicence;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class FREE_
{
    public static Class1 FREE = new Class1();
}
public class Win32API_
{
    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(Point pt);

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

    [DllImport("user32.dll")]
    public static extern IntPtr SetCapture(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr ReleaseCapture();

    /// <summary>
    /// 원도우 핸들 포커스 최상위 활성화
    /// </summary>
    /// <param name="hwnd"></param>
    /// <returns></returns>
    [DllImport("user32")]
    public static extern int SetForegroundWindow(IntPtr hwnd);
}

public class Gdi32
{
    public enum BinaryRasterOperations
    {
        R2_BLACK = 1,
        R2_NOTMERGEPEN = 2,
        R2_MASKNOTPEN = 3,
        R2_NOTCOPYPEN = 4,
        R2_MASKPENNOT = 5,
        R2_NOT = 6,
        R2_XORPEN = 7,
        R2_NOTMASKPEN = 8,
        R2_MASKPEN = 9,
        R2_NOTXORPEN = 10,
        R2_NOP = 11,
        R2_MERGENOTPEN = 12,
        R2_COPYPEN = 13,
        R2_MERGEPENNOT = 14,
        R2_MERGEPEN = 15,
        R2_WHITE = 16
    }

    public enum PenStyle : int
    {
        PS_SOLID = 0, //The pen is solid.
        PS_DASH = 1, //The pen is dashed.
        PS_DOT = 2, //The pen is dotted.
        PS_DASHDOT = 3, //The pen has alternating dashes and dots.
        PS_DASHDOTDOT = 4, //The pen has alternating dashes and double dots.
        PS_NULL = 5, //The pen is invisible.
        PS_INSIDEFRAME = 6,// Normally when the edge is drawn, it’s centred on the outer edge meaning that half the width of the pen is drawn
        // outside the shape’s edge, half is inside the shape’s edge. When PS_INSIDEFRAME is specified the edge is drawn 
        //completely inside the outer edge of the shape.
        PS_USERSTYLE = 7,
        PS_ALTERNATE = 8,
        PS_STYLE_MASK = 0x0000000F,

        PS_ENDCAP_ROUND = 0x00000000,
        PS_ENDCAP_SQUARE = 0x00000100,
        PS_ENDCAP_FLAT = 0x00000200,
        PS_ENDCAP_MASK = 0x00000F00,

        PS_JOIN_ROUND = 0x00000000,
        PS_JOIN_BEVEL = 0x00001000,
        PS_JOIN_MITER = 0x00002000,
        PS_JOIN_MASK = 0x0000F000,

        PS_COSMETIC = 0x00000000,
        PS_GEOMETRIC = 0x00010000,
        PS_TYPE_MASK = 0x000F0000
    };

    #region ### RECT ###
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

        public int X
        {
            get { return Left; }
            set { Right -= (Left - value); Left = value; }
        }

        public int Y
        {
            get { return Top; }
            set { Bottom -= (Top - value); Top = value; }
        }

        public int Height
        {
            get { return Bottom - Top; }
            set { Bottom = value + Top; }
        }

        public int Width
        {
            get { return Right - Left; }
            set { Right = value + Left; }
        }

        public System.Drawing.Point Location
        {
            get { return new System.Drawing.Point(Left, Top); }
            set { X = value.X; Y = value.Y; }
        }

        public System.Drawing.Size Size
        {
            get { return new System.Drawing.Size(Width, Height); }
            set { Width = value.Width; Height = value.Height; }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT)obj);
            else if (obj is System.Drawing.Rectangle)
                return Equals(new RECT((System.Drawing.Rectangle)obj));
            return false;
        }

        public override int GetHashCode()
        {
            return ((System.Drawing.Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }

    #endregion

    [DllImport("gdi32.dll")]
    public static extern bool Rectangle(IntPtr hDC, int left, int top, int right, int bottom);

    [DllImport("gdi32.dll")]
    public static extern int SetROP2(IntPtr hDC, int fnDrawMode);

    [DllImport("gdi32.dll")]
    public static extern bool MoveToEx(IntPtr hDC, int x, int y, ref Point p);

    [DllImport("gdi32.dll")]
    public static extern bool LineTo(IntPtr hdc, int x, int y);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);

    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObj);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [DllImport("user32.dll")]
    public static extern int FillRect(IntPtr hDC, [In] ref RECT lprc, IntPtr hbr);

    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateSolidBrush(int crColor);

}

/// <summary>
/// Provides utilities directly accessing the gdi32.dll 
/// </summary>
public static class GDI
{
    static private Point nullPoint = new Point(0, 0);

    // Convert the Argb from .NET to a gdi32 RGB
    static private int ArgbToRGB(int rgb)
    {
        return ((rgb >> 16 & 0x0000FF) | (rgb & 0x00FF00) | (rgb << 16 & 0xFF0000));
    }
    static public void DrawXorRectangle(this System.Drawing.Graphics graphics, Pen pen, Rectangle rectangle)
    {
        IntPtr hDC = graphics.GetHdc();
        IntPtr hPen = Gdi32.CreatePen((int)Gdi32.PenStyle.PS_SOLID, (int)pen.Width, ArgbToRGB(pen.Color.ToArgb()));
        Gdi32.SelectObject(hDC, hPen);
        int nRop2 = Gdi32.SetROP2(hDC, (int)Gdi32.BinaryRasterOperations.R2_NOTXORPEN);
        Gdi32.Rectangle(hDC, rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        Gdi32.DeleteObject(hPen);
        Gdi32.SetROP2(hDC, nRop2);
        graphics.ReleaseHdc(hDC);
    }

    static public void FillRectangle(this System.Drawing.Graphics graphics, Color color, Rectangle rectangle)
    {
        Gdi32.RECT rect = new Gdi32.RECT(rectangle);

        IntPtr hDC = graphics.GetHdc();
        IntPtr hBrush = Gdi32.CreateSolidBrush(ArgbToRGB(color.ToArgb()));
        Gdi32.FillRect(hDC, ref rect, hBrush);
        Gdi32.DeleteObject(hBrush);
        graphics.ReleaseHdc(hDC);
    }

    static public void DrawXorLine(this System.Drawing.Graphics graphics, Pen pen, int x1, int y1, int x2, int y2)
    {
        IntPtr hDC = graphics.GetHdc();
        IntPtr hPen = Gdi32.CreatePen(0, (int)pen.Width, ArgbToRGB(pen.Color.ToArgb()));
        Gdi32.SelectObject(hDC, hPen);
        int nRop2 = Gdi32.SetROP2(hDC, (int)Gdi32.BinaryRasterOperations.R2_NOTXORPEN);
        Gdi32.MoveToEx(hDC, x1, y1, ref nullPoint);
        Gdi32.LineTo(hDC, x2, y2);
        Gdi32.DeleteObject(hPen);
        Gdi32.SetROP2(hDC, nRop2);
        graphics.ReleaseHdc(hDC);
    }
}