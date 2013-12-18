using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MappingApplication
{
    class RotationHelper
    {
        private Dictionary<int, TouchPoint> startTouchPoints = new Dictionary<int, TouchPoint>();
        private Dictionary<int, TouchPoint> deltaTouchPoints = new Dictionary<int, TouchPoint>();
        private Line startLine;
        private Line deltaLine;
        private double lastSavedAngle = 0;

        private Map rotatedMap;
        private DateTime lastChangeTimestamp = System.DateTime.Now;
        private System.Device.Location.GeoCoordinate rotateCenter;

        public RotationHelper(Map rotatedMap)
        {

            Touch.FrameReported += this.Touch_FrameReported;

            this.rotatedMap = rotatedMap;

        }

        public void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            bool moveOccurred = false;
            TouchPointCollection pts = e.GetTouchPoints(rotatedMap);

            foreach (TouchPoint p in pts)
            {
                if (p.Action == TouchAction.Down)
                {
                    if (!startTouchPoints.ContainsKey(p.TouchDevice.Id))
                    {
                        startTouchPoints.Add(p.TouchDevice.Id, p);
                        deltaTouchPoints.Add(p.TouchDevice.Id, p);
                    }
                    if (startTouchPoints.Count > 1)
                    {
                        startLine = calculateLine(startTouchPoints);
                        Debug.WriteLine("Start line set to: {0:f}, {1:f}, {2:f}, {3:f}", startLine.X1, startLine.Y1, startLine.X2, startLine.Y2);
                        rotateCenter = this.rotatedMap.Center;
                    }
                }
                else if (p.Action == TouchAction.Move)
                {
                    TouchPoint oldPositionTouchpoint;
                    // updating the touchpoints set
                    if (deltaTouchPoints.TryGetValue(p.TouchDevice.Id, out oldPositionTouchpoint))
                    {
                        deltaTouchPoints[p.TouchDevice.Id] = p;
                    }
                    // if we are already in rotating, compute the angle and update the map view
                    if (deltaTouchPoints.Count == 2)
                    {
                        deltaLine = calculateLine(deltaTouchPoints);
                        Debug.WriteLine("Delta line set to: {0:f}, {1:f}, {2:f}, {3:f}", deltaLine.X1, deltaLine.Y1, deltaLine.X2, deltaLine.Y2);
                        moveOccurred = true;
                    }
                }
                else if (p.Action == TouchAction.Up)
                {
                    startTouchPoints.Remove(p.TouchDevice.Id);
                    deltaTouchPoints.Remove(p.TouchDevice.Id);
                    startLine = null;
                    deltaLine = null;
                    this.lastSavedAngle = this.rotatedMap.Heading;
                }
            }

            if (moveOccurred)
            {
                double angle = angleBetween2Lines(startLine, deltaLine);
                Debug.WriteLine("Angle difference to startline: " + angle);
                this.rotatedMap.SetView(rotateCenter, this.rotatedMap.ZoomLevel, lastSavedAngle + angle, MapAnimationKind.Linear);
            }
        }

        public static Line calculateLine(Dictionary<int, TouchPoint> points)
        {
            Line line = null;
            TouchPoint startPoint;
            TouchPoint endPoint;
            int key1 = points.Keys.ElementAt(0);
            int key2 = points.Keys.ElementAt(1);
            if (points.TryGetValue(key1, out startPoint) && points.TryGetValue(key2, out endPoint))
            {
                line =
                    new Line
                    {
                        X1 = startPoint.Position.X,
                        Y1 = startPoint.Position.Y,
                        X2 = endPoint.Position.X,
                        Y2 = endPoint.Position.Y
                    };
            }
            return line;
        }

        public static double angleBetween2Lines(Line line1, Line line2)
        {
            if (line1 != null && line2 != null)
            {
                double angle1 = Math.Atan2(line1.Y1 - line1.Y2,
                                           line1.X1 - line1.X2);
                double angle2 = Math.Atan2(line2.Y1 - line2.Y2,
                                           line2.X1 - line2.X2);
                return (angle1 - angle2) * 180 / Math.PI;
            }
            else { return 0.0; }
        }

    }
}
