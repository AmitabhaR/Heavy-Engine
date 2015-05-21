#include "GameObject_Scene.h"

void GameObject_Scene::Translate(int x, int y)
{
	pos_x += x;
	pos_y += y;
}

void GameObject_Scene::setText(std::string text)
{
	obj_instance.text = text;
}

void GameObject_Scene::setStatic(bool value)
{
	obj_instance._static = value;
}

void GameObject_Scene::isRigid(bool value)
{
	obj_instance.rigidbody = value;
}

void GameObject_Scene::isCollider(bool value)
{
	obj_instance.collider = value;
}

void GameObject_Scene::setTag(int tag)
{
	obj_instance.tag = tag;
}

void GameObject_Scene::setColor(Color col)
{
	obj_instance.color = col;
}

void GameObject_Scene::setFont(HFont font)
{
	obj_instance.font_name = font.font_name;
	obj_instance.font_size = (int) font.font_size;
}

void GameObject_Scene::setImage(HImage img)
{
	obj_instance.img = img;
}

void GameObject_Scene::registerScript(HeavyScript * script)
{
	scripts.push_back(script);
}

bool GameObject_Scene::scriptsEmpty()
{
	return (scripts.size( ) > 0) ? false : true;
}

void GameObject_Scene::processScripts()
{
	for (std::list<HeavyScript *>::iterator cur_script = scripts.begin(); cur_script != scripts.end();cur_script++)
	{
		((HeavyScript *) *(cur_script))->process(this);
	}
}
