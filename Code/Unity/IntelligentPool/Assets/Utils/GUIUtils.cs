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
//using UnityEditor;
namespace AaltoGames{
	public class GUIUtils{
		public static Material lineMaterial=null;
		//static GUIStyle style=null;
		static Texture2D texture;
		public static void drawRectangle(Vector2 minCorner, Vector2 maxCorner, Color color, bool filled)
		{
			if (filled)
			{
				init();
				Color oldColor=GUI.color;
				GUI.color=color;
				GUI.DrawTexture(new Rect(minCorner.x,minCorner.y,maxCorner.x-minCorner.x,maxCorner.y-minCorner.y),texture,ScaleMode.StretchToFill);
				GUI.color=oldColor;
				
				/*
				initMaterial();
				lineMaterial.SetPass(0);
				GL.PushMatrix();
				GL.LoadOrtho();
				GL.Color(color);
				GL.Begin(GL.QUADS);
				GL.Vertex(new Vector2(minCorner.x,maxCorner.y));
				GL.Vertex(maxCorner);
				GL.Vertex(new Vector2(maxCorner.x,minCorner.y));
				GL.Vertex(minCorner);
				GL.End();
				GL.PopMatrix();
				*/
			}
			else{
				init ();
				lineMaterial.SetPass(0);
				GL.PushMatrix();
				GL.LoadOrtho();
				GL.Color(color);
				GL.Begin(GL.QUADS);
				GL.Vertex(new Vector2(minCorner.x,maxCorner.y));
				GL.Vertex(maxCorner);
				GL.Vertex(new Vector2(maxCorner.x,minCorner.y));
				GL.Vertex(minCorner);
				GL.End();
				GL.PopMatrix();
				/*
				Handles.color=color;
				Handles.DrawLine(minCorner,new Vector2(maxCorner.x,minCorner.y));
				Handles.DrawLine(new Vector2(maxCorner.x,minCorner.y),maxCorner);
				Handles.DrawLine(maxCorner,new Vector2(minCorner.x,maxCorner.y));
				Handles.DrawLine(new Vector2(minCorner.x,maxCorner.y),minCorner);
				*/
			}
		}
		public static void drawLine(Vector2 pt1, Vector2 pt2, Color color)
		{
			init();
			lineMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			GL.Color(color);
			GL.Begin(GL.LINES);
			GL.Vertex(pt1);
			GL.Vertex(pt2);
			GL.End();
			GL.PopMatrix();
			/*
			Handles.color=color;
			Handles.DrawLine(pt1,pt2);
			*/
		}
		public static void draw3dCrosshair(Vector3 pos, Color color, float size)
		{
			init ();
			lineMaterial.SetPass(0);
			GL.Color (color);
			GL.Begin(GL.LINES);
			GL.Vertex3(pos.x-size,pos.y,pos.z);
			GL.Vertex3(pos.x+size,pos.y,pos.z);
			GL.Vertex3(pos.x,pos.y-size,pos.z);
			GL.Vertex3(pos.x,pos.y+size,pos.z);
			GL.Vertex3(pos.x,pos.y,pos.z-size);
			GL.Vertex3(pos.x,pos.y,pos.z+size);
			GL.End ();
		}
		public static void init()
		{	
			if (lineMaterial!=null)
				return;
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
		        "SubShader { Pass {" +
		        "   BindChannels { Bind \"Color\",color }" +
		        "   Blend SrcAlpha OneMinusSrcAlpha" +
		        "   ZWrite Off Cull Off Fog { Mode Off }" +
		        "} } }");
		        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
		    	lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
			texture=new Texture2D(1,1);
			texture.SetPixel(0,0,Color.white);
			texture.Apply();
			texture.wrapMode=TextureWrapMode.Repeat;
			
			
		}
 
	}
} //namespace AaltoGames
