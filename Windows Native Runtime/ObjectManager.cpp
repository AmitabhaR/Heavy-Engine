#include "ObjectManager.h"

static std::list<GameObject> gameObject_array;

void ObjectManager::loadObject(std::string name, std::string text, std::string img_path, int tag, bool isStatic, bool isPhysics, bool isRigid, bool isCollider, int img_Width, int img_Height)
{
	GameObject instance;

	instance.name = name;
	instance.text = text;

	FILE * file_handle = fopen(img_path.c_str(), "r");

	if (file_handle)
	{
		instance.img.baseImage = IMG_Load(img_path.c_str( ));
		instance.img.Width = img_Width;
		instance.img.Height = img_Height;
	}

	instance.tag = tag;
	instance._static = isStatic;
	instance.physics = isPhysics;
	instance.rigidbody = isRigid;
	instance.collider = isCollider;
	instance.font_name = "Verdana";
	instance.font_size = 12;
	instance.color = { 100, 100, 100, 100};
	
	gameObject_array.push_back(instance);
}

GameObject ObjectManager::findGameObjectWithName(std::string name)
{
	for (std::list<GameObject>::iterator cur_obj = gameObject_array.begin(); cur_obj != gameObject_array.end(); cur_obj++)
	{
		if (cur_obj->name == name)
		{
			return *cur_obj;
		}
	}
}

std::list<GameObject> ObjectManager::findGameObjectWithTag(int tag)
{
	std::list<GameObject> ret_list;

	for (std::list<GameObject>::iterator cur_obj = gameObject_array.begin(); cur_obj != gameObject_array.end(); cur_obj++)
	{
		if (cur_obj->tag == tag)
		{
			 ret_list.push_back (*cur_obj);
		}
	}

	return ret_list;
}