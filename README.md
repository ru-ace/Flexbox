# Flexbox

C# implementation of [flexbox CSS](https://www.w3.org/TR/css-flexbox-1/) layout algorithm. 

Initial code was extracted from [endlesstravel/Rockyfi](https://github.com/endlesstravel/Rockyfi), which containts pure c#-port of [kjk/flex](https://github.com/kjk/flex), which are pure go-port of [facebook/yoga](https://github.com/facebook/yoga) 

Initial code was extracted on May 16, 2019 and represent state of commit [9b9728](https://github.com/endlesstravel/Rockyfi/tree/9b972864658479a2f353c1e0043a926698061298)

## Quick start

```csharp
var root = Flex.CreateDefaultNode();
var node1 = Flex.CreateDefaultNode();
var node2 = Flex.CreateDefaultNode();
var node3 = Flex.CreateDefaultNode();

root.AddChild(node1);
root.AddChild(node2);
root.AddChild(node3);

root.nodeStyle.Apply("flex-direction: row; width: 200px; height: 200px; padding: 5px;");
node1.nodeStyle.Apply("align-self: flex-start; margin: 5px; height: 50%; flex-grow: 1;");
node2.nodeStyle.Apply("align-self: center; margin: 5px; height: 50%; flex-grow: 1;");
node3.nodeStyle.Apply("align-self: flex-end; margin: 5px; height: 50%; flex-grow: 1;");

Flex.CalculateLayout(root, 200f, 200f, Direction.LTR);
```
If you render rectangles for each node with (x: node.layout.absoluteLeft, y: node.layout.absoluteTop, w: node.layout.width, w: node.layout.height) you will get same result as this html:  
```html
<html>
<head><style> node { display: flex; box-sizing: border-box; border: 1px solid black; }</style></head>
<body>
    <node id="root" style="flex-direction: row; width: 200px; height: 200px; padding: 5px;">
        <node id="node1" style="align-self: flex-start; margin: 5px; height: 50%; flex-grow: 1;"></node>
        <node id="node2" style="align-self: center; margin: 5px; height: 50%; flex-grow: 1;"></node>
        <node id="node3" style="align-self: flex-end; margin: 5px; height: 50%; flex-grow: 1;"></node>
    </node>
</body>
</html>
```

## Changes:

* Default values sets to [flexbox CSS](https://www.w3.org/TR/css-flexbox-1/), instead of [facebook/yoga](https://github.com/facebook/yoga):
    * align-content: flex-start
    * flex-direction: column
* Some changes in `Flex.Parse.cs`
* `Node.LayoutGet*()` methods are internal: use Node.layout instead - `Node.Layout` struct (see `Node.Layout.cs`), which contain cached dimensions of layout, includes edges in absolute coordinates.
* `Style` can parse text in css format. Support change tracking.

## Supported style attributes

* `<integer>` = `integer value like [-1, 0, 1]` 
* `<number>` = `extend <integer> with float values like [-1.2, 0, 1.2]`
* `<length>` = `<integer>` | `<integer>px`
* `<length-percentage>` = `<length>` | `<number>%`

| Name | Default | Value |                                 
|-|-|-|
| display | flex | `none` \| `flex` |
| overflow | visible | `visible` \| `hidden` \| `scroll` |
| position | relative | `relative` \| `absolute` |
| align-content | stretch | `auto` \| `flex-start` \| `center` \| `flex-end` \| `stretch` \| `baseline` \| `space-between` \| `space-around` |
| align-items | stretch | `auto` \| `flex-start` \| `center` \| `flex-end` \| `stretch` \| `baseline` \| `space-between` \| `space-around` |
| align-self | auto | `auto` \| `flex-start` \| `center` \| `flex-end` \| `stretch` \| `baseline` \| `space-between` \| `space-around` |
| flex-direction | row | `column` \| `column-reverse` \| `row` \| `row-reverse` |
| flex-wrap | no-wrap | `no-wrap` \| `wrap` \| `wrap-reverse` |
| flex-basis | auto | `auto` \| `<length>` |
| flex-shrink | 1 | `<number>` |
| flex-grow | 0 | `<number>` |
| justify-content | flex-start | `flex-start` \| `center` \| `flex-end` \| `space-between` \| `space-around` |
| direction | inherit ltr | `inherit` \| `ltr` \| `rtl` |
| left | auto | `auto` \| `<length-percentage>` |
| top | auto | `auto` \| `<length-percentage>` |
| right | auto | `auto` \| `<length-percentage>` |
| bottom | auto | `auto` \| `<length-percentage>` |
| width | auto | `auto` \| `<length-percentage>` |
| height | auto | `auto` \| `<length-percentage>` |
| min-width | auto | `auto` \| `<length-percentage>` |
| min-height | auto | `auto` \| `<length-percentage>` |
| max-width | auto | `auto` \| `<length-percentage>` |
| max-height | auto | `auto` \| `<length-percentage>` |
| margin | 0 | `<length-percentage>{1,4}` |
| margin-left | 0 | `<length-percentage>` |
| margin-right | 0 | `<length-percentage>` |
| margin-top | 0 | `<length-percentage>` |
| margin-bottom | 0 | `<length-percentage>` |
| padding | 0 | `<length-percentage>{1,4}` |
| padding-left | 0 | `<length-percentage>` |
| padding-right | 0 | `<length-percentage>` |
| padding-top | 0 | `<length-percentage>` |
| padding-bottom | 0 | `<length-percentage>` |
| border-width | 0 | `<length>{1,4}` |
| border-left-width | 0 | `<length>` |
| border-right-width | 0 | `<length>` |
| border-top-width | 0 | `<length>` |
| border-bottom-width | 0 | `<length>` |

## Docs & Playgrounds

* https://yogalayout.com/

* https://www.w3.org/TR/css-flexbox-1/

* https://css-tricks.com/snippets/css/a-guide-to-flexbox/
