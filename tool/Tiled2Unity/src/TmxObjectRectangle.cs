﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Tiled2Unity
{
    class TmxObjectRectangle : TmxObjectPolygon
    {
        protected override void InternalFromXml(System.Xml.Linq.XElement xml, TmxMap tmxMap)
        {
            this.Points = new List<System.Drawing.PointF>();
            this.Points.Add(new PointF(0, 0));
            this.Points.Add(new PointF(this.Size.Width, 0));
            this.Points.Add(new PointF(this.Size.Width, this.Size.Height));
            this.Points.Add(new PointF(0, this.Size.Height));

            if (this.Size.Width == 0 || this.Size.Height == 0)
            {
                Program.WriteWarning("Warning: Rectangle has zero width or height in object group\n{0}", xml.Parent.ToString());
            }
        }

        protected override string InternalGetDefaultName()
        {
            return "RectangleObject";
        }

    }
}
