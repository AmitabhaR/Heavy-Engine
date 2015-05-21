
#ifndef NAVIGATION_MANAGER_H

#define NAVIGATION_MANAGER_H

#include "Navigator.h"

class NavigationManager
{
public:
	static void registerNavigation(Navigator *);

	static void updateNavigation();
};

#endif