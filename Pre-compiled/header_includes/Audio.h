#ifndef AUDIO_H

#define AUDIO_H

#include <SDL_mixer.h>
#include <string>

enum
{
	AUDIO_TYPE_SOUND = 0x000ffff,
	AUDIO_TYPE_MUSIC = 0x000fffa,
};

class Audio
{
public:
	Audio(std::string , int );
	bool Play(); 
	void Resume();
	void Pause();
	void Stop();
	void Flush();
private:
	Mix_Music * music_handle;
	Mix_Chunk * sound_handle;
};

#endif