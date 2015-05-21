
#ifndef ANIMATION_MANAGER_H

#define ANIMATION_MANAGER_H

#include "Animation.h"
#include<list>

class AnimationManager
{
public:

	static void registerAnimation(Animation_ptr);

	static void updateAnimation();
};

#endif