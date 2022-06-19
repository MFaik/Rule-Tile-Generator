# Rule Tile Generator
## How to download :
  ### - Add package from git (recommended way):
 1) Open the package manager from Window > Package Manager
 2) Click the plus button on the top left
 3) Select "Add package from git URL..."
 4) Paste this link: https://github.com/MFaik/Rule-Tile-Generator.git

## How to use :
  ### Mass Slicing Sprites :
To slice multiple sprites at once right click the sprite in the assets menu and click Editor Tools > Slice Sprites

After that you can enter the size of the tiles and it will slice the sprite. 

  ### Creating Rule Tiles :
1) Make a spritesheet in this format:

![filled](https://user-images.githubusercontent.com/19433863/173577012-fa87e0fc-859b-4a7e-be74-855d68fc3bfb.png)

(or you can just check the [Everest API](https://github.com/EverestAPI/Resources/wiki/Custom-Tilesets))

2) After importing the spritesheet in unity with the sprite mode set to multiple you can just slice it (make sure Keep Empty Rects is checked or use the sprite slicer) 

3) When the sprite sheet is ready, right click the sprite and click Editor Tools > Rule Tile from Multiple Sprite

... and you are done.


## Requirements : 
Tilemap Extras 2.2.2 or newer

Unity
