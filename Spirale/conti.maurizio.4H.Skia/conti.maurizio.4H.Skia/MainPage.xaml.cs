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
        const double cycleTime = 250;       // in milliseconds
        Stopwatch stopwatch = new Stopwatch();
        bool pageIsActive;
        float dashPhase;

        public MainPage()
        {
            InitializeComponent();
            Title = "Simple Circle";
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float radius = Math.Min(center.X, center.Y);

            using (SKPath path = new SKPath())
            {
                for (float angle = 0; angle < 3600; angle += 1)
                {
                    float scaledRadius = radius * angle / 3600;
                    double radians = Math.PI * angle / 180;
                    float x = center.X + scaledRadius * (float)Math.Cos(radians);
                    float y = center.Y + scaledRadius * (float)Math.Sin(radians);
                    SKPoint point = new SKPoint(x, y);

                    if (angle == 0)
                        path.MoveTo(point);
                    else
                        path.LineTo(point);
                }

                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Red,
                    StrokeWidth = 5,
                    StrokeCap = (SKStrokeCap)strokeCapPicker.SelectedItem,
                    PathEffect = SKPathEffect.CreateDash(GetPickerArray(dashArrayPicker), dashPhase)
                };

                canvas.DrawPath(path, paint);
            }
        }

        float[] GetPickerArray(Picker picker)
        {
            if (picker.SelectedIndex == -1)
                return new float[0];

            string str = (string)picker.SelectedItem;
            string[] strs = str.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            float[] array = new float[strs.Length];

            for (int i = 0; i < strs.Length; i++)
                array[i] = Convert.ToSingle(strs[i]);

            return array;
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            canvasView.InvalidateSurface();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            base.OnAppearing();
            pageIsActive = true;
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                dashPhase = (float)(dashPhase + 1);
                canvasView.InvalidateSurface();

                if (!pageIsActive)
                    stopwatch.Stop();

                return pageIsActive;
            });
        }

        private void ContentPage_Disappearing(object sender, EventArgs e)
        {
            base.OnDisappearing();
            pageIsActive = false;
        }
    }
}
