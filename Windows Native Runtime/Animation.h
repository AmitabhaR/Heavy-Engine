#ifndef ANIMATION_H

#define ANIMATION_H

#include "HExtra.h"
#include "GameObject_Scene.h"

#include<list>

class Animation
{
private:
	std::list<HImage> images;
	float update_delay = 0.0f;
	float update_counter = 0.0f;
	int current_frame = 0;
	bool canRepeat = false;
	bool isRunning = false;
	GameObject_Scene * baseGameObject;

public:

	Animation(GameObject_Scene * , float , bool );
	
	void start();

	void addFrame(HImage );

	bool isPlaying();

	void addFrame(std::string );

	void setFrame(int, HImage);

	void deleteFrame(int );

	HImage getFrame();

	void stop();

	void update();
};

typedef Animation * Animation_ptr;

#endif
