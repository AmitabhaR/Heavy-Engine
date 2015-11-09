#include "HeavyEngine.h"
#include "Camera.h"

static Vector2 camera_pos;
static float camera_rotation;

void Camera::TranslateCamera(Vector2 pos)
{
	camera_pos.x += pos.x;
	camera_pos.y += pos.y;
	std::list<GameObject_Scene_ptr> gameObject_list = HApplication::getActiveScene()->getAllGameObjects();

	for (register std::list<GameObject_Scene_ptr>::iterator cur_obj = gameObject_list.begin(); cur_obj != gameObject_list.end(); cur_obj++)
		if ((*cur_obj)->AllowCameraTranslation) (*cur_obj)->Translate(-pos.x, -pos.y);

	NavigationManager::updateNavigatorTargets(pos);
}

void Camera::RotateCamera(float rotate_angle)
{
	camera_rotation += rotate_angle;
	std::list<GameObject_Scene_ptr> gameObject_list = HApplication::getActiveScene()->getAllGameObjects();

	for (register std::list<GameObject_Scene_ptr>::iterator cur_obj = gameObject_list.begin(); cur_obj != gameObject_list.end(); cur_obj++)
		if ((*cur_obj)->AllowCameraRotation) (*cur_obj)->Rotate(-rotate_angle);

	NavigationManager::updateNavigatorTargets(-rotate_angle);
}

Vector2 Camera::getCameraPosition() { return camera_pos; }
float Camera::getCameraRotation() { return camera_rotation; }