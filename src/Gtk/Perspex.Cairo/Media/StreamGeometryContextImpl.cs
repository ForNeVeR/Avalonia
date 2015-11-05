﻿// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using Perspex.Media;
using Perspex.Platform;
using Perspex.RenderHelpers;

namespace Perspex.Cairo.Media
{
    using Cairo = global::Cairo;

    public class StreamGeometryContextImpl : IStreamGeometryContextImpl
    {
        private Point _currentPoint;
		public StreamGeometryContextImpl(Cairo.Path path = null)
        {

			_surf = new Cairo.ImageSurface (Cairo.Format.Argb32, 0, 0);
			_context = new Cairo.Context (_surf);
			this.Path = path;

			if (this.Path != null) 
			{
				_context.AppendPath(this.Path);
			}
        }

        public void ArcTo(Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection)
        {
            ArcToHelper.ArcTo(this, _currentPoint, point, size, rotationAngle, isLargeArc, sweepDirection);
            _currentPoint = point;
        }

        public void BeginFigure(Point startPoint, bool isFilled)
        {
            if (this.Path == null)
            {
                _context.MoveTo(startPoint.ToCairo());
                _currentPoint = startPoint;
            }
        }

        public void BezierTo(Point point1, Point point2, Point point3)
        {
            if (this.Path == null)
            {
                _context.CurveTo(point1.ToCairo(), point2.ToCairo(), point3.ToCairo());
                _currentPoint = point3;
            }
        }

        public void QuadTo(Point control, Point endPoint)
        {
            if (this.Path == null)
            {
                QuadBezierHelper.QuadTo(this, _currentPoint, control, endPoint);
                _currentPoint = endPoint;
            }
        }

        public void LineTo(Point point)
        {
            if (this.Path == null)
            {
                _context.LineTo(point.ToCairo());
                _currentPoint = point;
            }
        }

        private readonly Cairo.Context _context;
        private readonly Cairo.ImageSurface _surf;
		public Cairo.Path Path { get; private set; }
		public Rect Bounds { get; private set; }

        public void EndFigure(bool isClosed)
        {
			if (this.Path == null) 
			{
				if (isClosed)
					_context.ClosePath ();

				Path = _context.CopyPath ();
				Bounds = _context.FillExtents ().ToPerspex ();
			}
        }

        public void Dispose()
        {
			_context.Dispose ();
			_surf.Dispose ();
        }
    }
}
