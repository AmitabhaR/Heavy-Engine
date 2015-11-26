#ifndef HEXTRA_H

#define HEXTRA_H

#include<SDL2/SDL.h>
#include<SDL2/SDL_ttf.h>
#include<SDL2/SDL_image.h>
#include<stdio.h>
#include<string>
#include<list>

typedef void (*OnKeyDown)(Uint32 keyCode);
typedef void (*OnMouseDown)(Uint32 button);
typedef void (*OnMouseMove)(Uint32 x, Uint32 y);

// Defination of some basic elements in easy mannner.

typedef SDL_Rect Rect;
typedef SDL_Surface * Image;
typedef SDL_Surface * Canvas;
typedef SDL_Window *  Window;
typedef SDL_Color     Color;
typedef TTF_Font * Font;
typedef FILE * File;

struct HFont
{
	std::string font_name;
	int font_size;
};

struct HImage
{
	SDL_Surface * baseImage;
	int Width;
	int Height;
};

inline SDL_Rect GetRect(int x, int y, int width, int height)
{
	SDL_Rect ret_rect;

	ret_rect.x = x;
	ret_rect.y = y;
	ret_rect.w = width;
	ret_rect.h = height;

	return ret_rect;
}

inline SDL_Rect * GetRectP(int x, int y, int width, int height)
{
	SDL_Rect * ret_rect = new SDL_Rect;

	ret_rect->x = x;
	ret_rect->y = y;
	ret_rect->w = width;
	ret_rect->h = height;

	return ret_rect;
}

#endif
