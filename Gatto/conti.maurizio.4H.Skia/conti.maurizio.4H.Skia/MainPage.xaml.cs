using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Diagnostics;


// https://docs.microsoft.com/it-it/xamarin/xamarin-forms/user-interface/graphics/skiasharp/paths/polylines

namespace conti.maurizio._4H.Skia
{
    public partial class MainPage : ContentPage
    {
        List<SKPoint> gatto = new List<SKPoint>();
        float zoom = 70;

        public MainPage()
        {
            InitializeComponent();
            Title = "Gatto";

            gatto.Add(new SKPoint(1, 5));
            gatto.Add(new SKPoint(1, 3));
            gatto.Add(new SKPoint(2, 2));
            gatto.Add(new SKPoint(3, 2));
            gatto.Add(new SKPoint(4, 3));
            gatto.Add(new SKPoint(4, 5));
            gatto.Add(new SKPoint(3, 4));
            gatto.Add(new SKPoint(2, 4));
            gatto.Add(new SKPoint(1, 5));
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
        
            canvas.Clear();

            SKPath path = new SKPath();

            SKPoint p = new SKPoint(gatto[0].X * zoom, gatto[0].Y * zoom);
            path.MoveTo( p );
            
            for (int x = 1; x < gatto.Count; x++)
            {
                p = new SKPoint(gatto[x].X * zoom, gatto[x].Y * zoom);
                path.LineTo(p);
            }

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Red,
                StrokeWidth = 5
            };

            canvas.DrawPath(path, paint);
        }
    }
}
