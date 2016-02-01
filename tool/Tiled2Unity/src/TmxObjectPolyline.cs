﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tiled2Unity
{
    class TmxObjectPolyline : TmxObject, TmxHasPoints
    {
        public List<PointF> Points { get; protected set; }

        public TmxObjectPolyline()
        {
            this.Points = new List<PointF>();
        }

        public override RectangleF GetWorldBounds()
        {
            float xmin = float.MaxValue;
            float xmax = float.MinValue;
            float ymin = float.MaxValue;
            float ymax = float.MinValue;

            foreach (var p in this.Points)
            {
                xmin = Math.Min(xmin, p.X);
                xmax = Math.Max(xmax, p.X);
                ymin = Math.Min(ymin, p.Y);
                ymax = Math.Max(ymax, p.Y);
            }

            RectangleF bounds = new RectangleF(xmin, ymin, xmax - xmin, ymax - ymin);
            bounds.Offset(this.Position);
            return bounds;
        }

        protected override void InternalFromXml(System.Xml.Linq.XElement xml, TmxMap tmxMap)
        {
            Debug.Assert(xml.Name == "object");
            Debug.Assert(xml.Element("polyline") != null);

            var points = from pt in xml.Element("polyline").Attribute("points").Value.Split(' ')
                         let x = float.Parse(pt.Split(',')[0])
                         let y = float.Parse(pt.Split(',')[1])
                         select new PointF(x, y);

            this.Points = points.ToList();
            float grid = 4;
            for (int i = 0; i < Points.Count; i++)
            {
                PointF pt = Points[i];
                int x = (int)(Math.Round(pt.X / grid) * grid);
                int y = (int)(Math.Round(pt.Y / grid) * grid);
                Points[i] = new PointF(x, y);
            }
        }

        protected override string InternalGetDefaultName()
        {
            return "PolylineObject";
        }

        public bool ArePointsClosed()
        {
            // Lines are open
            return false;
        }
    }
}
