using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace AshkanFitPro {
 public sealed class PremiumHeroPanel : Panel {
  public string TitleText {get;set;} = "POWER YOUR GYM";
  public string SubtitleText {get;set;} = "Members, training and finances";
  public PremiumHeroPanel(){DoubleBuffered=true;ResizeRedraw=true;}
  protected override void OnPaint(PaintEventArgs e){
   base.OnPaint(e); var g=e.Graphics;g.SmoothingMode=SmoothingMode.AntiAlias;var r=ClientRectangle;if(r.Width<2||r.Height<2)return;
   using(var path=RoundRect(new Rectangle(0,0,r.Width-1,r.Height-1),20))
   using(var fill=new LinearGradientBrush(r,Color.FromArgb(17,90,103),Color.FromArgb(13,34,58),0f)){g.FillPath(fill,path);}
   using(var ring=new Pen(Color.FromArgb(70,57,228,195),2)){for(int i=0;i<5;i++)g.DrawEllipse(ring,r.Width-170-i*18,-95-i*18,230+i*36,230+i*36);}
   bool rtl=RightToLeft==RightToLeft.Yes;var area=new Rectangle(26,24,Math.Max(1,r.Width-52),48);
   using(var f=new Font("Segoe UI",19,FontStyle.Bold))using(var b=new SolidBrush(Color.White))using(var sf=new StringFormat{Alignment=rtl?StringAlignment.Far:StringAlignment.Near})g.DrawString(TitleText,f,b,area,sf);
   area.Y=79;area.Height=30;using(var f=new Font("Segoe UI",10))using(var b=new SolidBrush(Color.FromArgb(200,222,235)))using(var sf=new StringFormat{Alignment=rtl?StringAlignment.Far:StringAlignment.Near})g.DrawString(SubtitleText,f,b,area,sf);
  }
  static GraphicsPath RoundRect(Rectangle r,int radius){var p=new GraphicsPath();int d=radius*2;p.AddArc(r.Left,r.Top,d,d,180,90);p.AddArc(r.Right-d,r.Top,d,d,270,90);p.AddArc(r.Right-d,r.Bottom-d,d,d,0,90);p.AddArc(r.Left,r.Bottom-d,d,d,90,90);p.CloseFigure();return p;}
 }
}
