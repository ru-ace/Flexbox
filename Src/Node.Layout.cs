namespace Flexbox
{
    public partial class Node
    {
        public struct Layout8D
        {
            public int left, right, top, bottom;

            //Margin/Border/Padding Edge
            public int absoluteLeft, absoluteTop, width, height;

            internal Layout8D(int left, int right, int top, int bottom)
            {
                this.left = left;
                this.right = right;
                this.top = top;
                this.bottom = bottom;

                this.absoluteLeft = 0;
                this.absoluteTop = 0;
                this.width = 0;
                this.height = 0;

            }
            internal Layout8D(int left, int right, int top, int bottom, int absoluteLeft, int absoluteTop, int width, int height)
            {
                this.left = left;
                this.right = right;
                this.top = top;
                this.bottom = bottom;

                this.absoluteLeft = absoluteLeft;
                this.absoluteTop = absoluteTop;
                this.width = width;
                this.height = height;
            }

            internal void SetInnerEdge(Layout8D layout)
            {
                this.absoluteLeft = layout.absoluteLeft + layout.left;
                this.absoluteTop = layout.absoluteTop + layout.top;
                this.width = layout.width - (layout.left + layout.right);
                this.height = layout.height - (layout.top + layout.bottom);
            }

            internal void SetOuterEdge(Layout8D layout)
            {
                this.absoluteLeft = layout.absoluteLeft - left;
                this.absoluteTop = layout.absoluteTop - top;
                this.width = layout.width + (left + right);
                this.height = layout.height + (top + bottom);
            }

            public override string ToString()
            {
                return string.Format("(x:{0} y:{1} w:{2} h:{3}) (t:{4} r:{5} b:{6} l:{7})", absoluteLeft, absoluteTop, width, height, top, right, bottom, left);
            }
        }
        public struct Layout
        {
            public int left, right, top, bottom;
            //Content Edge
            public int absoluteLeft, absoluteTop, width, height;

            public Layout8D margin;
            public Layout8D border;
            public Layout8D padding;
            public Layout8D content;

            public bool hadOverflow;
            public Direction direction;

            internal Layout(Node node)
            {

                absoluteLeft = (int)node.LayoutGetAbsoluteLeft();
                absoluteTop = (int)node.LayoutGetAbsoluteTop();
                left = (int)node.LayoutGetLeft();
                right = (int)node.LayoutGetRight();
                top = (int)node.LayoutGetTop();
                bottom = (int)node.LayoutGetBottom();
                width = (int)node.LayoutGetWidth();
                height = (int)node.LayoutGetHeight();

                // https://yogalayout.com/docs/margins-paddings-borders
                // Padding in Yoga acts as if box-sizing: border-box; was set
                // Border in Yoga acts exactly like padding 
                border = new Layout8D((int)node.LayoutGetBorder(Edge.Left), (int)node.LayoutGetBorder(Edge.Right), (int)node.LayoutGetBorder(Edge.Top), (int)node.LayoutGetBorder(Edge.Bottom),
                    absoluteLeft, absoluteTop, width, height);
                padding = new Layout8D((int)node.LayoutGetPadding(Edge.Left), (int)node.LayoutGetPadding(Edge.Right), (int)node.LayoutGetPadding(Edge.Top), (int)node.LayoutGetPadding(Edge.Bottom));
                padding.SetInnerEdge(border);
                content = new Layout8D(0, 0, 0, 0);
                content.SetInnerEdge(padding);

                margin = new Layout8D((int)node.LayoutGetMargin(Edge.Left), (int)node.LayoutGetMargin(Edge.Right), (int)node.LayoutGetMargin(Edge.Top), (int)node.LayoutGetMargin(Edge.Bottom));
                margin.SetOuterEdge(border);


                hadOverflow = node.LayoutGetHadOverflow();
                direction = node.LayoutGetDirection();
            }

            public string ToStr() { return ToStr(0); }
            public string ToStr(int indent)
            {
                string line = "{\n";
                indent++;
                string tab = new System.String(' ', indent * 2);
                line += tab + "box = " + string.Format("(x:{0} y:{1} w:{2} h:{3}) (l:{4} t:{5} r:{6} b:{7})", absoluteLeft, absoluteTop, width, height, left, top, right, bottom) + "\n";
                line += tab + "margin = " + margin.ToString() + "\n";
                line += tab + "border = " + border.ToString() + "\n";
                line += tab + "padding = " + padding.ToString() + "\n";
                line += tab + "content = " + content.ToString() + "\n";
                indent--;
                line += new System.String(' ', indent * 2) + "}";
                return line;
            }

        }
    }
}
