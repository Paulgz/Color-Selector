# Color-Selector
Unity Color selector ui. To use, copy ColorSelector folder to your project
and place colorSelector prefab to a canvas in your project. The Color selector
has following functions:

setColor(): Set color to a given value

getColor(): Get current color

getState(): returns an enum giving the current state. Can be:

  busy: user selecting color
  
  okay: user selected okay button
  
  cancel: user selected cancel button
