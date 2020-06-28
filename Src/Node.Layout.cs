namespace Flexbox
{
    public partial class Node
    {
        public struct Layout8D
        {
            public int left, right, top, bottom;

            //Margin/Border/Padding Edge
            public int absoluteLeft, absoluteTop, width, height;

            public Layout8D(int left, int right, int top, int bottom, int childAbsoluteLeft, int childAbsoluteTop, int childWidth, int childHeight)
            {
                this.left = left;
                this.right = right;
                this.top = top;
                this.bottom = bottom;
                width = childWidth + (left + right);
                height = childHeight + (top + bottom);
                absoluteLeft = childAbsoluteLeft - left;
                absoluteTop = childAbsoluteTop - top;
            }
        }
        public struct Layout
        {
            public int left, right, top, bottom;

            //Content Edge
            public int absoluteLeft, absoluteTop, width, height;

            public Layout8D border;
            public Layout8D margin;
            public Layout8D padding;

            public bool hadOverflow;
            public Direction direction;

            public Layout(Node node)
            {
                absoluteLeft = (int)node.LayoutGetAbsoluteLeft();
                absoluteTop = (int)node.LayoutGetAbsoluteTop();
                left = (int)node.LayoutGetLeft();
                right = (int)node.LayoutGetRight();
                top = (int)node.LayoutGetTop();
                bottom = (int)node.LayoutGetBottom();
                width = (int)node.LayoutGetWidth();
                height = (int)node.LayoutGetHeight();

                padding = new Layout8D((int)node.LayoutGetPadding(Edge.Left), (int)node.LayoutGetPadding(Edge.Right), (int)node.LayoutGetPadding(Edge.Top), (int)node.LayoutGetPadding(Edge.Bottom),
                    absoluteLeft, absoluteTop, width, height);
                border = new Layout8D((int)node.LayoutGetBorder(Edge.Left), (int)node.LayoutGetBorder(Edge.Right), (int)node.LayoutGetBorder(Edge.Top), (int)node.LayoutGetBorder(Edge.Bottom),
                    padding.absoluteLeft, padding.absoluteTop, padding.width, padding.height);
                margin = new Layout8D((int)node.LayoutGetMargin(Edge.Left), (int)node.LayoutGetMargin(Edge.Right), (int)node.LayoutGetMargin(Edge.Top), (int)node.LayoutGetMargin(Edge.Bottom),
                    border.absoluteLeft, border.absoluteTop, border.width, border.height);

                hadOverflow = node.LayoutGetHadOverflow();
                direction = node.LayoutGetDirection();
            }
        }
    }
}
