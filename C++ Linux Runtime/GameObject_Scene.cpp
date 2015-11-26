#include "GameObject_Scene.h"
#include "SDL2_rotozoom.h"
#include "HApplication.h"
#include <math.h>

void GameObject_Scene::Translate(int x, int y)
{
	pos_x += x;
	pos_y += y;

	if (!isChildReady) LoadAllChilds(this);
	UpdateChildPosition(x, y);
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

void GameObject_Scene::Initialize()
{
	this->source_img = this->obj_instance.img.baseImage;
}

double GameObject_Scene::GetRotationAngle()
{
	return this->rotation_angle;
}

void GameObject_Scene::SetRotationAngle(double angle)
{
	double prev_angle = rotation_angle;
	this->ApplyRotation(angle);
	this->rotation_angle = angle;

	if (!isChildReady) LoadAllChilds(this);
	UpdateChildRotation(angle - prev_angle);
}

void GameObject_Scene::SetScale(double scale)
{
	if (scale > 0) this->scale_rate = scale;

	if (!isChildReady) LoadAllChilds(this);
	UpdateChildScale(scale);
}

double GameObject_Scene::GetScale()
{
	return this->scale_rate;
}

void GameObject_Scene::Rotate(double angle)
{
	ApplyRotation(rotation_angle + angle);
	rotation_angle += angle;

	if (!isChildReady) LoadAllChilds(this);
	UpdateChildRotation(angle);
}

void GameObject_Scene::ApplyRotation(double angle)
{
	obj_instance.img.baseImage = rotozoomSurface(this->source_img, angle,scale_rate,NULL);
}

void GameObject_Scene::LoadAllChilds(GameObject_Scene * gameObject)
{
	for (std::list<GameObject_Scene *>::iterator cur_obj = gameObject->child_list.begin( ); cur_obj != gameObject->child_list.end( ); cur_obj++)
	{
		*cur_obj = HApplication::getActiveScene()->findGameObject((*cur_obj)->instance_name);
		LoadAllChilds(*cur_obj);
	}

	gameObject->isChildReady = true;
}

GameObject_Scene * GameObject_Scene::FindChildWithName(std::string child_name)
{
	for (std::list<GameObject_Scene *>::iterator cur_obj = child_list.begin(); cur_obj != child_list.end();cur_obj++)
	{
		if ((*cur_obj)->instance_name == child_name) return *cur_obj;
	}

	return NULL;
}

std::list<GameObject_Scene *> GameObject_Scene::FindChildWithTag(int tag)
{
	std::list<GameObject_Scene *> gameObject_list;

	for (std::list<GameObject_Scene *>::iterator cur_obj = child_list.begin(); cur_obj != child_list.end( );cur_obj++)
	{
		if ((*cur_obj)->obj_instance.tag == tag) gameObject_list.push_back(*cur_obj);
	}

	return (gameObject_list.size( ) > 0) ? gameObject_list : std::list<GameObject_Scene *>( );
}

void GameObject_Scene::UpdateChildPosition(int rate_x, int rate_y, bool isParent)
{
	if (!isParent)
	{
		pos_x += rate_x;
		pos_y += rate_y;
	}

	for (std::list<GameObject_Scene *>::iterator cur_obj = child_list.begin(); cur_obj != child_list.end(); cur_obj++)
	{
		(*cur_obj)->UpdateChildPosition(rate_x, rate_y,false);
	}
}

void GameObject_Scene::UpdateChildRotation(float rate_angle, bool isParent)
{
	if (!isParent)
	{
		ApplyRotation((rate_angle + rotation_angle));
		rotation_angle += rate_angle;
	}

	for (std::list<GameObject_Scene *>::iterator cur_obj = child_list.begin(); cur_obj != child_list.end(); cur_obj++)
	{
		double rad_angle = 3.1459 * rate_angle / 180;
		double def_x = (*cur_obj)->pos_x - (pos_x + obj_instance.img.Width / 2);
		double def_y = (*cur_obj)->pos_y - (pos_y + obj_instance.img.Height / 2);

		(*cur_obj)->pos_x += (int)round(cos(rad_angle) * def_x - sin(rad_angle) * def_y);
		(*cur_obj)->pos_y += (int)round(sin(rad_angle) * def_x + cos(rad_angle) * def_y);

		(*cur_obj)->UpdateChildRotation(rate_angle, false);
	}
}

void GameObject_Scene::UpdateChildScale(float rate_scale,bool isParent)
{
	if (!isParent) scale_rate += rate_scale;

	for (std::list<GameObject_Scene *>::iterator cur_obj = child_list.begin(); cur_obj != child_list.end(); cur_obj++)
	{
		(*cur_obj)->UpdateChildScale(rate_scale, false);
	}
}

void GameObject_Scene::AddChild(std::string child_name)
{
	GameObject_Scene * gameObj = new GameObject_Scene();

	gameObj->instance_name = child_name;

	child_list.push_back(gameObj);
}

std::list<GameObject_Scene *> GameObject_Scene::GetAllChilds()
{
	return this->child_list;
}

