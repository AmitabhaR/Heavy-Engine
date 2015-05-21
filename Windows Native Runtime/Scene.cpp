#include "Scene.h"
#include <algorithm>
#include "HApplication.h"
#include "ResourceManager.h"
#include "NavigationManager.h"
#include "AnimationManager.h"

void Scene::startScene(SDL_Surface * canvas)
{
	this->canvas = canvas;
	isRunning = true;
	makeSorting();
}

bool Scene::checkSorted(std::list<DrawableGameObject> * index_array)
{
	for (int cnt = 0; cnt < index_array->size( ); cnt++)
	{
		DrawableGameObject base_object;

		struct LoopCounter
		{
			std::list<DrawableGameObject>::iterator cur_obj;
			int cntr;
		};

		for (LoopCounter base_counter = { index_array->begin( ) , 0 }; base_counter.cur_obj != index_array->end(); base_counter.cur_obj++, base_counter.cntr++)
		{
			if (base_counter.cntr == cnt)
			{
				base_object = *(base_counter.cur_obj);
				break;
			}
		}

		if (cnt + 1 < index_array->size( ))
		{
			DrawableGameObject cmp_object;

			for (LoopCounter base_counter = { index_array->begin(), 0 }; base_counter.cur_obj != index_array->end(); base_counter.cur_obj++, base_counter.cntr++)
			{
				if (base_counter.cntr == cnt + 1)
				{
					cmp_object = *(base_counter.cur_obj);
					break;
				}
			}

			if (base_object.depth < cmp_object.depth)
			{
				return false;
			}
		}
	}

	return true;
}

void Scene::sortElements(std::list<DrawableGameObject> * index_array)
{
	for (int cnt = 0; cnt < index_array->size( ); cnt++)
	{
		DrawableGameObject base_object;
		std::list<DrawableGameObject>::iterator base_object_iter;

		struct LoopCounter
		{
			std::list<DrawableGameObject>::iterator cur_obj;
			int cntr;
		};

		for (LoopCounter base_counter = { index_array->begin(), 0 }; base_counter.cur_obj != index_array->end(); base_counter.cur_obj++, base_counter.cntr++)
		{
			if (base_counter.cntr == cnt)
			{
				base_object = *(base_counter.cur_obj);
				base_object_iter = base_counter.cur_obj;
				break;
			}
		}

		for (int c = cnt + 1; c < index_array->size( ); c++)
		{
			DrawableGameObject cmp_object;
			std::list<DrawableGameObject>::iterator cmp_object_iter;

			for (LoopCounter base_counter = { index_array->begin(), 0 }; base_counter.cur_obj != index_array->end(); base_counter.cur_obj++, base_counter.cntr++)
			{
				if (base_counter.cntr == c)
				{
					cmp_object = *(base_counter.cur_obj);
					cmp_object_iter = base_counter.cur_obj;
					break;
				}
			}

			if (base_object.depth < cmp_object.depth)
			{
				DrawableGameObject cp = base_object;

				*base_object_iter = cmp_object;
				*cmp_object_iter = cp;

				break;
			}
		}
	}
}

void Scene::makeSorting()
{
	sortedArray = std::list<DrawableGameObject>( );

	for (int cnt = 0; cnt < object_array.size( ); cnt++)
	{
		GameObject_Scene_ptr obj = NULL;

		struct LoopCounter
		{
			std::list<GameObject_Scene_ptr>::iterator cur_obj;
			int cntr;
		};

		for (LoopCounter base_counter = { object_array.begin(), 0 }; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++,base_counter.cntr++)
		{
			if (base_counter.cntr == cnt)
			{
				obj = *(base_counter.cur_obj);
				break;
			}
		}

		DrawableGameObject dobj;

		dobj.depth = obj->depth;
		dobj.index = cnt;

		sortedArray.push_back(dobj);
	}

	while (!checkSorted(&sortedArray))
	{
		sortElements(&sortedArray);
	}
}

void Scene::updateScene()
{
	bool hasDeleted = false;
	
	struct LoopCounter
	{
		std::list<GameObject_Scene_ptr>::iterator cur_obj;
		int cntr;
	};

re:
	for (int cnt = 0; cnt < object_array.size( ); cnt++)
	{
		GameObject_Scene_ptr obj = NULL;

		LoopCounter base_counter = { object_array.begin(), 0 };

		for (; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++,base_counter.cntr++)
		{
			if (base_counter.cntr == cnt)
			{
				obj = *(base_counter.cur_obj);
				break;
			}
		}

		if (obj->isDestroyed)
		{
			object_array.erase(base_counter.cur_obj);
			hasDeleted = true;
			goto re;
		}
	}

	if (hasDeleted) makeSorting();

	for (int cnt = 0; cnt < object_array.size( ); cnt++)
	{
		GameObject_Scene_ptr obj = NULL;

		for (LoopCounter base_counter = { object_array.begin(), 0 }; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++, base_counter.cntr++)
		{
			if (base_counter.cntr == cnt)
			{
				obj = *(base_counter.cur_obj);
				break;
			}
		}

		if (!obj->obj_instance._static)
		{
			if (obj->obj_instance.physics)
			{
				if (obj->obj_instance.rigidbody)
				{
					obj->pos_y += gravity;
				}

				for (int cnt0 = 0; cnt0 < object_array.size( ); cnt0++)
				{
					if (cnt0 == cnt)
					{
						continue;
					}
					else
					{
						GameObject_Scene_ptr cmp_obj = NULL;

						for (LoopCounter base_counter = { object_array.begin(), 0 }; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++, base_counter.cntr++)
						{
							if (base_counter.cntr == cnt0)
							{
								cmp_obj = *(base_counter.cur_obj);
								break;
							}
						}

						if (obj->obj_instance.img.baseImage != NULL && cmp_obj->obj_instance.img.baseImage != NULL && obj->obj_instance.physics && cmp_obj->obj_instance.physics && obj->obj_instance.collider && cmp_obj->obj_instance.collider)
						{
							if (obj->pos_x + obj->obj_instance.img.Width > cmp_obj->pos_x && obj->pos_x < cmp_obj->pos_x + cmp_obj->obj_instance.img.Width && obj->pos_y + obj->obj_instance.img.Height > cmp_obj->pos_y && cmp_obj->pos_y < cmp_obj->pos_y + cmp_obj->obj_instance.img.Height)
							{
								std::list<OnCollisionHandler> col_handler = HApplication::getCollisionHandlers();

								for (std::list<OnCollisionHandler>::iterator cur_handler = col_handler.begin(); cur_handler != col_handler.end(); cur_handler++)
								{
									(*cur_handler)(obj,cmp_obj);
								}
							}
						}
					}
				}
			}

		}

		SDL_Event e;

		while (SDL_PollEvent(&e))
		{
			if (e.type == SDL_KEYDOWN)
			{
				std::list<OnKeyDown> keydown_handlers = HApplication::getKeyboardHandlers();

				for (std::list<OnKeyDown>::iterator cur_handler = keydown_handlers.begin(); cur_handler != keydown_handlers.end(); cur_handler++)
				{
					(*cur_handler)(e.key.keysym.sym);
				}
			}
			else if (e.type == SDL_QUIT)
			{
				HApplication::Exit();
			}
			else if (e.type == SDL_MOUSEBUTTONDOWN)
			{
				if (e.button.button == SDL_BUTTON_LEFT || e.button.button == SDL_BUTTON_RIGHT)
				{
					std::list<OnMouseDown> mousedown_handers = HApplication::getMouseDownHandlers();

					for (std::list<OnMouseDown>::iterator cur_handler = mousedown_handers.begin(); cur_handler != mousedown_handers.end();cur_handler++)
					{
						(*cur_handler)(e.button.button);
					}
				}
			}
			else if (e.type == SDL_MOUSEMOTION)
			{
				std::list<OnMouseMove> mousemove_handlers = HApplication::getMouseMoveHandlers();

				for (std::list<OnMouseMove>::iterator cur_handler = mousemove_handlers.begin(); cur_handler != mousemove_handlers.end(); cur_handler++)
				{
					(*cur_handler)(e.button.x, e.button.y);
				}
			}
		}

		if (!obj->scriptsEmpty())
		{
			obj->processScripts();
		}
	}

	NavigationManager::updateNavigation();
	AnimationManager::updateAnimation();

	drawScene();
	SDL_Delay(speed);
}

void Scene::drawScene()
{
	struct LoopCounter
	{
		std::list<GameObject_Scene_ptr>::iterator cur_obj;
		int cntr;
	};

	struct LoopCounter_DrawableGameObject
	{
		std::list<DrawableGameObject>::iterator cur_obj;
		int cntr;
	};

	SDL_FillRect(this->canvas, GetRectP(0, 0, HApplication::getSize().w, HApplication::getSize().h),SDL_MapRGBA(canvas->format,R,G,B,A));

	for (int cntr = 0; cntr < sortedArray.size( ); cntr++)
	{
		DrawableGameObject obj;

		for (LoopCounter_DrawableGameObject base_counter = { sortedArray.begin(), 0 }; base_counter.cur_obj != sortedArray.end(); base_counter.cur_obj++, base_counter.cntr++)
		{
			if (cntr == base_counter.cntr)
			{
				obj = *(base_counter.cur_obj);
				break;
			}
		}

		int cnt = obj.index;

		GameObject_Scene_ptr base_obj = NULL;

		for (LoopCounter base_counter = { object_array.begin(), 0 }; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++, base_counter.cntr++)
		{
			if (cnt == base_counter.cntr)
			{
				base_obj = *(base_counter.cur_obj);
				break;
			}
		}

		if (base_obj->obj_instance.img.baseImage != NULL)
		{
			SDL_BlitSurface(base_obj->obj_instance.img.baseImage, NULL, canvas,GetRectP(base_obj->pos_x,base_obj->pos_y,base_obj->obj_instance.img.Width,base_obj->obj_instance.img.Height));
		}
		else if (base_obj->obj_instance.text != "")
		{
			TTF_Font * baseFont = TTF_OpenFont(ResourceManager::getResource(base_obj->obj_instance.font_name).c_str(), base_obj->obj_instance.font_size);
			SDL_Surface * baseSurface = TTF_RenderText_Solid(baseFont, base_obj->obj_instance.text.c_str(), base_obj->obj_instance.color);
			
			SDL_BlitSurface(baseSurface, NULL, canvas, GetRectP(base_obj->pos_x,base_obj->pos_y,0,0));
		}
	}

	SDL_UpdateWindowSurface(HApplication::getWindowHandle());
}

void Scene::endScene()
{
	this->isRunning = false;
}

bool Scene::loadGameObject(GameObject_Scene_ptr gameObject)
{
	if (gameObject->instance_name == "")
	{
		return false;
	}

	struct LoopCounter
	{
		std::list<GameObject_Scene_ptr>::iterator cur_obj;
		int cntr;
	};

	for (LoopCounter base_counter = { object_array.begin(), 0 }; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++)
	{
		GameObject_Scene_ptr gameObj = *(base_counter.cur_obj);

		if (gameObj->instance_name == gameObject->instance_name)
		{
			return false;
		}
	}

	object_array.push_back(gameObject);
	makeSorting();

	return true;
}

void Scene::destroyGameObject(std::string instance_name)
{
	struct LoopCounter
	{
		std::list<GameObject_Scene_ptr>::iterator cur_obj;
		int cntr;
	};

	for (LoopCounter base_counter = { object_array.begin(), 0 }; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++)
	{
		GameObject_Scene_ptr gameObj = *(base_counter.cur_obj);

		if (gameObj->instance_name == instance_name)
		{
			gameObj->isDestroyed = true;
			return;
		}
	}
}

GameObject_Scene_ptr Scene::findGameObject(std::string name)
{
	struct LoopCounter
	{
		std::list<GameObject_Scene_ptr>::iterator cur_obj;
		int cntr;
	};

	for (LoopCounter base_counter = { object_array.begin(), 0 }; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++)
	{
		GameObject_Scene_ptr gameObj = *(base_counter.cur_obj);

		if (gameObj->instance_name == name)
		{
			return gameObj;
		}
	}

	return NULL;
}

std::list<GameObject_Scene_ptr> Scene::findGameObject(int tag)
{
	std::list<GameObject_Scene_ptr> ret_list;

	struct LoopCounter
	{
		std::list<GameObject_Scene_ptr>::iterator cur_obj;
		int cntr;
	};

	for (LoopCounter base_counter = { object_array.begin(), 0 }; base_counter.cur_obj != object_array.end(); base_counter.cur_obj++)
	{
		GameObject_Scene_ptr gameObj = *(base_counter.cur_obj);

		if (gameObj->obj_instance.tag == tag)
		{
			ret_list.push_back(gameObj); 
		}
	}

	return ret_list;
}
