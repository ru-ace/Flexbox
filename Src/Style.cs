using System;
using System.Collections.Generic;
using System.Linq;
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
        internal PositionType PositionType = PositionType.Relative;
        internal Wrap FlexWrap = Wrap.NoWrap;
        internal Overflow Overflow = Overflow.Visible;
        internal Display Display = Display.Flex;
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


        // default values of supported attrs
        protected static readonly Dictionary<string, string> layoutAttributeDefault = new Dictionary<string, string>()
        {
            {"display", "flex"},
            {"overflow", "visible"},
            {"position", "relative"},
            {"align-content", "stretch"},
            {"align-items", "stretch"},
            {"align-self", "auto"},
            {"flex-direction", "row"},
            {"flex-wrap", "no-wrap"},
            {"flex-basis", "auto"},
            {"flex-shrink", "1"},
            {"flex-grow", "0"},
            {"justify-content", "flex-start"},
            {"direction", "inherit"},
            {"left", "auto"},
            {"top", "auto"},
            {"right", "auto"},
            {"bottom", "auto"},
            {"width", "auto"},
            {"height", "auto"},
            {"min-width", "auto"},
            {"min-height", "auto"},
            {"max-width", "auto"},
            {"max-height", "auto"},
            {"margin", "0"},
            {"margin-left", "0"},
            {"margin-right", "0"},
            {"margin-top", "0"},
            {"margin-bottom", "0"},
            {"padding", "0"},
            {"padding-left", "0"},
            {"padding-right", "0"},
            {"padding-top", "0"},
            {"padding-bottom", "0"},
            {"border-width", "0"},
            {"border-left-width", "0"},
            {"border-right-width", "0"},
            {"border-top-width", "0"},
            {"border-bottom-width", "0"},
        };

        // default values for inherit attrs
        protected static readonly Dictionary<string, string> layoutAttributeInherit = new Dictionary<string, string>()
        {
            {"direction", "ltr"}
        };

        protected bool setMode = false;

        protected bool forceLayoutDirty = false;

        public bool layoutDirty
        {
            get
            {
                return forceLayoutDirty || layoutAttributeChanged.Count > 0;
            }
            set
            {
                if (!value)
                {
                    layoutAttributeChanged.Clear();
                    forceLayoutDirty = false;
                }
                else
                    throw new Exception("Flexbox.Style.layoutDirty cannot be set to true");
            }
        }

        // use to store affected attrs
        protected readonly Dictionary<string, string> layoutAttribute = new Dictionary<string, string>();
        // use to store previous values for changed attributes (relative Apply() and this[])
        protected readonly Dictionary<string, string> layoutAttributeChanged = new Dictionary<string, string>();
        // use to store attrs values before Set() was called. Thus attributeChanged represents changed values relative values before Set() was called.
        protected readonly Dictionary<string, string> layoutAttributeWas = new Dictionary<string, string>();
        // use to store attrs values which changed by animation(see ApplyAnimation())
        protected readonly Dictionary<string, string> layoutAttributeAnimated = new Dictionary<string, string>();

        public virtual string this[string attr]
        {
            get
            {
                if (layoutAttributeAnimated.ContainsKey(attr)) return layoutAttributeAnimated[attr];
                if (!layoutAttributeDefault.ContainsKey(attr)) throw new Exception("Try to get unknown layout style attribute [" + attr + "]");
                string value = layoutAttribute.ContainsKey(attr) ? layoutAttribute[attr] : layoutAttributeDefault[attr];
                if (layoutAttributeInherit.ContainsKey(attr) && value == "inherit")
                {
                    //! Dirty-dirty hack (for good solution need to knew style of parent node, placed in TODO)
                    value = layoutAttributeInherit[attr];
                }
                return value;
            }
            set
            {
                if (!layoutAttributeDefault.ContainsKey(attr)) throw new Exception("Try to set unknown layout style attribute [" + attr + "]");
                value = value.Trim();
                // TODO: if attr is margin, padding, border - ignore it in change tracking and expands it to edges(top, left, bottom, right)

                if (setMode)
                {
                    var old_value = layoutAttributeWas.ContainsKey(attr) ? layoutAttributeWas[attr] : layoutAttributeDefault[attr];
                    if (value != old_value)
                        layoutAttributeChanged[attr] = old_value;
                    else if (layoutAttributeChanged.ContainsKey(attr))
                        layoutAttributeChanged.Remove(attr);
                }
                else
                {
                    //if (this[attr] == value) return;
                    layoutAttributeChanged[attr] = this[attr];
                }
                layoutAttribute[attr] = value;
                if (!Flex.ParseStyleAttr(this, attr, value))
                    throw new Exception("Failed to parse attribute [" + attr + ":" + value + "]");

            }
        }
        public virtual void ApplyAnimation(Dictionary<string, string> animated_attrs = null)
        {
            layoutAttributeAnimated.Clear();
            if (animated_attrs != null)
            {
                foreach (var kv in animated_attrs)
                {
                    var attr = kv.Key;
                    if (this[attr] != kv.Value)
                        layoutAttributeChanged[attr] = this[attr];
                    layoutAttributeAnimated[attr] = kv.Value;
                }
            }
            Sync();
        }

        protected virtual void Sync()
        {
            SetDefault(false);
            foreach (var kv in layoutAttribute)
                if (!Flex.ParseStyleAttr(this, kv.Key, kv.Value))
                    throw new Exception("Failed to parse attribute [" + kv.Key + ":" + kv.Value + "]");

            foreach (var kv in layoutAttributeAnimated)
                if (!Flex.ParseStyleAttr(this, kv.Key, kv.Value))
                    throw new Exception("Failed to parse attribute [" + kv.Key + ":" + kv.Value + "]");
        }

        public virtual string[] GetChangedLayoutAttributeNames()
        {
            return layoutAttributeChanged.Keys.ToArray();
        }
        public virtual string GetChangedLayoutAttributePreviousValue(string attr)
        {
            return layoutAttributeChanged[attr];
        }

        public virtual void Set(string style)
        {
            layoutAttributeWas.Clear();
            if (layoutAttribute.Count > 0)
            {
                foreach (var kv in layoutAttribute)
                    layoutAttributeWas.Add(kv.Key, kv.Value);
                setMode = true;
            }
            SetDefault();
            Apply(style);
            foreach (var kv in layoutAttributeWas)
                if (!layoutAttribute.ContainsKey(kv.Key))
                    layoutAttributeChanged[kv.Key] = layoutAttributeDefault[kv.Key];

            setMode = false;
        }

        public virtual void Apply(string style)
        {
            if (style.Trim() != "") Parse(style);
        }

        public virtual void Parse(string style)
        {
            var items = style.Split(';');
            foreach (var item in items)
            {
                if (item.Trim() == "") continue;
                var part = item.Trim().Split(':');
                if (part.Length == 2)
                    this[part[0].Trim()] = part[1].Trim();
                else
                    throw new System.Exception("Failed to parse style [" + item + "] in  [" + style + "]");
            }
        }

        public virtual void SetDefault(bool clear_text_values = true)
        {
            if (clear_text_values)
            {
                layoutAttributeChanged.Clear();
                layoutAttribute.Clear();
            }
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
