#include "Animation.h"
#include "ResourceManager.h"
#include <memory>

Animation::Animation(GameObject_Scene * baseObject, float delay, bool repeat)
{
	this->canRepeat = repeat;
	this->update_delay = delay;
	this->baseGameObject = baseObject;
}
void Animation::start()
{
	if (!isRunning)
	{
		isRunning = true;
		current_frame = 0;
	}
}

void Animation::stop()
{
	isRunning = false;
}

bool Animation::isPlaying()
{
	return isRunning;
}

void Animation::addFrame(HImage frame)
{
	this->images.push_back(frame);
}

void Animation::addFrame(std::string name)
{
	if (name != "")
	{
		if (ResourceManager::getResource(name) != "")
		{
			HImage img;

			img.baseImage = IMG_Load(ResourceManager::getResource(name).c_str());

			images.push_back(img);
		}
	}
}

void Animation::update()
{
	if (update_counter >= update_delay)
	{
		if (current_frame == images.size( ))
		{
			if (canRepeat)
			{
				std::list<HImage>::iterator cur_image = images.begin();
				current_frame = 0;
				this->baseGameObject->setImage(*cur_image);
				this->baseGameObject->SetRotationAngle(this->baseGameObject->GetRotationAngle());
				update_counter = 0;
			}
			else
			{
				isRunning = false;
			}
		}
		else
		{
			HImage img;
			int cntr = 0;

			for (std::list<HImage>::iterator cur_image = images.begin(); cur_image != images.end(); cur_image++,cntr++)
			{
				if (cntr == current_frame)
				{
					img = *cur_image;
					break;
				}
			}

			this->baseGameObject->setImage(img);
			this->baseGameObject->SetRotationAngle(this->baseGameObject->GetRotationAngle());
			current_frame++;
			update_counter = 0;
		}
	}
	else
	{
		update_counter += 1;
	}
}

void Animation::deleteFrame(int frame_id)
{
	if (frame_id > -1 && frame_id < images.size( ))
	{
		int cntr = 0;

		for (std::list<HImage>::iterator cur_frame = this->images.begin(); cur_frame != this->images.end(); cur_frame++, cntr++)
		{
			if (cntr == frame_id)
			{
				images.erase(cur_frame);
			}
		}
	}
}

HImage Animation::getFrame()
{
	int cntr = 0;

	for (std::list<HImage>::iterator cur_frame = this->images.begin(); cur_frame != this->images.end(); cur_frame++, cntr++)
	{
		if (cntr == current_frame)
		{
			return *cur_frame;
		}
	}
}

void Animation::setFrame(int frame_id, HImage frame)
{
	if (frame_id > -1 && frame_id < this->images.size())
	{
		int cntr = 0;

		for (std::list<HImage>::iterator cur_frame = this->images.begin(); cur_frame != this->images.end(); cur_frame++,cntr++)
		{
			if (cntr == frame_id)
			{
				*cur_frame = frame;
			}
		}
	}
}

