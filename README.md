# Flexbox

C# implementation of [flexbox CSS](https://www.w3.org/TR/css-flexbox-1/) layout algorithm. 

Initial code was extracted from [endlesstravel/Rockyfi](https://github.com/endlesstravel/Rockyfi), which containts pure c#-port of [kjk/flex](https://github.com/kjk/flex), which are pure go-port of [facebook/yoga](https://github.com/facebook/yoga) 

Initial code was extracted on May 16, 2019 and represent state of commit [9b9728](https://github.com/endlesstravel/Rockyfi/tree/9b972864658479a2f353c1e0043a926698061298)

For examples code please examine [endlesstravel/Rockyfi/README.md](https://github.com/endlesstravel/Rockyfi/blob/9b972864658479a2f353c1e0043a926698061298/README.md#usage)

## Changes:

* Default values sets to [flexbox CSS](https://www.w3.org/TR/css-flexbox-1/), instead of [facebook/yoga](https://github.com/facebook/yoga) 
* Some changes in `Flex.Parse.cs`
* `Node.LayoutGet*()` methods are internal: use Node.layout instead - `Node.Layout` struct (see `Node.Layout.cs`), which contain cached dimensions of layout, includes edges in absolute coordinates.

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
| border-left | 0 | `<length>` |
| border-right | 0 | `<length>` |
| border-top | 0 | `<length>` |
| border-bottom | 0 | `<length>` |

## Docs & Playgrounds

* https://www.w3.org/TR/css-flexbox-1/

* https://html5book.ru/css3-flexbox/

* https://yogalayout.com/

* https://css-tricks.com/snippets/css/a-guide-to-flexbox/
