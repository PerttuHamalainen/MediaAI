/*

Aalto Game Tools license

Copyright (C) 2012 Perttu Hämäläinen

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AaltoGames{ 
	public class GraphUtils{
		 
		static Material lineMaterialZTestOff=null,lineMaterialZTestOn=null;
        public static void setMaterials(Material _lineMaterialZTestOff,Material _lineMaterialZTestOn)
        {
            lineMaterialZTestOff = _lineMaterialZTestOff;
            lineMaterialZTestOn = _lineMaterialZTestOn;
        }
		public static void setLineMaterialPass(int pass, bool zTest)
		{
			if (lineMaterialZTestOff==null)
			{
            //    lineMaterialZTestOff = new Material("Shader \"Lines/Colored Blended\" {" + "SubShader { Pass { " + "    Blend SrcAlpha OneMinusSrcAlpha " + "    ZWrite Off Cull Off Fog { Mode Off } " + "    BindChannels {" + "      Bind \"vertex\", vertex Bind \"color\", color }" + "} } }");

                //lineMaterialZTestOff = new Material(Shader.Find("Custom/GizmoShader"));

               lineMaterialZTestOff = new Material( "Shader \"Lines/Colored Blended\" {" +
            "SubShader { Pass {" +
            "   BindChannels { Bind \"Color\",color }" +
            "   Blend SrcAlpha OneMinusSrcAlpha" +
            "   ZTest Off ZWrite Off Cull Off Fog { Mode Off }" +
            "} } }");
                lineMaterialZTestOff.hideFlags = HideFlags.HideAndDontSave;
                lineMaterialZTestOff.shader.hideFlags = HideFlags.HideAndDontSave;
            }
			if (lineMaterialZTestOn==null)
			{
                //lineMaterialZTestOn = new Material(Shader.Find("Custom/GizmoShader"));

                lineMaterialZTestOn = new Material("Shader \"Lines/Colored Blended\" {" + "SubShader { Pass { " + "    Blend SrcAlpha OneMinusSrcAlpha " + "    ZWrite Off Cull Off Fog { Mode Off } " + "    BindChannels {" + "      Bind \"vertex\", vertex Bind \"color\", color }" + "} } }");
                lineMaterialZTestOn.hideFlags = HideFlags.HideAndDontSave;
                lineMaterialZTestOn.shader.hideFlags = HideFlags.HideAndDontSave;
            //    lineMaterialZTestOn = new Material("Shader \"Lines/Colored Blended\" {" +
            //"SubShader { Pass {" +
            //"   BindChannels { Bind \"Color\",color }" +
            //"   Blend SrcAlpha OneMinusSrcAlpha" +
            //"   ZWrite Off Cull Off Fog { Mode Off }" +
            //"} } }");
			}
			if (zTest)
			{
				lineMaterialZTestOn.SetPass(pass);
			}
			else{
				lineMaterialZTestOff.SetPass(pass);
			}
		}
		public static 	void drawArrow2d(Vector2 start, Vector2 end, float headSize)
		{
		    GL.Begin(GL.LINES);
			GL.Vertex(start);
			GL.Vertex(end);
		    GL.End();
			Vector2 arrowDir=(end-start).normalized;
			Vector2 arrowNormal=new Vector2(-arrowDir.y,arrowDir.x);
			GL.Begin(GL.TRIANGLES);
			GL.Vertex(end);
			GL.Vertex(end-arrowDir*headSize+arrowNormal*0.5f*headSize);
			GL.Vertex(end-arrowDir*headSize-arrowNormal*0.5f*headSize);
			GL.End();
		}
		public static void drawCapsuleCap(Vector3 origin,float radius,Vector3 up,Vector3 right)
		{
			//draw circle
			GL.Begin(GL.LINES);
			for (float angle=0; angle<=2.0f*Mathf.PI+0.01f; angle+=0.5f)
			{
				Vector3 axisToCylinder=right*radius;
				axisToCylinder=Quaternion.AngleAxis(Mathf.Rad2Deg*angle,up)*axisToCylinder;
				GL.Vertex(origin+axisToCylinder);
				//we are drawing a line strip, but Unity GL doesn't support it directly. 
				//Thus, for others than the first point, we duplicate the vertex
				if (angle!=0)
					GL.Vertex(origin+axisToCylinder);
			}
			GL.End();
			
			//draw cap "stripes"
			for (float angle=0; angle<2.0f*Mathf.PI; angle+=1.0f)
			{
				Vector3 axisToCylinder=right*radius;
				axisToCylinder=Quaternion.AngleAxis(Mathf.Rad2Deg*angle,up)*axisToCylinder;
				
				GL.Begin(GL.LINES);
				for (float t=0; t<=1.01f; t+=0.1f)
				{
					Vector3 v=Vector3.Slerp(axisToCylinder,up*radius,Mathf.Clamp01(t));
					GL.Vertex(origin+v);
					//we are drawing a line strip, but Unity GL doesn't support it directly. 
					//Thus, for others than the first point, we duplicate the vertex
					if (t!=0)
						GL.Vertex(origin+v);
				}
				GL.End();
			}
		}
		public static void drawCapsule(CapsuleCollider cc)
		{
			float cylinderHeight=Mathf.Max(0,cc.height-2.0f*cc.radius);
			//draw caps
			drawCapsuleCap(cc.center+cc.transform.position+cylinderHeight*0.5f*cc.transform.up,cc.radius,cc.transform.up,cc.transform.right);
			drawCapsuleCap(cc.center+cc.transform.position-cylinderHeight*0.5f*cc.transform.up,cc.radius,-cc.transform.up,cc.transform.right);
			//draw cylinder lines
			GL.Begin(GL.LINES);
			for (float angle=0; angle<2.0f*Mathf.PI; angle+=1.0f)
			{
				Vector3 axisEnd=cc.center+cc.transform.position+cylinderHeight*0.5f*cc.transform.up;
				Vector3 axisToCylinder=cc.transform.right*cc.radius;
				axisToCylinder=Quaternion.AngleAxis(Mathf.Rad2Deg*angle,cc.transform.up) * axisToCylinder;
				GL.Vertex(axisEnd+axisToCylinder);
				GL.Vertex(axisEnd+axisToCylinder-cc.transform.up*cylinderHeight);
			}
			GL.End();
		}
		public static void drawCrosshair(Vector3 center, float size)
		{
			size*=0.5f;
			GL.Begin(GL.LINES);
			GL.Vertex(center-size*Vector3.right);
			GL.Vertex(center+size*Vector3.right);

			GL.Vertex(center-size*Vector3.up);
			GL.Vertex(center+size*Vector3.up);

			GL.Vertex(center-size*Vector3.forward);
			GL.Vertex(center+size*Vector3.forward);
			GL.End();
		}
        public static void addCrosshair(Vector3 center, float size,Color color)
        {
            size *= 0.5f;
            AddLine(center - size * Vector3.right,center + size * Vector3.right,color);
            AddLine(center - size * Vector3.up,center + size * Vector3.up,color);
            AddLine(center - size * Vector3.forward,center + size * Vector3.forward,color);
        }
        public static void drawCube(Vector3 center, float size, Vector3 up, Vector3 right, Vector3 forward)
		{
			size*=0.5f;
			GL.Begin(GL.LINES);
			//front quad
			GL.Vertex(center+size*(-right+forward-up));
			GL.Vertex(center+size*(-right+forward+up));

			GL.Vertex(center+size*(-right+forward+up));
			GL.Vertex(center+size*(right+forward+up));

			GL.Vertex(center+size*(right+forward+up));
			GL.Vertex(center+size*(right+forward-up));

			GL.Vertex(center+size*(right+forward-up));
			GL.Vertex(center+size*(-right+forward-up));
			
			//back quad
			GL.Vertex(center+size*(-right-forward-up));
			GL.Vertex(center+size*(-right-forward+up));

			GL.Vertex(center+size*(-right-forward+up));
			GL.Vertex(center+size*(right-forward+up));

			GL.Vertex(center+size*(right-forward+up));
			GL.Vertex(center+size*(right-forward-up));

			GL.Vertex(center+size*(right-forward-up));
			GL.Vertex(center+size*(-right-forward-up));
			
			//lines connecting the quads
			GL.Vertex(center+size*(-right+forward-up));
			GL.Vertex(center+size*(-right-forward-up));

			GL.Vertex(center+size*(-right+forward+up));
			GL.Vertex(center+size*(-right-forward+up));

			GL.Vertex(center+size*(right+forward+up));
			GL.Vertex(center+size*(right-forward+up));

			GL.Vertex(center+size*(right+forward-up));
			GL.Vertex(center+size*(right-forward-up));
			GL.End();
		
		}
        public class LineData
        {
            public Vector3 pt1;
            public Vector3 pt2;
            public Color color;
            public LineData(Vector3 pt1, Vector3 pt2, Color color)
            {
                this.pt1 = pt1;
                this.pt2 = pt2;
                this.color = color;
            }
        };
        public static List<LineData> pendingLines = new List<LineData>();
        //Note: you need to manually call GL.Begin() and setLineMaterialPass() for this to work
        public static void AddLine(Vector3 pt1, Vector3 pt2, Color color)
        {
            pendingLines.Add(new LineData(pt1, pt2, color));
        }
        public static void DrawPendingLines(bool zTest=true, bool clear=true)
        {
            setLineMaterialPass(0, zTest);
            GL.Begin(GL.LINES);
            foreach (LineData ld in pendingLines)
            {
                GL.Color(ld.color);
                GL.Vertex3(ld.pt1.x, ld.pt1.y, ld.pt1.z);
                GL.Vertex3(ld.pt2.x, ld.pt2.y, ld.pt2.z);           

            }
            GL.End();
            if (clear)
                pendingLines.Clear();
        }

    }  //GraphUtils
	public class RunningGraph  {
		List<float> buffer=new List<float>();
		int writeIdx=0;
		Material lineMaterial;
		int bufferSize;

		public RunningGraph (int bufferSize)
		{
			this.bufferSize=bufferSize;
	 		lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
	        "SubShader { Pass {" +
	        "   BindChannels { Bind \"Color\",color }" +
	        "   Blend SrcAlpha OneMinusSrcAlpha" +
	        "   ZWrite Off Cull Off Fog { Mode Off }" +
	        "} } }");
	        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
	    	lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
			clear ();
		}
		public void addPoint(float val)
		{
			buffer[writeIdx]=val;
			writeIdx++;
			if (writeIdx>=buffer.Capacity)
				writeIdx=0;
		}
		//call this from OnRenderObject of some other object
		public void renderRunning(Vector2 graphOrigin, float graphWidth, float graphHeight, float minValue, float maxValue)
		{
			//Render the floor display
		    lineMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			float graphXStep=0;
			float graphYStep=graphHeight;
			if (buffer.Capacity>0)
				graphXStep=graphWidth/buffer.Capacity;
			GraphUtils.drawArrow2d(graphOrigin,new Vector2(graphOrigin.x,graphOrigin.y+graphHeight),0.02f);
			GraphUtils.drawArrow2d(graphOrigin,new Vector2(graphOrigin.x+graphWidth,graphOrigin.y),0.02f);
		    GL.Begin(GL.LINES);
			Vector2 previous=new Vector2();
			for (int i=0; i<buffer.Capacity; i++)
			{
				int delay=buffer.Capacity-i;
				int idx=writeIdx-delay;
				if (idx<0)
					idx+=buffer.Capacity;
				float val=buffer[idx];
				val=val-minValue;
				val=val/(maxValue-minValue);
				Vector2 current=new Vector2(graphOrigin.x+graphXStep*(float)i,graphOrigin.y+graphYStep*val);
				if (i!=0){
					GL.Vertex(previous);
					GL.Vertex(current);
				}
				previous=current;
			}
			GL.End();
			GL.PopMatrix();
		}
		//returns the screen coordinates for the maximum of the graph
		public Vector2 render(Vector2 graphOrigin, float graphWidth, float graphHeight, float minValue, float maxValue)
		{
			//Render the floor display
		    lineMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			float graphXStep=0;
			float graphYStep=graphHeight;
			if (writeIdx>0)
				graphXStep=graphWidth/writeIdx;
			GraphUtils.drawArrow2d(graphOrigin,new Vector2(graphOrigin.x,graphOrigin.y+graphHeight),0.02f);
			GraphUtils.drawArrow2d(graphOrigin,new Vector2(graphOrigin.x+graphWidth,graphOrigin.y),0.02f);
		    GL.Begin(GL.LINES);
			Vector2 previous=new Vector2();
			Vector2 result=new Vector2(-float.MaxValue,-float.MaxValue);
			for (int i=0; i<writeIdx; i++)
			{
				float val=buffer[i];
				val=val-minValue;
				val=val/(maxValue-minValue);
				Vector2 current=new Vector2(graphOrigin.x+graphXStep*(float)i,graphOrigin.y+graphYStep*val);
				if (current.y > result.y)
					result=current;
				if (i!=0){
					GL.Vertex(previous);
					GL.Vertex(current);
				}
				previous=current;
			}
			GL.End();
			GL.PopMatrix();
			return result;
		}
		public void clear()
		{
			writeIdx=0;
			buffer.Clear();
			buffer.Capacity=bufferSize;
			for (int i=0; i<buffer.Capacity; i++)
				buffer.Add(0);
		}
	}
} //namespace AaltoGames