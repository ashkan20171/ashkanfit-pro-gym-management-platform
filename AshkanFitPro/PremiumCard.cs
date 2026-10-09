using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace AshkanFitPro {
/// <summary>Reusable anti-aliased, rounded workspace surface.</summary>
public sealed class PremiumCard : Panel {
 int radius=18;
 public int Radius { get {return radius;} set {radius=Math.Max(0,value);Invalidate();} }
 public PremiumCard(){SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint|ControlStyles.OptimizedDoubleBuffer|ControlStyles.ResizeRedraw,true);}
 protected override void OnPaintBackground(PaintEventArgs e){
  e.Graphics.SmoothingMode=SmoothingMode.AntiAlias;
  using(var path=Shape(ClientRectangle,Radius))using(var fill=new SolidBrush(BackColor)){e.Graphics.FillPath(fill,path);}
 }
 protected override void OnPaint(PaintEventArgs e){base.OnPaint(e);if(Width<2||Height<2)return;using(var path=Shape(new Rectangle(0,0,Width-1,Height-1),Radius))using(var pen=new Pen(Color.FromArgb(45,76,96))){e.Graphics.SmoothingMode=SmoothingMode.AntiAlias;e.Graphics.DrawPath(pen,path);}}
 static GraphicsPath Shape(Rectangle r,int rad){var path=new GraphicsPath();int d=Math.Min(Math.Min(r.Width,r.Height),rad*2);if(d<=0){path.AddRectangle(r);return path;}path.AddArc(r.X,r.Y,d,d,180,90);path.AddArc(r.Right-d,r.Y,d,d,270,90);path.AddArc(r.Right-d,r.Bottom-d,d,d,0,90);path.AddArc(r.X,r.Bottom-d,d,d,90,90);path.CloseFigure();return path;}
}
}
