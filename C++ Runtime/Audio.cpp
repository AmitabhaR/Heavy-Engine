#include "Audio.h"

Audio::Audio(std::string file_name,int type)
{
	FILE * fl = fopen(file_name.c_str(),"r");

	if (fl)
	{
		fclose(fl);

		if (type == AUDIO_TYPE_MUSIC)
		{
			music_handle = Mix_LoadMUS(file_name.c_str());
		}
		else
		{
			sound_handle = Mix_LoadWAV(file_name.c_str());
		}
	}
}

bool Audio::Play()
{
	if (sound_handle)
	{
		Mix_PlayChannel(-1, sound_handle, -1);
		return true;
	}
	else if (music_handle)
	{
		Mix_PlayMusic(music_handle, -1);
		return true;
	}
	else
	{
		return false;
	}
}

void Audio::Resume()
{
	if (this->music_handle)
	{
		Mix_ResumeMusic();
	}
}

void Audio::Pause()
{
	if (this->music_handle)
	{
		Mix_PauseMusic();
	}
}

void Audio::Stop()
{
	if (this->music_handle)
	{
		Mix_HaltMusic();
	}
}

void Audio::Flush()
{
	if (this->music_handle)
	{
		Mix_FreeMusic(this->music_handle);
	}
	else if (this->sound_handle)
	{
		Mix_FreeChunk(this->sound_handle);
	}
}