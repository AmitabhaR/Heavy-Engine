
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
	int CollisionRectX = 0, CollisionRectY = 0;
	GameObject obj_instance;
	std::string instance_name;
	int depth;
	bool isDestroyed = false;
	bool AllowCameraTranslation = false;
	bool AllowCameraRotation = true;
	bool Visibility = true;


	void Initialize();

	double GetRotationAngle();

	void SetRotationAngle(double);

	double GetScale();

	void SetScale(double);

	void Rotate(double );

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

	void LoadAllChilds(GameObject_Scene * );
	
	GameObject_Scene * FindChildWithName(std::string child_name);

	std::list<GameObject_Scene *> FindChildWithTag(int);

	std::list<GameObject_Scene *> GetAllChilds();

	void UpdateChildPosition(int , int , bool isParent = true);
	
	void UpdateChildRotation(float , bool isParent = true);

	void UpdateChildScale(float , bool isParent = true);
	
	void AddChild(std::string child_name);
	
private: 
	  double rotation_angle;
      double scale_rate;
	  Image source_img;
	  void ApplyRotation(double);
	  bool isChildReady;
	  std::list<GameObject_Scene *> child_list;
};

typedef GameObject_Scene* GameObject_Scene_ptr;

#endif