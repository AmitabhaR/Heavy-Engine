Release Notes (Beta 1.0.2)
========================

### New Features
	1. Animation Editor.
	2. Navigation Editor.
	3. Windows C++ support with SDL.
	4. UI Update
	   =========
		1. Added game object hierarchy
		2. Added project file viewer.
		3. Editor skin changed.
		4. Replaced Zooming buttons with track bar for smooth working.
		5. Camera X and Y are found on the status strip at the bottom.
	5. Sprite Baker for breaking frames from sprite. 
	6. Non-Threaded Navigator and Animation.
	7. File Name Encryption during Build Supported.
	8. Object selection highlighting supported.
	9. TileMapEditor for making tile-based games.
	10. Added Networking for Windows Native (x86).
	11. Editor now saves the last selected platform by the user.
	12. Added Rotation , Scaling and Parent-Child grouping system features.
	13. Added Duplication of game objects in the editor.
	
### Fixes
	1. Multi-threaded Animation not supported anymore.
	2. Added a save level message for preventing progress loss.
	3. Build system made more reliable by adding support to paths with spaces.
	4. Fixed a animation bug causing no change in animation over time.
	5. Added animation as well as navigation manager for managing those tasks.
	6. Fixed a api problem : scene_array is not available any more directly.
	7. Bosch Scripts Code Generation problem fixed while using scene_addGameObject.
	8. Bosch Scripts Code Generation problem fixed while using scene_activate.
	9. Added Build Log Support for all platforms.
	10. Added build progress window for more better build visulalization.
	11. Added Auto-Resource Import Support.
	12. Fixed Depth Collision Detection and Depth Rendering Problems.
	13. Error with string encryption in native windows build has been fixed.
	14. getResource() function file error has been fixed in windows native build.
	
### Notice
	1. Bosch Scripts not yet updated according to latest features added to the engine . It is usable but the latest features cannot be accessed right now . Further notice will be given for it's update.
