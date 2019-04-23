/*

Aalto Game Tools license

Copyright (C) 2012 Perttu Hämäläinen

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using UnityEngine;
using System.Collections;
namespace AaltoGames{
		
	public class GeometryUtils{
	    public static float fromToAngle2dShortest(Vector2 from, Vector2 to)
	    {
	        float absAngle = Vector2.Angle(from, to);
	        Vector2 fromNormal = new Vector2(-from.y, from.x);
	        float angle = absAngle * Mathf.Sign(Vector2.Dot(to, fromNormal));
	        return angle;
	    }
		public static float wrapAngleToClosest(float angle, float target)
		{
			while (Mathf.Abs(angle-360.0f-target) < Mathf.Abs(angle-target))
				angle-=360.0f;
			while (Mathf.Abs(angle+360.0f-target) < Mathf.Abs(angle-target))
				angle+=360.0f;
			return angle; 
		}
		public static bool pointInRectangle2d(Vector2 point, Vector2 minCorner, Vector2 maxCorner){
			if (point.x >= minCorner.x && point.x<=maxCorner.x && point.y >=minCorner.y && point.y < maxCorner.y)
				return true;
			return false;
		}
	    //lineDirection must be normalized
	    public static float pointDistanceFromLine(Vector3 pointOnLine, Vector3 lineDirection, Vector3 point)
	    {
	        Vector3 pointToPoint = point - pointOnLine;
	        Vector3 projected = pointOnLine + Vector3.Project(pointToPoint, lineDirection);
	        return Vector3.Distance(point, projected);
	    }
        public static Vector3 closestPointOnLineSegment(Vector3 pt, Vector3 segmentPoint1, Vector3 segmentPoint2)
        {
            Vector3 v=(segmentPoint2-segmentPoint1);
            Vector3 dir=v.normalized;
            float projected = Vector3.Dot(pt-segmentPoint1, dir);
            if (projected > 0 && projected < v.magnitude)
                return segmentPoint1 + projected * dir;
            else if (projected <= 0)
                return segmentPoint1;
            return segmentPoint2;
        }
        public static Vector3 closestPointOnLineSegmentWithBuffer(Vector3 pt, Vector3 segmentPoint1, Vector3 segmentPoint2, float bufferZone)
        {
            Vector3 v = segmentPoint2 - segmentPoint1;
            Vector3 dir = v.normalized;
            Vector3 middle = 0.5f * (segmentPoint1 + segmentPoint2);
            float middleProjected = Vector3.Dot(middle - segmentPoint1, dir);
            float bufferAmount= Mathf.Min(middleProjected, bufferZone);
            Vector3 bufferedEnd1 = segmentPoint1 + dir * bufferAmount;
            Vector3 bufferedEnd2 = segmentPoint2 - dir * bufferAmount;
            v = bufferedEnd2 - bufferedEnd1;
            float projected = Vector3.Dot(pt - bufferedEnd1, dir);
            if (projected > 0 && projected < v.magnitude)
                return bufferedEnd1 + projected * dir;
            else if (projected <= 0)
                return bufferedEnd1;
            return bufferedEnd2;
        }
        public static Vector3 clampPointToCylinder(Vector3 cylinderCenter, Vector3 upVector, float height, float radius, Vector3 point)
	    {
	        Vector3 centerToPoint = point - cylinderCenter;
	        Vector3 pointProjectedOnCylinderAxis = cylinderCenter + Vector3.Project(centerToPoint, upVector);
	        //check cylinder height
	        float sqrHalfHeight=height*0.5f;
	        sqrHalfHeight*=sqrHalfHeight;
	        if (Vector3.SqrMagnitude(pointProjectedOnCylinderAxis-cylinderCenter)<sqrHalfHeight){
	            //check cylinder radius
	            Vector3 axisToPoint=point-pointProjectedOnCylinderAxis;
	            if (axisToPoint.sqrMagnitude < radius*radius)
	            {
	                float scale=radius/axisToPoint.magnitude;
	                axisToPoint*=scale;
	                point=pointProjectedOnCylinderAxis+axisToPoint;
	            }
	        }
	        return point;
	    }
	    public static Vector3 clampPointToSphereSurface(Vector3 center, float radius, Vector3 point)
	    {
	        Vector3 centerToPoint = point - center;
	        if (centerToPoint.sqrMagnitude < radius * radius)
	        {
	            float scale = radius / centerToPoint.magnitude;
	            centerToPoint *= scale;
	            point = center + centerToPoint;
	        }
	        return point;
	    }
	
	    //height includes capsule radius
	    public static Vector3 clampPointToCapsuleSurface(Vector3 center, Vector3 upVector, float height, float radius, Vector3 point)
	    {
	        //first check end spheres
	        float cylinderHalfHeight=0.5f*(height-radius*2.0f);
	        cylinderHalfHeight = Mathf.Max(0, cylinderHalfHeight);
	        point=  clampPointToSphereSurface(center + upVector * cylinderHalfHeight, radius, point);
	        point = clampPointToSphereSurface(center - upVector * cylinderHalfHeight, radius, point);
	        //check cylinder
	        point = clampPointToCylinder(center, upVector, cylinderHalfHeight * 2.0f, radius, point);
	        return point;
	    }
	}
	
}//aaltogames