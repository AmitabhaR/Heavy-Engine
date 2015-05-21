
#ifndef GAMEOBJECT_SCENE_H

#define GAMEOBJECT_SCENE_H

#include "GameObject.h"
#include "HeavyScript.h"
#include<list>

class GameObject_Scene
{
private:
	// Heavy Script List.
	std::list<HeavyScript *> scripts;

public:
	int pos_x;
	int pos_y;
	GameObject obj_instance;
	std::string instance_name;
	int depth;
	bool isDestroyed = false;

	void Translate(int , int );

	void setText(std::string );

	void setStatic(bool );

	void isRigid(bool);

	void isCollider(bool );

	void setTag(int );

	void setColor(Color );

	void setFont(HFont );

	void setImage(HImage );

	void registerScript(HeavyScript * );

	bool scriptsEmpty();

	void processScripts();
};

typedef GameObject_Scene* GameObject_Scene_ptr;

#endif