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
        bool DisegnaGliglia = false;

        public MainPage()
        {
            InitializeComponent();
            Title = "Gatto";
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
        
            canvas.Clear();

            //----------------------------------------

            SKPath griglia = CreaGriglia(5, 5);
            SKPaint pennelloGriglia = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.BlueViolet,
                StrokeWidth = 1
            };
            canvas.DrawPath(griglia, pennelloGriglia);

            //----------------------------------------

            SKPath path = new SKPath();

            SKPoint p = new SKPoint(gatto[0].X * zoom, 400-(gatto[0].Y * zoom));
            path.MoveTo( p );
            
            for (int x = 1; x < gatto.Count; x++)
            {
                p = new SKPoint(gatto[x].X * zoom, 400-(gatto[x].Y * zoom));
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

        private void btnApri_click(object sender, EventArgs e)
        {
            gatto.Clear();

            gatto.Add(new SKPoint(1, 5));
            gatto.Add(new SKPoint(1, 3));
            gatto.Add(new SKPoint(2, 2));
            gatto.Add(new SKPoint(3, 2));
            gatto.Add(new SKPoint(4, 3));
            gatto.Add(new SKPoint(4, 5));
            gatto.Add(new SKPoint(3, 4));
            gatto.Add(new SKPoint(2, 4));
            gatto.Add(new SKPoint(1, 5));

            canvasView.InvalidateSurface();
        }

        public SKPath CreaGriglia(int righe, int colonne)
        {
            SKPath path = new SKPath();

            int END_X = 300;
            int END_Y = 300;

            for (int riga = 0; riga < righe; riga++)
            {
                SKPoint pStart = new SKPoint(0, riga * zoom);
                SKPoint pStop = new SKPoint(END_X, riga * zoom);
                path.MoveTo(pStart);
                path.LineTo(pStop);
            }
            for (int colonna = 0; colonna < colonne; colonna++)
            {
                SKPoint pStart = new SKPoint(colonna*zoom, 0);
                SKPoint pStop = new SKPoint(colonna * zoom, END_Y);
                path.MoveTo(pStart);
                path.LineTo(pStop);
            }

            return path;
        }

        private void btnGliglia_click(object sender, EventArgs e)
        {

        }
    }
}
