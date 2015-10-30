#include "AnimationManager.h"

static std::list<Animation_ptr> animation_list;

void AnimationManager::registerAnimation(Animation_ptr anim_ptr)
{
	animation_list.push_back(anim_ptr);
	if (!anim_ptr->isPlaying()) anim_ptr->start();
}

void AnimationManager::updateAnimation()
{
	int cnt = 0;
x:

	for (; cnt < animation_list.size( ); cnt++)
	{
		int cntr = 0;
		Animation_ptr anim_ptr = NULL;
		std::list<Animation_ptr>::iterator anim_iter;

		for (std::list<Animation_ptr>::iterator cur_anim = animation_list.begin(); cur_anim != animation_list.end(); cur_anim++,cntr++)
		{
			if (cntr == cnt)
			{
				anim_ptr = *cur_anim;
				anim_iter = cur_anim;
			}
		}


		if (!anim_ptr->isPlaying())
		{
			animation_list.erase(anim_iter);
			goto x;
		}
		else
		{
			anim_ptr->update();
		}
	}
}