using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class GraphLayout
{
    


    static public void FloatField(GUIContent content, List<float> list, Texture texture,Color color,Rect area,bool toggle)
    {                    
        GUI.Box(area,content);
        if (toggle)
        {
            if (list.Count != 0)
            {
                var minValue = list.Min();
                var maxValue = list.Max();
                var avgValue = list.Average();
                var scale = area.height / maxValue * 0.85f; // 最大値の高さが描画範囲の80%位に

                for (var i = 0; i < list.Count; i++)
                {
                    var w = 1.0f;
                    var h = list[list.Count - (i + 1)] * scale;
                    var x = area.x + area.width - (i + 1) * w;
                    var y = area.y + area.height - h;
                    var rect = new Rect(x, y, w, h);
                    GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true, 0, color, 0, 0);
                }

                // 最大値の補助線
                {
                    var x = area.x;
                    var y = area.y + area.height - maxValue * scale;
                    var w = area.width;
                    var h = 1.0f;
                    var rect = new Rect(x, y, w, h);

                    GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true, 0, Color.white, 0, 0);


                    var label = new GUIContent(Format("{0,3:F1}", maxValue));
                    var contentSize = GUI.skin.label.CalcSize(label);
                    rect = new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y);
                    GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true, 0, Color.black, 0, 0);
                    GUI.Label(rect, label);
                }

                // 平均値の補助線
                {
                    var x = area.x;
                    var y = area.y + area.height - avgValue * scale;
                    var w = area.width;
                    var h = 1.0f;
                    var rect = new Rect(x, y, w, h);
                    GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true, 0, Color.white, 0, 0);

                    var label = new GUIContent(Format("{0,3:F1}", avgValue));
                    var contentSize = GUI.skin.label.CalcSize(label);
                    rect = new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y);
                    GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true, 0, Color.black, 0, 0);
                    GUI.Label(rect, label);
                }

                // 最小値の補助線
                {
                    var x = area.x;
                    var y = area.y + area.height - minValue * scale;
                    var w = area.width;
                    var h = 1.0f;
                    var rect = new Rect(x, y, w, h);

                    GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true, 0, Color.white, 0, 0);


                    var label = new GUIContent(Format("{0,3:F1}", minValue));
                    var contentSize = GUI.skin.label.CalcSize(label);
                    rect = new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y);
                    GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, true, 0, Color.black, 0, 0);
                    GUI.Label(rect, label);
                }
            }
        }
        
    }

    public static string Format(string fmt, params object[] args)
    {
        return String.Format(CultureInfo.InvariantCulture.NumberFormat, fmt, args);

    }

}
