namespace Flexbox
{
    public class Style
    {
        internal Direction Direction = Direction.Inherit;
        internal FlexDirection FlexDirection = FlexDirection.Row;
        internal Justify JustifyContent = Justify.FlexStart;
        internal Align AlignContent = Align.Stretch;
        internal Align AlignItems = Align.Stretch;
        internal Align AlignSelf = Align.Auto;
        public PositionType PositionType = PositionType.Relative;
        public Wrap FlexWrap = Wrap.NoWrap;
        public Overflow Overflow = Overflow.Visible;
        public Display Display = Display.Flex;
        internal float FlexGrow = 0f;
        internal float FlexShrink = 1f;
        internal Value FlexBasis = CreateAutoValue();
        internal Value[] Margin = CreateDefaultEdgeValuesUnit();
        internal Value[] Position = CreateDefaultEdgeValuesUnit();
        internal Value[] Padding = CreateDefaultEdgeValuesUnit();
        internal Value[] Border = CreateDefaultEdgeValuesUnit();
        internal Value[] Dimensions = new Value[2] { CreateAutoValue(), CreateAutoValue() };
        internal Value[] MinDimensions = new Value[2] { Value.UndefinedValue, Value.UndefinedValue };
        internal Value[] MaxDimensions = new Value[2] { Value.UndefinedValue, Value.UndefinedValue };
        // Yoga specific properties, not compatible with flexbox specification
        internal float AspectRatio = float.NaN;

        public void SetDefault()
        {
            Direction = Direction.Inherit;
            FlexDirection = FlexDirection.Row;
            JustifyContent = Justify.FlexStart;
            AlignContent = Align.Stretch;
            AlignItems = Align.Stretch;
            AlignSelf = Align.Auto;
            PositionType = PositionType.Relative;
            FlexWrap = Wrap.NoWrap;
            Overflow = Overflow.Visible;
            Display = Display.Flex;
            FlexGrow = 0f;
            FlexShrink = 1f;
            FlexBasis = CreateAutoValue();
            Margin = CreateDefaultEdgeValuesUnit();
            Position = CreateDefaultEdgeValuesUnit();
            Padding = CreateDefaultEdgeValuesUnit();
            Border = CreateDefaultEdgeValuesUnit();
            Dimensions = new Value[2] { CreateAutoValue(), CreateAutoValue() };
            MinDimensions = new Value[2] { Value.UndefinedValue, Value.UndefinedValue };
            MaxDimensions = new Value[2] { Value.UndefinedValue, Value.UndefinedValue };
            // Yoga specific properties, not compatible with flexbox specification
            AspectRatio = float.NaN;
        }


        public static void Copy(Style dest, Style src)
        {
            dest.Direction = src.Direction;
            dest.FlexDirection = src.FlexDirection;
            dest.JustifyContent = src.JustifyContent;
            dest.AlignContent = src.AlignContent;
            dest.AlignItems = src.AlignItems;
            dest.AlignSelf = src.AlignSelf;
            dest.PositionType = src.PositionType;
            dest.FlexWrap = src.FlexWrap;
            dest.Overflow = src.Overflow;
            dest.Display = src.Display;
            dest.FlexGrow = src.FlexGrow;
            dest.FlexShrink = src.FlexShrink;
            dest.FlexBasis = src.FlexBasis.Clone();

            Value.CopyValue(dest.Margin, src.Margin);
            Value.CopyValue(dest.Position, src.Position);
            Value.CopyValue(dest.Padding, src.Padding);
            Value.CopyValue(dest.Border, src.Border);

            Value.CopyValue(dest.Dimensions, src.Dimensions);
            Value.CopyValue(dest.MinDimensions, src.MinDimensions);
            Value.CopyValue(dest.MaxDimensions, src.MaxDimensions);

            dest.AspectRatio = src.AspectRatio;
        }
        internal static Value CreateAutoValue()
        {
            return new Value(float.NaN, Unit.Auto);
        }
        internal static Value[] CreateDefaultEdgeValuesUnit()
        {
            return new Value[Constant.EdgeCount]{
                Value.UndefinedValue,
                Value.UndefinedValue,
                Value.UndefinedValue,
                Value.UndefinedValue,
                Value.UndefinedValue,
                Value.UndefinedValue,
                Value.UndefinedValue,
                Value.UndefinedValue,
                Value.UndefinedValue,
            };
        }

        public virtual Style Clone()
        {
            var clone = new Style();
            Style.Copy(this, clone);
            return clone;
        }
    }
}
