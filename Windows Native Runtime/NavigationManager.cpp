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
			}
		}


		if (!navigator_ptr->isNavigating())
		{
			navigator_list.erase(navigator_iter);
			goto x;
		}
		else
		{
			navigator_ptr->update();
		}
	}
}