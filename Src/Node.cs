using System.Collections.Generic;
namespace Flexbox
{
    public partial class Node
    {
        internal Style nodeStyle = new Style();
        readonly internal Flex.Layout nodeLayout = new Flex.Layout();
        internal int lineIndex;

        internal Node Parent = null;
        internal readonly List<Node> Children = new List<Node>();

        public int ChildrenCount
        {
            get
            {
                return Children.Count;
            }
        }
        internal Node NextChild;

        internal MeasureFunc measureFunc;
        internal BaselineFunc baselineFunc;
        internal PrintFunc printFunc;
        internal Config config = Constant.configDefaults;

        public bool IsDirty
        {
            get;
            internal set;
        }
        internal bool hasNewLayout = true;
        internal NodeType NodeType = NodeType.Default;

        internal readonly Value[] resolvedDimensions = new Value[2] { Flex.ValueUndefined, Flex.ValueUndefined };
        public object Context;

        public Node()
        {

        }

        public Node(Style style)
        {
            nodeStyle = style;
        }


        public void CalculateLayout(float parentWidth, float parentHeight, Direction parentDirection)
        {
            Flex.CalculateLayout(this, parentWidth, parentHeight, parentDirection);
        }
        public void MarkAsDirty()
        {
            Flex.nodeMarkDirtyInternal(this);
        }


        #region Layout

        public int LayoutGetScreenX()
        {
            int x = (int)this.nodeLayout.Position[(int)Edge.Left];
            if (this.nodeStyle.PositionType == PositionType.Relative && this.Parent != null)
                x += this.Parent.LayoutGetScreenX();
            return x;
        }

        public int LayoutGetScreenY()
        {
            int y = (int)this.nodeLayout.Position[(int)Edge.Top];
            if (this.nodeStyle.PositionType == PositionType.Relative && this.Parent != null)
                y += this.Parent.LayoutGetScreenY();
            return y;
        }
        // LayoutGetLeft gets left
        public float LayoutGetLeft()
        {
            return this.nodeLayout.Position[(int)Edge.Left];
        }

        // LayoutGetTop gets top
        public float LayoutGetTop()
        {

            return this.nodeLayout.Position[(int)Edge.Top];
        }

        // LayoutGetRight gets right
        public float LayoutGetRight()
        {
            return this.nodeLayout.Position[(int)Edge.Right];
        }

        // LayoutGetBottom gets bottom
        public float LayoutGetBottom()
        {
            return this.nodeLayout.Position[(int)Edge.Bottom];
        }

        // LayoutGetWidth gets width
        public float LayoutGetWidth()
        {
            return this.nodeLayout.Dimensions[(int)Dimension.Width];
        }

        // LayoutGetHeight gets height
        public float LayoutGetHeight()
        {
            return this.nodeLayout.Dimensions[(int)Dimension.Height];
        }

        // LayoutGetMargin gets margin
        public float LayoutGetMargin(Edge edge)
        {
            Flex.assertWithNode(this, edge < Edge.End, "Cannot get layout properties of multi-edge shorthands");
            if (edge == Edge.Left)
            {
                if (this.nodeLayout.Direction == Direction.RTL)
                {
                    return this.nodeLayout.Margin[(int)Edge.End];
                }
                return this.nodeLayout.Margin[(int)Edge.Start];
            }
            if (edge == Edge.Right)
            {
                if (this.nodeLayout.Direction == Direction.RTL)
                {
                    return this.nodeLayout.Margin[(int)Edge.Start];
                }
                return this.nodeLayout.Margin[(int)Edge.End];
            }
            return this.nodeLayout.Margin[(int)edge];
        }

        // LayoutGetBorder gets border
        public float LayoutGetBorder(Edge edge)
        {
            Flex.assertWithNode(this, edge < Edge.End,
                "Cannot get layout properties of multi-edge shorthands");
            if (edge == Edge.Left)
            {
                if (this.nodeLayout.Direction == Direction.RTL)
                {
                    return this.nodeLayout.Border[(int)Edge.End];
                }
                return this.nodeLayout.Border[(int)Edge.Start];
            }
            if (edge == Edge.Right)
            {
                if (this.nodeLayout.Direction == Direction.RTL)
                {
                    return this.nodeLayout.Border[(int)Edge.Start];
                }
                return this.nodeLayout.Border[(int)Edge.End];
            }
            return this.nodeLayout.Border[(int)edge];
        }

        // LayoutGetPadding gets padding
        public float LayoutGetPadding(Edge edge)
        {
            Flex.assertWithNode(this, edge < Edge.End,
                "Cannot get layout properties of multi-edge shorthands");
            if (edge == Edge.Left)
            {
                if (this.nodeLayout.Direction == Direction.RTL)
                {
                    return this.nodeLayout.Padding[(int)Edge.End];
                }
                return this.nodeLayout.Padding[(int)Edge.Start];
            }
            if (edge == Edge.Right)
            {
                if (this.nodeLayout.Direction == Direction.RTL)
                {
                    return this.nodeLayout.Padding[(int)Edge.Start];
                }
                return this.nodeLayout.Padding[(int)Edge.End];
            }
            return this.nodeLayout.Padding[(int)edge];
        }

        public Direction LayoutGetDirection()
        {
            return this.nodeLayout.Direction;
        }

        public bool LayoutGetHadOverflow()
        {
            return this.nodeLayout.HadOverflow;
        }

        #endregion

        #region other props

        public void SetMeasureFunc(MeasureFunc measureFunc)
        {
            Flex.SetMeasureFunc(this, measureFunc);
        }

        public MeasureFunc GetMeasureFunc()
        {
            return this.measureFunc;
        }

        public void SetBaselineFunc(BaselineFunc baselineFunc)
        {
            this.baselineFunc = baselineFunc;
        }

        public BaselineFunc GetBaselineFunc()
        {
            return this.baselineFunc;
        }

        public void SetPrintFunc(PrintFunc printFunc)
        {
            this.printFunc = printFunc;
        }

        public PrintFunc GetPrintFunc()
        {
            return this.printFunc;
        }
        #endregion

        #region tree
        public Node GetChild(int idx)
        {
            return Flex.GetChild(this, idx);
        }
        public void AddChild(Node child)
        {
            Flex.InsertChild(this, child, ChildrenCount);
        }
        public void InsertChild(Node child, int idx)
        {
            Flex.InsertChild(this, child, idx);
        }
        public void RemoveChild(Node child)
        {
            Flex.RemoveChild(this, child);
        }
        #endregion


    }



}
