#include "HeavyEngine.h"
#include "NavigationManager.h"

static std::list<Navigator *> navigator_list;

void NavigationManager::registerNavigation(Navigator * navigation_ptr)
{
	navigator_list.push_back(navigation_ptr);
	if (!navigation_ptr->isNavigating()) navigation_ptr->start();
}

void NavigationManager::updateNavigation()
{
	int cnt = 0;
x:

	for (; cnt < navigator_list.size(); cnt++)
	{
		int cntr = 0;
		Navigator_ptr navigator_ptr = NULL;
		std::list<Navigator_ptr>::iterator navigator_iter;

		for (std::list<Navigator_ptr>::iterator cur_navigator = navigator_list.begin(); cur_navigator != navigator_list.end(); cur_navigator++, cntr++)
		{
			if (cntr == cnt)
			{
				navigator_ptr = *cur_navigator;
				navigator_iter = cur_navigator;
				break;
			}
		}

		if (!navigator_ptr->isNavigating())
		{
			navigator_list.erase(navigator_iter);
			goto x;
		}
		else navigator_ptr->update();
	}
}

void NavigationManager::updateNavigatorTargets(Vector2 pos) // Called by camera class only.
{
	for(register std::list<Navigator *>::iterator cur_nav = navigator_list.begin();cur_nav != navigator_list.end( );cur_nav++) (*cur_nav)->cameraUpdatePoints(pos);
}

void NavigationManager::updateNavigatorTargets(float rotation_angle) // Called by camera class only.
{
	for(std::list<Navigator *>::iterator cur_obj = navigator_list.begin( );cur_obj != navigator_list.end( );cur_obj++) if ((*cur_obj)->isCameraRotationAllowed()) (*cur_obj)->cameraRotatePoints(rotation_angle);
}