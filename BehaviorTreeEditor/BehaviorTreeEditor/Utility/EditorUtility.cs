﻿using BehaviorTreeEditor.Properties;
using System.Drawing;

namespace BehaviorTreeEditor
{
    public static class EditorUtility
    {
        static EditorUtility()
        {
            NameStringFormat.LineAlignment = StringAlignment.Center;
            NameStringFormat.Alignment = StringAlignment.Center;
            //框选范围用虚线
            SelectionModePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }

        #region ==================Background===================
        //画布中心点
        public static Vec2 Center = new Vec2(5000f, 5000f);
        //视图缩放最小值
        public static float ZoomScaleMin = 0.5f;
        //视图缩放最大值
        public static float ZoomScaleMax = 2.0f;
        //普通格子线 画笔
        public static Pen LineNormalPen = new Pen(Color.FromArgb(172, 172, 172), 1);
        //粗格子线 画笔
        public static Pen LineBoldPen = new Pen(Color.FromArgb(172, 172, 172), 2);

        #endregion

        #region ==================Transition===================

        //节点普通连线 画笔
        public static Pen TransitionNormalPen = new Pen(Color.Blue, 2);
        //节点普通连线 画笔
        public static Pen TransitionSelectedPen = new Pen(Color.Orange, 2);
        //普通箭头 笔刷
        public static Brush ArrowNormalBrush = new SolidBrush(Color.Blue);
        //普通箭头 笔刷
        public static Brush ArrowSelectedBrush = new SolidBrush(Color.Orange);
        //箭头宽度像素
        public static int ArrowWidth = 17;
        //箭头高度度像素
        public static int ArrowHeight = 10;

        #endregion

        #region  =================节点=====================
        //节点外框 画笔
        public static Pen NodeNormalPen = new Pen(Color.White, 2);
        //节点选中 画笔
        public static Pen NodeSelectedPen = new Pen(Color.Orange, 4);
        //框选笔刷
        public static Brush SelectionModeBrush = new SolidBrush(Color.FromArgb(50, Color.Green));
        //框选范围 画笔
        public static Pen SelectionModePen = new Pen(Color.Green, 1.5f);

        //节点字体
        public static Font NodeFont = new Font("宋体", 15, FontStyle.Regular);
        public static Brush NodeBrush = new SolidBrush(Color.White);
        //标题节点高
        public static int TitleNodeHeight = 30;
        //节点最小宽度
        public static int NodeWidth = 150;
        //节点最小高度
        public static int NodeHeight = 60;
        //普通状态图片
        public static Brush NodeTitleBrush = new SolidBrush(Color.FromArgb(255, 54, 74, 85));
        public static Brush NodeContentBrush = new TextureBrush(Resources.NodeBackground_Light);//普通状态图片
        public static StringFormat NameStringFormat = new StringFormat(StringFormatFlags.NoWrap);

        #endregion

        //节点标题Rect
        public static Rect GetTitleRect(NodeDesigner node, Vec2 offset)
        {
            return new Rect(node.Rect.x - offset.x, node.Rect.y - offset.y, node.Rect.width, EditorUtility.TitleNodeHeight);
        }

        //节点内存Rect
        public static Rect GetContentRect(NodeDesigner node, Vec2 offset)
        {
            return new Rect(node.Rect.x - offset.x, node.Rect.y + EditorUtility.TitleNodeHeight - offset.y, node.Rect.width, node.Rect.height - EditorUtility.TitleNodeHeight);
        }

        //左边连接点
        public static Vec2 GetLeftLinkPoint(NodeDesigner node, Vec2 offset)
        {
            return new Vec2(node.Rect.x - offset.x, node.Rect.y + EditorUtility.TitleNodeHeight / 2.0f - offset.y);
        }

        //右边连接点
        public static Vec2 GetRightLinkPoint(NodeDesigner node, Vec2 offset)
        {
            return new Vec2(node.Rect.x + node.Rect.width - offset.x, node.Rect.y + EditorUtility.TitleNodeHeight / 2.0f - offset.y);
        }

        /// <summary>
        /// 画格子线
        /// </summary>
        /// <param name="graphics">graphics</param>
        /// <param name="rect">画线总区域</param>
        /// <param name="gridSize">间距</param>
        /// <param name="offset">偏移</param>
        public static void DrawGridLines(Graphics graphics, Rect rect, int gridSize, Vec2 offset, bool normal)
        {
            Pen pen = normal ? EditorUtility.LineNormalPen : EditorUtility.LineBoldPen;
            for (float i = rect.x + (offset.x < 0 ? gridSize : 0) + offset.x % gridSize; i < rect.x + rect.width; i = i + gridSize)
            {
                DrawLine(graphics, pen, new Vec2(i, rect.y), new Vec2(i, rect.y + rect.height));
            }
            for (float j = rect.y + (offset.y < 0 ? gridSize : 0) + offset.y % gridSize; j < rect.y + rect.height; j = j + gridSize)
            {
                DrawLine(graphics, pen, new Vec2(rect.x, j), new Vec2(rect.x + rect.width, j));
            }
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="graphics">graphics</param>
        /// <param name="pen">画笔</param>
        /// <param name="p1">起始坐标</param>
        /// <param name="p2">结束坐标</param>
        public static void DrawLine(Graphics graphics, Pen pen, Vec2 p1, Vec2 p2)
        {
            graphics.DrawLine(pen, p1, p2);
        }

        /// <summary>
        /// 绘制节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="graphics">graphics</param>
        /// <param name="offset">偏移</param>
        /// <param name="on">是否选中</param>
        public static void Draw(NodeDesigner node, Graphics graphics, Vec2 offset, bool on)
        {
            Rect titleRect = GetTitleRect(node, offset);
            Rect contentRect = GetContentRect(node, offset);

            //画标题底框
            //graphics.DrawImage(Resources.NodeBackground_Dark, titleRect);
            graphics.FillRectangle(EditorUtility.NodeTitleBrush, titleRect);
            //标题
            graphics.DrawString(node.ClassType, EditorUtility.NodeFont, EditorUtility.NodeBrush, titleRect.x + titleRect.width / 2, titleRect.y + titleRect.height / 2 + 1, EditorUtility.NameStringFormat);
            //画内容底框
            graphics.FillRectangle(EditorUtility.NodeContentBrush, contentRect);

            //graphics.DrawRectangle(EditorUtility.NodeNormalPen, node.Rect - offset);

            //选中边框
            if (on)
            {
                graphics.DrawRectangle(EditorUtility.NodeSelectedPen, node.Rect - offset);
            }

            //graphics.DrawString(node.Rect.x + " " + node.Rect.y, EditorUtility.NodeFont, EditorUtility.NodeBrush, titleRect.x + titleRect.width / 2, titleRect.y + titleRect.height / 2 + contentRect.height / 3 + 1, EditorUtility.NameStringFormat);
            //graphics.DrawString(node.Rect.x + " " + node.Rect.y, EditorUtility.NodeFont, EditorUtility.NodeBrush, titleRect.x + titleRect.width / 2, titleRect.y + titleRect.height / 2 + contentRect.height + 1, EditorUtility.NameStringFormat);

        }

        /// <summary>
        /// 获取字段对应的名字
        /// </summary>
        /// <returns></returns>
        public static string GetFieldTypeName(FieldType fieldType)
        {
            string content = string.Empty;
            switch (fieldType)
            {
                case FieldType.IntField:
                    content = "int";
                    break;
                case FieldType.LongField:
                    content = "long";
                    break;
                case FieldType.FloatField:
                    content = "float";
                    break;
                case FieldType.DoubleField:
                    content = "double";
                    break;
                case FieldType.ColorField:
                    content = "color";
                    break;
                case FieldType.Vector2:
                    content = "vector2";
                    break;
                case FieldType.Vector3:
                    content = "vector3";
                    break;
                case FieldType.EnumField:
                    content = "enum";
                    break;
                case FieldType.BooleanField:
                    content = "bool";
                    break;
                case FieldType.RepeatIntField:
                    content = "int[]";
                    break;
                case FieldType.RepeatLongField:
                    content = "long[]";
                    break;
                case FieldType.RepeatFloatField:
                    content = "float[]";
                    break;
                case FieldType.RepeatVector2Field:
                    content = "vector2[]";
                    break;
                case FieldType.RepeatVector3Field:
                    content = "vector3[]";
                    break;
            }
            return content;
        }
    }
}
