# Flexbox

C# implementation of [flexbox CSS](https://www.w3.org/TR/css-flexbox-1/) layout algorithm. 

Initial code was extracted from [endlesstravel/Rockyfi](https://github.com/endlesstravel/Rockyfi), which containts pure c#-port of [kjk/flex](https://github.com/kjk/flex), which are pure go-port of [facebook/yoga](https://github.com/facebook/yoga) 

Initial code was extracted on May 16, 2019 and represent state of commit [9b9728](https://github.com/endlesstravel/Rockyfi/tree/9b972864658479a2f353c1e0043a926698061298)

For examples code please examine [endlesstravel/Rockyfi/README.md](https://github.com/endlesstravel/Rockyfi/blob/9b972864658479a2f353c1e0043a926698061298/README.md#usage)

## Changes:

* Default values sets to [flexbox CSS](https://www.w3.org/TR/css-flexbox-1/), instead of [facebook/yoga](https://github.com/facebook/yoga) 
* Some changes in `Flex.Parse.cs`

## Supported style attributes

| Name | Default | Example |
|-|-|-|
| display | flex | none \| flex |
| overflow | visible | visible \| hidden \|  scroll |
| position | relative | relative \| absolute |
| align-content | stretch | auto \| flex-start \| center \| flex-end \| stretch \| baseline \| space-between \| space-around |
| align-items | stretch | auto \| flex-start \| center \| flex-end \| stretch \| baseline \| space-between \| space-around |
| align-self | auto | auto \| flex-start \| center \| flex-end \| stretch \| baseline \| space-between \| space-around |
| flex-direction | row | column \| column-reverse \| row \| row-reverse |
| flex-wrap | no-wrap| no-wrap \| wrap \| wrap-reverse |
| flex-basis | auto | 15px \| 30% |
| flex-shrink | 1 | 5 \| -3.2 |
| flex-grow | 0 | 5 \| -3.2 |
| justify-content | flex-start | flex-start \| center \| flex-end \| space-between \| space-around |
| direction | inherit (ltr) | inherit \| ltr \| rtl |
| left | auto | 15px \| 30% |
| top | auto | 15px \| 30% |
| right | auto | 15px \| 30% |
| bottom | auto | 15px \| 30% |
| width | auto | 15px \| 30% |
| height | auto | 15px \| 30% |
| min-width | auto | 15px \| 30% |
| min-height | auto | 15px \| 30% |
| max-width | auto | 15px \| 30% |
| max-height | auto | 15px \| 30% |
| margin | 0 | 1px \| 1px 2px 3px 4px \| 2px 3px |
| margin-left | 0 | 15px \| 30% |
| margin-right | 0 | 15px \| 30% |
| margin-top | 0 | 15px \| 30% |
| margin-bottom | 0 | 15px \| 30% |
| padding | 0 | 1px \| 1px 2px 3px 4px \| 2px 3px |
| padding-left | 0 | 15px \| 30% |
| padding-right | 0 | 15px \| 30% |
| padding-top | 0 | 15px \| 30% |
| padding-bottom | 0 | 15px \| 30% |
| border-width | 0 | 1px \| 1px 2px 3px 4px \| 2px 3px |
| border-left | 0 | 15px |
| border-right | 0 | 15px |
| border-top | 0 | 15px |
| border-bottom | 0 | 15px |

## Docs & Playgrounds

* https://www.w3.org/TR/css-flexbox-1/

* https://html5book.ru/css3-flexbox/

* https://yogalayout.com/

* https://css-tricks.com/snippets/css/a-guide-to-flexbox/
